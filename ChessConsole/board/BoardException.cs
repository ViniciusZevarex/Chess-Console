using System;

namespace board
{
    class BoardException : ApplicationException
    {
        public BoardException(string msg) : base(msg) { }
    }
}
