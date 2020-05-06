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

        public abstract bool[,] PossiblesMoviments();

    }
}
