using System;
using System.Collections.Generic;
using Egsp.Core;
using UnityEngine;

namespace Game.Io
{
    [RequireComponent(typeof(GameInputUsers))]
    public class GameInputSystem : MonoBehaviour
    {
        private Option<InputSettings> _inputSettings = Option<InputSettings>.None;

        private List<KeyObserver> _keyObservers = new List<KeyObserver>();
        private List<AxisObserver> _axisObservers = new List<AxisObserver>();
        
        public List<IInputUser> InputUsers { private get; set; }

        private void Awake()
        {
            _inputSettings = LoadInputs();
            if (_inputSettings.IsNone)
                _inputSettings = InputSettings.PcSettings();
            
            _keyObservers = CreateKeyObservers(_inputSettings.Value);
            _axisObservers = CreateAxisObservers(_inputSettings.Value);
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
        public KeyState Get(GameplayKeyCode key)
        {
            var coincidence = FindKey(key);

            if (coincidence.IsNone)
                return KeyState.Inactive;
            
            return coincidence.Value.KeyState;
        }

        public bool GetState(GameplayKeyCode key, KeyState state, bool holdTolerance = true)
        {
            var keyState = Get(key);
            switch (keyState)
            {
                case KeyState.Inactive:
                    return keyState == state;
                case KeyState.Down:
                    return keyState == state;
                case KeyState.Hold:
                    if (holdTolerance)
                    {
                        if (state == KeyState.Down)
                            return true;
                        
                        return keyState == state;
                    }
                    else
                    {
                        return keyState == state;
                    }
            }

            return false;
        }

        private Option<KeyObserver> FindKey(GameplayKeyCode key)
        {
            return _keyObservers.FirstOrNone(x =>
                x.Key.GameplayKey == key);
        }

        public float GetAxis(GameplayAxis axis)
        {
            var axisO = FindAxis(axis);
            if (axisO.IsNone)
                return 0;

            return axisO.Value.Get();
        }

        private Option<AxisObserver> FindAxis(GameplayAxis axis)
        {
            return _axisObservers.FirstOrNone(x =>
                x.GameplayAxis == axis);
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

        private List<AxisObserver> CreateAxisObservers(InputSettings input)
        {
            var axisObservers = new List<AxisObserver>(input.Axes.Count);

            for (var i = 0; i < input.Axes.Count; i++)
            {
                var axis = input.Axes[i];
                var axisObserver = new AxisObserver(axis);
                axisObservers.Add(axisObserver);
            }

            return axisObservers;
        }
    }
}