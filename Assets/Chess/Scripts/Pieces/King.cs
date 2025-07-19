using System.Collections.Generic;
using UnityEngine;

public class King : BaseChessPiece
{
    public override List<Vector2Int> GetLegalMoves()
    {
        List<Vector2Int> moves = new List<Vector2Int>();

        int[] directions = { -1, 0, 1 };
        foreach (int dx in directions)
        {
            foreach (int dy in directions)
            {
                if (dx == 0 && dy == 0) continue;

                int r = currentRow + dx;
                int c = currentColumn + dy;

                if (r < 0 || r >= 8 || c < 0 || c >= 8) continue;

                GameObject tile = ChessBoardPlacementHandler.Instance.GetTile(r, c);
                if (tile == null) continue;

                bool canAdd = true;
                if (tile.transform.childCount > 0)
                {
                    var piece = tile.transform.GetChild(0).GetComponent<BaseChessPiece>();
                    if (piece != null && piece.isWhite == isWhite)
                        canAdd = false;
                }

                if (canAdd)
                    moves.Add(new Vector2Int(r, c));
            }
        }

        return moves;
    }
}
