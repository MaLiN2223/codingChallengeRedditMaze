using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLibrary.Game
{
    public abstract class AbstractGame : IGame
    {
        protected MazeMaster master;
        public AbstractGame(MazeMaster master)
        {
            this.master = master;
        }

        public abstract void DisplayDebug(string data);
        public abstract void Start();
    }
}
