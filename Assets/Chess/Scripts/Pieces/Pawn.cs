using System.Collections.Generic;
using UnityEngine;

public class Pawn : BaseChessPiece
{
    public override List<Vector2Int> GetLegalMoves()
    {
        List<Vector2Int> moves = new List<Vector2Int>();
        int dir = isWhite ? -1 : 1;

        // Move forward
        int forwardRow = currentRow + dir;
        if (IsTileEmpty(forwardRow, currentColumn))
            moves.Add(new Vector2Int(forwardRow, currentColumn));

        // Diagonal captures
        if (CanCapture(forwardRow, currentColumn - 1))
            moves.Add(new Vector2Int(forwardRow, currentColumn - 1));

        if (CanCapture(forwardRow, currentColumn + 1))
            moves.Add(new Vector2Int(forwardRow, currentColumn + 1));

        return moves;
    }

    private bool IsTileEmpty(int row, int col)
    {
        if (row < 0 || row >= 8 || col < 0 || col >= 8) return false;
        var tile = ChessBoardPlacementHandler.Instance.GetTile(row, col);
        return tile != null && tile.transform.childCount == 0;
    }

    private bool CanCapture(int row, int col)
    {
        if (row < 0 || row >= 8 || col < 0 || col >= 8) return false;
        var tile = ChessBoardPlacementHandler.Instance.GetTile(row, col);
        if (tile == null || tile.transform.childCount == 0) return false;

        var piece = tile.transform.GetChild(0).GetComponent<BaseChessPiece>();
        return piece != null && piece.isWhite != isWhite;
    }
}
