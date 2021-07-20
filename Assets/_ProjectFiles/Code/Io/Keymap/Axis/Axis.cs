using System.Collections.Generic;

namespace Game.Io
{
    public class Axis
    {
        public readonly GameplayAxis GameplayAxis;

        public readonly string Name;
        
        public Axis(GameplayAxis gameplayAxis, string name)
        {
            GameplayAxis = gameplayAxis;
            Name = name;
        }
    }
}