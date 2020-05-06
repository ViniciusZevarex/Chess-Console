using System;
using board;
using chess;

namespace ChessConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Board board = new Board(8, 8);

                board.ToSetPiece(new Tower(board, Color.Black), new Position(0, 0));
                board.ToSetPiece(new Tower(board, Color.Black), new Position(1, 3));
                board.ToSetPiece(new King(board, Color.Black), new Position(2, 4));

                Display.ToDisplayBoard(board);
            }
            catch (BoardException e)
            {
                Console.WriteLine("Error: " + e.Message);
            }
            
        }
    }
}
