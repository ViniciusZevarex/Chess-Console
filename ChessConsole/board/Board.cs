namespace board
{
    class Board
    {
        public int Row { get; set; }
        public int Column { get; set; }
        public Piece[,] Pieces;

        public Board(int row, int column, Piece[,] pieces)
        {
            Row = row;
            Column = column;
            Pieces = new Piece[row, column];
        }
    }
}
