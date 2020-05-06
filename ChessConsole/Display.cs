using System;
using board;

namespace ChessConsole
{
    class Display
    {
        public static void ToDisplayBoard(Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                for (int j = 0; j < board.Columns; j++)
                {
                    //if ternário para verificar se há peças nessa posição
                    Console.Write(
                        (board.GetPiece(i,j) != null ) 
                        ? board.GetPiece(i,j) + " " 
                        : "- "
                        );
                }
                Console.WriteLine();
            }
        }
    }
}
