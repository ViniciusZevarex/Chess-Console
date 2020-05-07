using board;

namespace chess
{
    class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "B";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.GetPiece(pos);

            return (p == null || p.Color != this.Color);
        }

        public override bool[,] PossiblesMoviments()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];

            Position pos = new Position(0, 0);

            //up right
            pos.SetPosition(Position.Row - 1, Position.Column + 1);

            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;

                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }

                pos.SetPosition(pos.Row - 1, pos.Column + 1);
            }

            //down right
            pos.SetPosition(Position.Row + 1, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;

                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }

                pos.SetPosition(pos.Row + 1, pos.Column + 1);
            }

            //upleft
            pos.SetPosition(Position.Row - 1, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;

                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }

                pos.SetPosition(pos.Row - 1, pos.Column - 1);
            }

            //down right
            pos.SetPosition(Position.Row + 1, Position.Column - 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;

                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }

                pos.SetPosition(pos.Row + 1, pos.Column - 1);
            }



            return mat;
        }
    }
}
