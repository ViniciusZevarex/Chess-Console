﻿using board;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
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

        public bool Check { get; set; }

        public ChessMatch()
        {
            Board = new Board(8,8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            Pieces = new HashSet<Piece>();
            Captureds = new HashSet<Piece>();

            initializePieces();
        }

        public Piece ToMove(Position origin, Position destiny)
        {
            Piece p = Board.ToRemovePiece(origin);
            p.UpdateAmtMoviments();//update amount moviments of this piece
            Piece capturedPiece = Board.ToRemovePiece(destiny); //to controll the capitured 
            Board.ToSetPiece(p, destiny);

            if (capturedPiece != null)
            {
                Captureds.Add(capturedPiece);
            }

            return capturedPiece;
        }

        public void ToPlay(Position origin, Position destiny)
        {
            ToMove(origin, destiny);

            if (IsInCheck(CurrentPlayer))
            {
                UndoMoviment(origin, destiny);
                throw new BoardException("Você não pode se colocar em xeque");
            }

            if (IsInCheck(Adversary(CurrentPlayer)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (IsInCheck(Adversary(CurrentPlayer)))
            {
                Finished = true;
            }
            else
            {
                Turn++;
                ChangePlyaer();
            }

            
        }

        public void UndoMoviment(Position origin, Position destiny, Piece capturedPiece = null)
        {
            Piece p = Board.ToRemovePiece(destiny);
            p.UpdateDecrementAmtMoviments();

            if (capturedPiece != null)
            {
                Board.ToSetPiece(capturedPiece, destiny);
                Captureds.Remove(capturedPiece);
            }
            Board.ToSetPiece(p,origin);

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

        public HashSet<Piece> InGamePieces(Color color)
        {
            HashSet<Piece> aux = new HashSet<Piece>();

            foreach (Piece item in Pieces)
            {
                if (item.Color == color)
                {
                    aux.Add(item);
                }
            }

            aux.ExceptWith(CapituredPieces(color));

            return aux;
        }

        public Color Adversary(Color color)
        {
            if (color == Color.White)
            {
                return Color.Black;
            }
            else
            {
                return Color.White;
            }
        }

        public Piece King(Color color)
        {
            foreach (Piece x  in InGamePieces(color))
            {
                if (x is King)
                {
                    return x;
                }
            }

            return null;
        }

        public bool IsInCheck(Color color)
        {
            Piece K = King(color);

            if (K == null)
            {
                throw new BoardException("Não tem rei da cor " + color + " no tabuleiro!");
            }

            foreach (Piece item in InGamePieces(Adversary(color)))
            {
                bool[,] mat = item.PossiblesMoviments();

                if (mat[K.Position.Row, K.Position.Column])
                {
                    return true;
                }
            }
            return false;
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
            if (!Board.GetPiece(origin).PossibleMoviment(destiny))
            {
                throw new BoardException("Invalid destiny position!");
            }
        }

        public bool CheckMateTest(Color color)
        {
            if (!IsInCheck(color))
            {
                return false;
            }

            foreach (Piece p in InGamePieces(color))
            {
                bool[,] mat = p.PossiblesMoviments();

                for (int i = 0; i < Board.Rows; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position destiny = new Position(i, j);
                            Piece capturedPiece = ToMove(p.Position,destiny);
                            bool CheckTest = IsInCheck(color);
                            UndoMoviment(p.Position, destiny, capturedPiece);
                            if (!CheckTest)
                            {
                                return false;
                            }
                        }

                    }
                }
            }
            return true;
        }


        public void ToSetNewPiece(char column, int row, Piece piece)
        {
            Board.ToSetPiece(piece, new ChessPosition(column, row).ToPosition());
            Pieces.Add(piece);
        }

        public void initializePieces()
        {
            ToSetNewPiece('a', 1, new Tower(Board, Color.White));
            ToSetNewPiece('b', 1, new Horse(Board, Color.White));
            ToSetNewPiece('c', 1, new Bishop(Board, Color.White));
            ToSetNewPiece('d', 1, new Queen(Board, Color.White));
            ToSetNewPiece('e', 1, new King(Board, Color.White));
            ToSetNewPiece('f', 1, new Bishop(Board, Color.White));
            ToSetNewPiece('g', 1, new Horse(Board, Color.White));
            ToSetNewPiece('h', 1, new Tower(Board, Color.White));
            ToSetNewPiece('a', 2, new Pawn(Board, Color.White));
            ToSetNewPiece('b', 2, new Pawn(Board, Color.White));
            ToSetNewPiece('c', 2, new Pawn(Board, Color.White));
            ToSetNewPiece('d', 2, new Pawn(Board, Color.White));
            ToSetNewPiece('e', 2, new Pawn(Board, Color.White));
            ToSetNewPiece('f', 2, new Pawn(Board, Color.White));
            ToSetNewPiece('g', 2, new Pawn(Board, Color.White));
            ToSetNewPiece('h', 2, new Pawn(Board, Color.White));



            ToSetNewPiece('a', 8, new Tower(Board, Color.Black));
            ToSetNewPiece('b', 8, new Horse(Board, Color.Black));
            ToSetNewPiece('c', 8, new Bishop(Board, Color.Black));
            ToSetNewPiece('d', 8, new Queen(Board, Color.Black));
            ToSetNewPiece('e', 8, new King(Board, Color.Black));
            ToSetNewPiece('f', 8, new Bishop(Board, Color.Black));
            ToSetNewPiece('g', 8, new Horse(Board, Color.Black));
            ToSetNewPiece('h', 8, new Tower(Board, Color.Black));
            ToSetNewPiece('a', 7, new Pawn(Board, Color.Black));
            ToSetNewPiece('b', 7, new Pawn(Board, Color.Black));
            ToSetNewPiece('c', 7, new Pawn(Board, Color.Black));
            ToSetNewPiece('d', 7, new Pawn(Board, Color.Black));
            ToSetNewPiece('e', 7, new Pawn(Board, Color.Black));
            ToSetNewPiece('f', 7, new Pawn(Board, Color.Black));
            ToSetNewPiece('g', 7, new Pawn(Board, Color.Black));
            ToSetNewPiece('h', 7, new Pawn(Board, Color.Black));

        }
    }
}
