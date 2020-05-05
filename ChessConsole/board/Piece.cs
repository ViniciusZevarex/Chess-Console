using System.Reflection.Emit;

namespace board
{
    class Piece
    {
        public Position Position { get; set; }
        public Color Color { get; set; }
        public int AmtMoviments { get; protected set; }
        public Board Board { get; set; }

        public Piece(Position position, Color color, Board board)
        {
            Position = position;
            Color = color;
            AmtMoviments = 0;
            Board = board;
        }
    }
}
