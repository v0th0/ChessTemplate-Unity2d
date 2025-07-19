using System.Collections.Generic;
using UnityEngine;

public abstract class BaseChessPiece : MonoBehaviour
{
    public int currentRow;
    public int currentColumn;
    public bool isWhite;

    public abstract List<Vector2Int> GetLegalMoves();

    public void HighlightMoves()
    {
        ChessBoardPlacementHandler.Instance.ClearHighlights();

        foreach (Vector2Int move in GetLegalMoves())
        {
            GameObject tile = ChessBoardPlacementHandler.Instance.GetTile(move.x, move.y);
            if (tile == null) continue;

            bool isEnemy = false;

            foreach (Transform child in tile.transform)
            {
                BaseChessPiece other = child.GetComponent<BaseChessPiece>();
                if (other != null)
                {
                    if (other.isWhite == this.isWhite)
                    {
                        tile = null;
                        break;
                    }
                    else
                    {
                        isEnemy = true;
                    }
                }
            }

            if (tile != null)
            {
                if (isEnemy)
                    ChessBoardPlacementHandler.Instance.Highlight(move.x, move.y, Color.red);
                else
                    ChessBoardPlacementHandler.Instance.Highlight(move.x, move.y);
            }
        }
    }
}
