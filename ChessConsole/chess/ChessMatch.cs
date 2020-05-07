using board;
using System;

namespace chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public int Turn { get; private set; }
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

        public void ToPlay(Position origin, Position destiny)
        {
            ToMove(origin, destiny);
            Turn++;
            ChangePlyaer();
        }

        public void ValidateOriginPosition(Position pos)
        {
            if (Board.GetPiece(pos) == null)
            {
                throw new BoardException("There aren't pice on the origin position choosed!");
            }
            if (CurrentPlayer != Board.GetPiece(pos).Color)
            {
                throw new BoardException("The piece origin choosed is not yours!");
            }
            if (!Board.GetPiece(pos).ThereArePossiblesMoviments())
            {
                throw new BoardException("There aren't possibles moviments to the origin piece choosed!");
            }
        }

        public void ChangePlyaer()
        {
            if (CurrentPlayer == Color.White)
            {
                CurrentPlayer = Color.Black;
            }
            else
            {
                CurrentPlayer = Color.White;
            }
        }

        public void ValidateDestinyPosition(Position origin, Position destininy)
        {
            if (!Board.GetPiece(origin).CanMoveTo(destininy))
            {
                throw new BoardException("Invalid destiny position!");
            }
        }


        public void initializePieces()
        {
            Board.ToSetPiece(new Tower(Board, Color.Black), new ChessPosition('c', 1).ToPosition());

            Board.ToSetPiece(new King(Board, Color.White), new ChessPosition('c', 8).ToPosition());
            Board.ToSetPiece(new King(Board, Color.White), new ChessPosition('c', 7).ToPosition());
            Board.ToSetPiece(new King(Board, Color.White), new ChessPosition('c', 6).ToPosition());
            Board.ToSetPiece(new King(Board, Color.White), new ChessPosition('c', 5).ToPosition());

        }
    }
}
