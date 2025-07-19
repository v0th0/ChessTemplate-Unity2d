using System.Collections.Generic;
using UnityEngine;

public class Queen : BaseChessPiece
{
    public override List<Vector2Int> GetLegalMoves()
    {
        List<Vector2Int> moves = new List<Vector2Int>();

        AddDirection(moves, 1, 0);  // Right
        AddDirection(moves, -1, 0); // Left
        AddDirection(moves, 0, 1);  // Up
        AddDirection(moves, 0, -1); // Down
        AddDirection(moves, 1, 1);  // Diagonal ↗
        AddDirection(moves, -1, 1); // Diagonal ↖
        AddDirection(moves, 1, -1); // Diagonal ↘
        AddDirection(moves, -1, -1);// Diagonal ↙

        return moves;
    }

    private void AddDirection(List<Vector2Int> moves, int dx, int dy)
    {
        int row = currentRow + dx;
        int col = currentColumn + dy;

        while (row >= 0 && row < 8 && col >= 0 && col < 8)
        {
            GameObject tile = ChessBoardPlacementHandler.Instance.GetTile(row, col);
            if (tile == null) break;

            if (tile.transform.childCount > 0)
            {
                foreach (Transform child in tile.transform)
                {
                    BaseChessPiece other = child.GetComponent<BaseChessPiece>();
                    if (other != null)
                    {
                        if (other.isWhite != isWhite)
                            moves.Add(new Vector2Int(row, col)); // Can capture
                        break; // Stop in any case (blocked)
                    }
                }
                break;
            }

            moves.Add(new Vector2Int(row, col));
            row += dx;
            col += dy;
        }
    }
}
