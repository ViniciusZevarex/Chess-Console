using System;
using board;

namespace ChessConsole
{
    class Display
    {
        public static void ToDisplayBoard(Board board)
        {
            for (int i = 0; i < board.Row; i++)
            {
                for (int j = 0; j < board.Column; j++)
                {
                    Console.Write(
                        (board.getPiece(i,j) != null ) 
                        ? board.getPiece(i,j) + " " 
                        : "- "
                        );
                }
                Console.WriteLine();
            }
        }
    }
}
