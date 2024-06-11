using Shaski_Bakhmut.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shaski_Bakhmut
{
    public class Checker
        //Coordinate(Position), Player(Owner) AvailableMoves()
    {
        public CheckerType Type { get; set; } = CheckerType.Regular;
        public List<int> Position { get; set; }
        public Coordinate Coordinate { get; set; }
        public SideType Color { get; set; }
        public bool Killed { get; set; } = false;
        public Player Player { get; private set; }

        public Checker(List<int> position, SideType color)
        {
            Type = CheckerType.Regular;
            Position = position;
            Color = color;
            Killed = false;
        }

        public Checker(Coordinate coordinate, Player player)
        {
            if (player == null)
            {
                throw new ArgumentNullException(nameof(player), "Player cannot be null!");
            }

            Type = CheckerType.Regular;
            Coordinate = coordinate;
            Player = player;
            Killed = false;
        }

        public void MakeLady()
        {
            if (Killed)
            {
                throw new InvalidOperationException("Cannot make a killed checker a lady!");
            }
            Type = CheckerType.Lady;
        }

        public void Kill()
        {
            if (Killed)
            {
                throw new InvalidOperationException("Checker is already killed!");
            }
            Killed = true;
        }

        public void Move(Coordinate coordinate)
        {
            Coordinate = coordinate;
        }
    }
}
