using Egsp.Core;

namespace Game
{
    /// <summary>
    /// Представляет собой счетчик очков захвата команд.
    /// </summary>
    public class TeamClaim
    {
        public const uint MaxScore = 10;
        
        /// <summary>
        /// Лидирующая по счету команда.
        /// </summary>
        public Option<Team> Leader { get; private set; } = Option<Team>.None;

        /// <summary>
        /// Счет лидирующей команды.
        /// </summary>
        public TeamScore Score { get; private set; } = new TeamScore(1);

        /// <summary>
        /// Добавить очко захвата.
        /// Если команды разные, то очки захвата будут расчитаны в пользу захватчика.
        /// </summary>
        public void Claim(Option<Team> team)
        {
            if (IsLeaderTeam(Leader, team))
            {
                Score += 1;
                if (Score >= MaxScore)
                    Score = new TeamScore(MaxScore);
            }
            else
            {
                ClaimBy(team);
            }
        }

        private void ClaimBy(Option<Team> team)
        {
            Score -= 1;
            if (Score == 0)
            {
                // Меняем команду, т.к. прошлый клейм сбит.
                Leader = team;
                Score = new TeamScore(1);
            }
        }

        /// <summary>
        /// Одинаковы ли команды.
        /// </summary>
        private bool IsLeaderTeam(Option<Team> a, Option<Team> b)
        {
            if (a.IsNone || b.IsNone)
            {
                if (a.IsNone && b.IsNone)
                    return true;
                
                return false;
            }

            if (a.Value.Equals(b.Value))
                return true;

            return false;
        }

        public enum ClaimType
        {
            Increase,
            Decrease
        }
    }
}