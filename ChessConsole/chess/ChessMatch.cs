using board;
using System;

namespace chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public int Turn { get; set; }
        public bool Finished { get; set; }

        public ChessMatch()
        {
            Board = new Board(8,8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            initializePieces();
        }

        public void ToMove(Position origin, Position destiny)
        {
            Piece p = Board.ToRemovePiece(origin);
            p.UpdateAmtMoviments();//update amount moviments of this piece
            Piece capturedPiece = Board.ToRemovePiece(origin); //to controll the capitured pieces

            Board.ToSetPiece(p, destiny);
        }

        public void initializePieces()
        {
            Board.ToSetPiece(new Tower(Board, Color.Black), new ChessPosition('c', 1).ToPosition());

            Board.ToSetPiece(new Tower(Board, Color.White), new ChessPosition('c', 8).ToPosition());

        }
    }
}
