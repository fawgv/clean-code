using System.Collections.Generic;
using System.Linq;

namespace Chess
{
    public class ChessProblem
    {
        private Board board;
        public ChessStatus ChessStatus;

        public ChessProblem(string[] lines)
        {
            board = new BoardParser().ParseBoard(lines);
        }

        // Определяет мат, шах или пат белым.
        public void CalculateChessStatus()
        {
            var hasMoves = GetHasMoves();
            VerifyResultGame(IsCheckForWhite(), hasMoves);
        }

        private bool GetHasMoves()
        {
            var hasMoves = board.GetPieces(PieceColor.White)
                .Aggregate(false, (current1, locFrom) => GetFromLocationMoves(locFrom)
                    .Aggregate(current1, (current, locTo) => HasMoves(locTo, locFrom, current)));
            return hasMoves;
        }

        private  IEnumerable<Location> GetFromLocationMoves(Location locFrom)
        {
            return board.GetPiece(locFrom).GetMoves(locFrom, board);
        }

        private void VerifyResultGame(bool isCheck, bool hasMoves)
        {
            if (isCheck)
                ChessStatus = hasMoves ? ChessStatus.Check : ChessStatus.Mate;
            else if (hasMoves) ChessStatus = ChessStatus.Ok;
            else ChessStatus = ChessStatus.Stalemate;
        }

        private bool HasMoves(Location locTo, Location locFrom, bool hasMoves)
        {
            var old = board.GetPiece(locTo);
            board.Set(locTo, board.GetPiece(locFrom));
            board.Set(locFrom, null);
            if (!IsCheckForWhite())
                hasMoves = true;
            board.Set(locFrom, board.GetPiece(locTo));
            board.Set(locTo, old);
            return hasMoves;
        }

        // check — это шах
        private bool IsCheckForWhite()
        {
            var isCheck = false;
            foreach (var loc in board.GetPieces(PieceColor.Black))
            {
                var piece = board.GetPiece(loc);
                var moves = piece.GetMoves(loc, board);
                isCheck = moves.Aggregate(isCheck, (current, destination) => IsCheckWhiteKing(destination, current));
            }
            return isCheck;
        }

        private bool IsCheckWhiteKing(Location destination, bool isCheck)
        {
            if (Piece.Is(board.GetPiece(destination),
                PieceColor.White, PieceType.King))
                isCheck = true;
            return isCheck;
        }
    }
}