using System.Collections.Generic;
using UnityEngine;

namespace Game.Io
{
    [RequireComponent(typeof(GameInputSystem))]
    public class GameInputUsers : MonoBehaviour
    {
        [SerializeField] private List<GameObject> inputUsers;

        private void Awake()
        {
            var system = GetComponent<GameInputSystem>();
            if (system != null)
            {
                var list = new List<IInputUser>(inputUsers.Count);

                for (var i = 0; i < inputUsers.Count; i++)
                {
                    var user = inputUsers[i].GetComponent<IInputUser>();
                    if (user != null)
                        list.Add(user);
                }

                system.InputUsers = list;
            }
        }
    }
}