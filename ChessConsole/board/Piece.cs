using System.Reflection.Emit;

namespace board
{
    abstract class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; set; }
        public int AmtMoviments { get; protected set; }
        public Board Board { get; set; }

        public Piece(Board board, Color color)
        {
            Position = null;
            Color = color;
            AmtMoviments = 0;
            Board = board;
        }

        public void UpdateAmtMoviments()
        {
            AmtMoviments++;
        }

        public void UpdateDecrementAmtMoviments()
        {
            AmtMoviments--;
        }

        public bool ThereArePossiblesMoviments()
        {
            bool[,] mat = PossiblesMoviments();

            for (int i = 0; i < Board.Rows; i++)
            {
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (mat[i,j])
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool CanMoveTo(Position pos)
        {
            return PossiblesMoviments()[pos.Row, pos.Column];
        }

        public abstract bool[,] PossiblesMoviments();

    }
}
