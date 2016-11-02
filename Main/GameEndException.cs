﻿using System;

namespace Main
{
    public class GameEndException : Exception
    {
        private GameEndReason reason;
        public GameEndException(string message, GameEndReason reason) : base(message)
        {
            this.reason = reason;
        }
        public enum GameEndReason { ExitFound }
    }
}
