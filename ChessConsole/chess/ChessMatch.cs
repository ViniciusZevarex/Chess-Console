using board;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.PortableExecutable;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
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
        public Piece CanPassant { get; private set; }

        public bool Check { get; set; }

        public ChessMatch()
        {
            Board = new Board(8,8);
            Turn = 1;
            CurrentPlayer = Color.White;
            Finished = false;
            Check = false;
            CanPassant = null;
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

            //#Special Moviment Small Rock
            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position originTower = new Position(origin.Row, origin.Column + 3);
                Position destinyTower = new Position(origin.Row, origin.Column + 1);
                Piece T = Board.ToRemovePiece(originTower);
                T.UpdateAmtMoviments();
                Board.ToSetPiece(T, destinyTower);

            }

            //#Special Moviment Small Rock
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originTower = new Position(origin.Row, origin.Column - 4);
                Position destinyTower = new Position(origin.Row, origin.Column - 1);
                Piece T = Board.ToRemovePiece(originTower);
                T.UpdateAmtMoviments();
                Board.ToSetPiece(T, destinyTower);

            }

            //#Special Moviment En Passant
            if (p is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPiece == null)
                {
                    Position posP;

                    if (p.Color == Color.White)
                    {
                        posP = new Position(destiny.Row + 1, destiny.Column);
                    }
                    else
                    {
                        posP = new Position(destiny.Row - 1, destiny.Column);
                    }
                    capturedPiece = Board.ToRemovePiece(posP);
                    Captureds.Add(capturedPiece);
                }
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

            Piece p = Board.GetPiece(destiny);

            //#Special Moviment: promotion
            if (p is Pawn)
            {
                if ((p.Color == Color.White && destiny.Row == 0) || (p.Color == Color.Black && destiny.Row == 7))
                {
                    p = Board.ToRemovePiece(destiny);
                    Pieces.Remove(p);
                    Piece queen = new Queen(Board, p.Color);
                    Board.ToSetPiece(queen, destiny);
                    Pieces.Add(queen);
                }
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

            

            //#special moviment en passant
            if (p is Pawn && (destiny.Row == origin.Row - 2) || (destiny.Row == origin.Row + 2))
            {
                CanPassant = p;
            }
            else
            {
                CanPassant = null;
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


            //#Special Moviment Small Rock
            if (p is King && destiny.Column == origin.Column + 2)
            {
                Position originTower = new Position(origin.Row, origin.Column + 3);
                Position destinyTower = new Position(origin.Row, origin.Column + 1);
                Piece T = Board.ToRemovePiece(destinyTower);
                T.UpdateDecrementAmtMoviments();
                Board.ToSetPiece(T, originTower);

            }

            //#Special Moviment Small Rock
            if (p is King && destiny.Column == origin.Column - 2)
            {
                Position originTower = new Position(origin.Row, origin.Column - 4);
                Position destinyTower = new Position(origin.Row, origin.Column - 1);
                Piece T = Board.ToRemovePiece(destinyTower);
                T.UpdateAmtMoviments();
                Board.ToSetPiece(T, originTower);
            }

            //#Special Moviment En Passant
            if (p is Pawn)
            {
                if (origin.Column != destiny.Column && capturedPiece == CanPassant)
                {
                    Piece pawn = Board.ToRemovePiece(destiny);
                    Position posP;
                    if (p.Color == Color.White)
                    {
                        posP = new Position(3, destiny.Column);
                    }
                    else
                    {
                        posP = new Position(4, destiny.Column);
                    }

                    Board.ToSetPiece(pawn, posP);
                }
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
                throw new BoardException("Não existe peça na posição de origem escolhida!");
            }
            if (CurrentPlayer != Board.GetPiece(pos).Color)
            {
                throw new BoardException("A peça escolhida não é sua!");
            }
            if (!Board.GetPiece(pos).ThereArePossiblesMoviments())
            {
                throw new BoardException("Não há movimentos possíveis para a peça escolhida!");
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
                throw new BoardException("Posição de destino inválida!");
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
            //White
            ToSetNewPiece('a', 1, new Tower(Board, Color.White));
            ToSetNewPiece('b', 1, new Horse(Board, Color.White));
            ToSetNewPiece('c', 1, new Bishop(Board, Color.White));
            ToSetNewPiece('d', 1, new Queen(Board, Color.White));
            ToSetNewPiece('e', 1, new King(Board, Color.White,this));
            ToSetNewPiece('f', 1, new Bishop(Board, Color.White));
            ToSetNewPiece('g', 1, new Horse(Board, Color.White));
            ToSetNewPiece('h', 1, new Tower(Board, Color.White));
            ToSetNewPiece('a', 2, new Pawn(Board, Color.White, this));
            ToSetNewPiece('b', 2, new Pawn(Board, Color.White, this));
            ToSetNewPiece('c', 2, new Pawn(Board, Color.White, this));
            ToSetNewPiece('d', 2, new Pawn(Board, Color.White, this));
            ToSetNewPiece('e', 2, new Pawn(Board, Color.White, this));
            ToSetNewPiece('f', 2, new Pawn(Board, Color.White, this));
            ToSetNewPiece('g', 2, new Pawn(Board, Color.White, this));
            ToSetNewPiece('h', 2, new Pawn(Board, Color.White, this));



            //Black
            ToSetNewPiece('a', 8, new Tower(Board, Color.Black));
            ToSetNewPiece('b', 8, new Horse(Board, Color.Black));
            ToSetNewPiece('c', 8, new Bishop(Board, Color.Black));
            ToSetNewPiece('d', 8, new Queen(Board, Color.Black));
            ToSetNewPiece('e', 8, new King(Board, Color.Black, this));
            ToSetNewPiece('f', 8, new Bishop(Board, Color.Black));
            ToSetNewPiece('g', 8, new Horse(Board, Color.Black));
            ToSetNewPiece('h', 8, new Tower(Board, Color.Black));
            ToSetNewPiece('a', 7, new Pawn(Board, Color.Black, this));
            ToSetNewPiece('b', 7, new Pawn(Board, Color.Black, this));
            ToSetNewPiece('c', 7, new Pawn(Board, Color.Black, this));
            ToSetNewPiece('d', 7, new Pawn(Board, Color.Black, this));
            ToSetNewPiece('e', 7, new Pawn(Board, Color.Black, this));
            ToSetNewPiece('f', 7, new Pawn(Board, Color.Black, this));
            ToSetNewPiece('g', 7, new Pawn(Board, Color.Black, this));
            ToSetNewPiece('h', 7, new Pawn(Board, Color.Black, this));

        }
    }
}
