using System.Collections.Generic;
using UnityEngine;

public class Knight : BaseChessPiece
{
    public override List<Vector2Int> GetLegalMoves()
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        int[,] offsets = {
            { 2, 1 }, { 1, 2 }, { -1, 2 }, { -2, 1 },
            { -2, -1 }, { -1, -2 }, { 1, -2 }, { 2, -1 }
        };

        for (int i = 0; i < offsets.GetLength(0); i++)
        {
            int r = currentRow + offsets[i, 0];
            int c = currentColumn + offsets[i, 1];

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

        return moves;
    }
}
