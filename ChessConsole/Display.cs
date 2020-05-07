using System;
using System.Collections.Generic;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using board;
using chess;
using Microsoft.VisualBasic.CompilerServices;

namespace ChessConsole
{
    class Display
    {

        public static void ToDisplayMatch(ChessMatch match)
        {
            ToDisplayBoard(match.Board);
            Console.WriteLine();

            ToDisplayCapturedPieces(match);


            Console.WriteLine("Turno: " + match.Turn);

            if (!match.Finished)
            {
                Console.WriteLine("Aguardando jogada: " + match.CurrentPlayer);

                if (match.Check)
                {
                    Console.WriteLine("XEQUE!");
                }
            }
            else
            {
                Console.WriteLine("XEQUEMATE!");
                Console.WriteLine("Vencedor: " + match.CurrentPlayer);
            }
            
        }


        public static void ToDisplayCapturedPieces(ChessMatch match)
        {
            Console.WriteLine("Peças capturadas: ");
            Console.Write("Branca: ");
            CapturedPieces(match.CapituredPieces(Color.White));
            Console.WriteLine();
            Console.Write("Pretas: ");
            CapturedPieces(match.CapituredPieces(Color.Black));
            Console.WriteLine();
        }

        public static void CapturedPieces(HashSet<Piece> hashset)
        {
            Console.Write("{");

            foreach (Piece item in hashset)
            {
                Console.Write(item + " ");
            }
            Console.Write("}");
        }

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
                Console.BackgroundColor = OriginalBackgroundColor;
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
