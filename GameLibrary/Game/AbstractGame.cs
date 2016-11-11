using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Game
{
    public abstract class AbstractGame : IGame
    {
        public AbstractGame()
        {
        }
        public abstract void DisplayDebug(string data);
        public abstract void Start();
    }
}
