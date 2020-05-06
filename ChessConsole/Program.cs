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
                ChessMatch match = new ChessMatch();

                while (!match.Finished)
                {
                    Console.Clear();

                    Display.ToDisplayBoard(match.Board);

                    Console.WriteLine();
                    Console.Write("Origem: ");
                    Position origin = Display.ReadChessPosition().ToPosition();

                    Console.Write("Destino: ");
                    Position destiny = Display.ReadChessPosition().ToPosition();

                    match.ToMove(origin, destiny);
                }

                 
             }
             catch (BoardException e)
             {
                 Console.WriteLine("Error: " + e.Message);
             }
        }
    }
}
