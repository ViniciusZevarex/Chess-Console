using board;

namespace chess
{
    class Horse : Piece
    {
        public Horse(Board board, Color color) : base(board, color) { }

        public override string ToString()
        {
            return "H";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.GetPiece(pos);

            return p == null || p.Color != this.Color;
        }

        public override bool[,] PossiblesMoviments()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];

            Position pos = new Position(0, 0);

            pos.SetPosition(Position.Row - 1, Position.Column - 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.SetPosition(Position.Row - 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.SetPosition(Position.Row - 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }


            pos.SetPosition(Position.Row - 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.SetPosition(Position.Row + 1, Position.Column + 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.SetPosition(Position.Row + 2, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.SetPosition(Position.Row + 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }


            pos.SetPosition(Position.Row + 2, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            pos.SetPosition(Position.Row + 1, Position.Column - 2);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }


            return mat;
        }
    }
}

