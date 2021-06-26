using System;

namespace Game
{
    /// <summary>
    /// Структура хранит в себе информацию о команде.
    /// </summary>
    public readonly struct Team
    {
        /// <summary>
        /// Идентификатор команды.
        /// </summary>
        public readonly TeamId Id;
        /// <summary>
        /// Название команды.
        /// </summary>
        public readonly TeamName Name;

        public Team(TeamId id, TeamName name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return $"{Id}|{Name}";
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            if (obj is Team team)
            {
                if (Id == team.Id)
                    return true;
            }

            return false;
        }

        public static bool operator ==(Team a, Team b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(Team a, Team b)
        {
            return !(a == b);
        }
    }
}