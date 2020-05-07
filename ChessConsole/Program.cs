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
                    try
                    {
                        Console.Clear();

                        Display.ToDisplayBoard(match.Board);
                        Console.WriteLine();

                        Console.WriteLine("Turno: " + match.Turn);
                        Console.WriteLine("Aguardando jogada: " + match.CurrentPlayer);

                        Console.WriteLine();
                        Console.Write("Origem: ");
                        Position origin = Display.ReadChessPosition().ToPosition();

                        match.ValidateOriginPosition(origin);


                        bool[,] possiblePositions = match.Board.GetPiece(origin).PossiblesMoviments();

                        Console.Clear();
                        Display.ToDisplayBoard(match.Board, possiblePositions);

                        Console.Write("Destino: ");
                        Position destiny = Display.ReadChessPosition().ToPosition();

                        match.ValidateDestinyPosition(origin, destiny);


                        match.ToPlay(origin, destiny);
                    }
                    catch(BoardException e){
                        Console.WriteLine(e.Message);
                        Console.ReadLine();
                    }
                }

                 
             }
             catch (BoardException e)
             {
                 Console.WriteLine("Error: " + e.Message);
             }
        }
    }
}
