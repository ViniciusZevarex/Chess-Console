namespace board
{
    class Board
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public Piece[,] Pieces;

        public Board(int row, int column)
        {
            Rows = row;
            Columns = column;
            Pieces = new Piece[row, column];
        }

        public Piece GetPiece(int row, int column)
        {
            return Pieces[row, column];
        }

        public Piece GetPiece(Position pos)
        {
            return Pieces[pos.Row, pos.Column];
        }

        public bool IsSetPiece(Position pos)
        {
            ToValidatePosition(pos);
            return GetPiece(pos) != null;
        }

        public void ToSetPiece(Piece p, Position pos)
        {
            p.Position = pos;
            Pieces[pos.Row, pos.Column] = p;
        }

        public bool ValidPosition(Position pos)
        {
            return !(
                pos.Row < 0 
                || pos.Row >= Rows 
                || pos.Column < 0 
                || pos.Column >= Columns
                );
        }

        public void ToValidatePosition(Position pos)
        {
            if (!ValidPosition(pos))
            {
                throw new BoardException("Invalid Position!");
            }
        }
    }
}
