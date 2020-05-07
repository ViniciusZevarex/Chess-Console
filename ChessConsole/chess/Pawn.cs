using board;
using System.Text.RegularExpressions;

namespace chess
{
    class Pawn : Piece
    {

        private ChessMatch Match;

        public Pawn(Board board, Color color, ChessMatch match) : base(board, color) {
            Match = match;
        }
        public override string ToString()
        {
            return "P";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.GetPiece(pos);

            return p == null || p.Color != this.Color;
        }

        public bool ThereIsAdversary(Position pos)
        {
            Piece p = Board.GetPiece(pos);

            return p != null && p.Color != Color;
        }

        public bool FreePosition(Position pos)
        {
            return Board.GetPiece(pos) == null;
        }

        public override bool[,] PossiblesMoviments()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];

            Position pos = new Position(0, 0);

            if (Color == Color.White)
            {
                pos.SetPosition(Position.Row - 1, Position.Column);
                if (Board.ValidPosition(pos) && FreePosition(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.SetPosition(Position.Row - 2, Position.Column);
                if (Board.ValidPosition(pos) && FreePosition(pos) && AmtMoviments == 0)
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.SetPosition(Position.Row - 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && ThereIsAdversary(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.SetPosition(Position.Row - 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && ThereIsAdversary(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                //#special moviment
                if (Position.Row == 3)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);
                    
                    if (Board.ValidPosition(left) && ThereIsAdversary(left) && Board.GetPiece(left) == Match.CanPassant)
                    {
                        mat[left.Row - 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Row, Position.Column + 1);

                    if (Board.ValidPosition(right) && ThereIsAdversary(right) && Board.GetPiece(right) == Match.CanPassant)
                    {
                        mat[right.Row - 1, right.Column] = true;
                    }
                }


            }
            else
            {
                pos.SetPosition(Position.Row + 1, Position.Column);
                if (Board.ValidPosition(pos) && FreePosition(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.SetPosition(Position.Row + 2, Position.Column);
                if (Board.ValidPosition(pos) && FreePosition(pos) && AmtMoviments == 0)
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.SetPosition(Position.Row + 1, Position.Column - 1);
                if (Board.ValidPosition(pos) && ThereIsAdversary(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }
                pos.SetPosition(Position.Row + 1, Position.Column + 1);
                if (Board.ValidPosition(pos) && ThereIsAdversary(pos))
                {
                    mat[pos.Row, pos.Column] = true;
                }

                //#special moviment
                if (Position.Row == 4)
                {
                    Position left = new Position(Position.Row, Position.Column - 1);

                    if (Board.ValidPosition(left) && ThereIsAdversary(left) && Board.GetPiece(left) == Match.CanPassant)
                    {
                        mat[left.Row + 1, left.Column] = true;
                    }

                    Position right = new Position(Position.Row, Position.Column + 1);

                    if (Board.ValidPosition(right) && ThereIsAdversary(right) && Board.GetPiece(right) == Match.CanPassant)
                    {
                        mat[right.Row + 1, right.Column] = true;
                    }
                }
            }
            

            return mat;
        }
    }
}
