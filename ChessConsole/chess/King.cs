using board;

namespace chess
{
    class King : Piece
    {
       private ChessMatch Match;
       public King(Board board, Color color, ChessMatch chessMatch) : base(board, color){
            Match = chessMatch;
       }

        public override string ToString()
        {
            return "K";
        }

        private bool CanMove(Position pos)
        {
            Piece p = Board.GetPiece(pos);

            return p == null || p.Color != this.Color;
        }

        private bool TestTowerToRock(Position pos)
        {
            Piece p = Board.GetPiece(pos);
            return p != null && p is Tower && p.Color == Color && p.AmtMoviments == 0;
        }

        public override bool[,] PossiblesMoviments()
        {
            bool[,] mat = new bool[Board.Rows, Board.Columns];

            Position pos = new Position(0,0);

            //up
            pos.SetPosition(Position.Row - 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //up right
            pos.SetPosition(Position.Row - 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //up left
            pos.SetPosition(Position.Row - 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //left
            pos.SetPosition(Position.Row, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //right
            pos.SetPosition(Position.Row , Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //down
            pos.SetPosition(Position.Row + 1, Position.Column);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //down right
            pos.SetPosition(Position.Row + 1, Position.Column + 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            //down left
            pos.SetPosition(Position.Row + 1, Position.Column - 1);
            if (Board.ValidPosition(pos) && CanMove(pos))
            {
                mat[pos.Row, pos.Column] = true;
            }

            // #SpecialMoviment: Small Rock
            if (AmtMoviments == 0 && !Match.Check)
            {
                Position posTower1 = new Position(Position.Row, Position.Column + 3);
                if (TestTowerToRock(posTower1)){
                    Position p1 = new Position(Position.Row, Position.Column + 1);
                    Position p2 = new Position(Position.Row, Position.Column + 2);
                    if (Board.GetPiece(p1) == null && Board.GetPiece(p2) == null)
                    {
                        mat[Position.Row, Position.Column + 2] = true;
                    }
                }
            }

            // #SpecialMoviment: Large Rock
            if (AmtMoviments == 0 && !Match.Check)
            {
                Position posTower2 = new Position(Position.Row, Position.Column - 4);
                if (TestTowerToRock(posTower2))
                {
                    Position p1 = new Position(Position.Row, Position.Column - 1);
                    Position p2 = new Position(Position.Row, Position.Column - 2);
                    Position p3 = new Position(Position.Row, Position.Column - 3);
                    if (Board.GetPiece(p1) == null && Board.GetPiece(p2) == null && Board.GetPiece(p3) == null)
                    {
                        mat[Position.Row, Position.Column - 2] = true;
                    }
                }
            }
            return mat;
        }
    }
}
