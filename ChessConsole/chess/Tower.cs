using board;

namespace chess
{
    class Tower : Piece
    {
        public Tower(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "T";
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

            //up
            pos.SetPosition(Position.Row - 1, Position.Column);

            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;

                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }

                pos.Row = pos.Row - 1;
            }

            //down
            pos.SetPosition(Position.Row + 1, Position.Column);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;

                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }

                pos.Row = pos.Row + 1;
            }

            //right
            pos.SetPosition(Position.Row, Position.Column + 1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;

                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }

                pos.Column = pos.Column + 1;
            }

            //left
            pos.SetPosition(Position.Row, Position.Column -1);
            while (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;

                if (Board.GetPiece(pos) != null && Board.GetPiece(pos).Color != Color)
                {
                    break;
                }

                pos.Column = pos.Column - 1;
            }

            return mat;
        }
    }
}
