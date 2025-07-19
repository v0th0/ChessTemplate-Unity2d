using System.Collections.Generic;
using UnityEngine;

public class Rook : BaseChessPiece
{
    public override List<Vector2Int> GetLegalMoves()
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        AddDirection(moves, 1, 0); AddDirection(moves, -1, 0);
        AddDirection(moves, 0, 1); AddDirection(moves, 0, -1);
        return moves;
    }

    private void AddDirection(List<Vector2Int> moves, int dx, int dy)
    {
        int r = currentRow + dx;
        int c = currentColumn + dy;

        while (r >= 0 && r < 8 && c >= 0 && c < 8)
        {
            GameObject tile = ChessBoardPlacementHandler.Instance.GetTile(r, c);
            if (tile == null) break;

            if (tile.transform.childCount > 0)
            {
                var piece = tile.transform.GetChild(0).GetComponent<BaseChessPiece>();
                if (piece != null && piece.isWhite != isWhite)
                    moves.Add(new Vector2Int(r, c));
                break;
            }

            moves.Add(new Vector2Int(r, c));
            r += dx; c += dy;
        }
    }
}
