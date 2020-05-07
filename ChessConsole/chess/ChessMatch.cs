using board;
using System;
using System.Collections.Generic;
using System.Threading.Channels;

namespace chess
{
    class ChessMatch
    {
        public Board Board { get; private set; }
        public Color CurrentPlayer { get; private set; }
        public int Turn { get; private set; }
        public bool Finished { get; set; }
        public HashSet<Piece> Pieces { get; set; }
        public HashSet<Piece> Captureds { get; set; }

        public ChessMatch()
        {
            Board = new Board(8,8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Pieces = new HashSet<Piece>();
            Captureds = new HashSet<Piece>();

            initializePieces();
        }

        public void ToMove(Position origin, Position destiny)
        {
            Piece p = Board.ToRemovePiece(origin);
            p.UpdateAmtMoviments();//update amount moviments of this piece
            Piece capturedPiece = Board.ToRemovePiece(destiny); //to controll the capitured 
            Board.ToSetPiece(p, destiny);

            if (capturedPiece != null)
            {
                Captureds.Add(capturedPiece);
            }
        }

        public HashSet<Piece> CapituredPieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach(Piece x in Captureds)
            {
                if (x.Color == color)
                {
                    aux.Add(x);
                }
            }

            return aux;
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

        public void ValidateDestinyPosition(Position origin, Position destiny)
        {
            if (!Board.GetPiece(origin).CanMoveTo(destiny))
            {
                throw new BoardException("Invalid destiny position!");
            }
        }


        public void ToSetNewPiece(char column, int row, Piece piece)
        {
            Board.ToSetPiece(piece, new ChessPosition(column, row).ToPosition());
            Pieces.Add(piece);
        }

        public void initializePieces()
        {
            ToSetNewPiece('c', 1, new Tower(Board, Color.Black));
            ToSetNewPiece('b', 2, new Tower(Board, Color.Black));
            ToSetNewPiece('a', 3, new Tower(Board, Color.Black));

            ToSetNewPiece('d', 8, new King(Board, Color.White));
            ToSetNewPiece('e', 7, new King(Board, Color.White));
            ToSetNewPiece('f', 6, new King(Board, Color.White));
        }
    }
}
