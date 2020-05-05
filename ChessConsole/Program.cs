using System;
using board;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(8, 8);

            Display.ToDisplayBoard(board);
        }
    }
}
