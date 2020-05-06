using System;
using board;
using chess;

namespace ChessConsole
{
    class Display
    {
        public static void ToDisplayBoard(Board board)
        {
            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    ToDisplayPiece(board.GetPiece(i, j));
                }
                Console.WriteLine();
            }
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ToDisplayBoard(Board board, bool[,] possiblePositions)
        {
            ConsoleColor OriginalBackgroundColor = Console.BackgroundColor;
            ConsoleColor OtherBackgroundColor = ConsoleColor.DarkGray;


            for (int i = 0; i < board.Rows; i++)
            {
                Console.Write(8 - i + " ");
                for (int j = 0; j < board.Columns; j++)
                {
                    if (possiblePositions[i,j])
                    {
                        Console.BackgroundColor = OtherBackgroundColor;
                    }
                    else
                    {
                        Console.BackgroundColor = OriginalBackgroundColor;
                    }

                    ToDisplayPiece(board.GetPiece(i, j));
                }
                Console.WriteLine();
            }
            Console.BackgroundColor = OriginalBackgroundColor;
            Console.WriteLine("  a b c d e f g h");
        }

        public static void ToDisplayPiece(Piece p)
        {
            if (p == null)
            {
                Console.Write("- ");
            }
            else
            {
                if (p.Color == Color.White)
                {
                    Console.Write(p);
                }
                else
                {
                    ConsoleColor aux = Console.ForegroundColor;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write(p);
                    Console.ForegroundColor = aux;
                }
                Console.Write(" ");
            }


        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine();
            char column = s[0];
            int row = int.Parse(s[1] + "");

            return new ChessPosition(column, row);
        }
    }
}
