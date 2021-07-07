using System.Collections.Generic;
using Egsp.Core;
using UnityEngine;

namespace Game.Io
{
    public class GameInputSystem : MonoBehaviour
    {
        private Option<InputSettings> _inputSettings = Option<InputSettings>.None;

        private List<KeyObserver> _keyObservers = new List<KeyObserver>();
        
        public List<IInputUser> InputUsers { private get; set; }

        private void Awake()
        {
            _inputSettings = LoadInputs();
            if (_inputSettings.IsNone)
                _inputSettings = InputSettings.PcSettings();
            
            _keyObservers = CreateKeyObservers(_inputSettings.Value);
        }

        private void Update()
        {
            var deltaTime = Time.deltaTime;
            for (var i = 0; i < _keyObservers.Count; i++)
            {
                _keyObservers[i].Observe(deltaTime);
            }
            
            for (var i = 0; i < InputUsers.Count; i++)
            {
                InputUsers[i].UseInput(this);
            }
        }

        /// <summary>
        /// Получение состояния ввода.
        /// </summary>
        public KeyState Read(GameplayKeyCode key)
        {
            var coincidence = FindKey(key);

            if (coincidence.IsNone)
                return KeyState.Inactive;
            
            return coincidence.Value.KeyState;
        }

        public bool ReadState(GameplayKeyCode key, KeyState state)
        {
            return Read(key) == state;
        }

        private Option<KeyObserver> FindKey(GameplayKeyCode key)
        {
            return _keyObservers.FirstOrNone(x =>
                x.Key.GameplayKey == key);
        } 

        private Option<InputSettings> LoadInputs()
        {
            var input = Storage.Current.GetObject<InputSettings>(Settings.Input("input"));
            return input;
        }

        private void SaveInputs(InputSettings input)
        {
            Storage.Current.SetObject(input, Settings.Input("input"));
        }

        private List<KeyObserver> CreateKeyObservers(InputSettings input)
        {
            var treshold = input.HoldTreshold;
            var keyObservers = new List<KeyObserver>(input.Keys.Count);
            for (var i = 0; i < input.Keys.Count; i++)
            {
                var keytokey = input.Keys[i];
                var observer = new KeyObserver(keytokey, treshold);
                keyObservers.Add(observer);
            }

            return keyObservers;
        }
    }
}