using UnityEngine;

public class PieceSelector : MonoBehaviour
{
    private BaseChessPiece selectedPiece;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit.collider == null) return;

            Transform clicked = hit.collider.transform;

            if (clicked.CompareTag("Highlight") && selectedPiece != null)
            {
                Transform targetTile = clicked.parent;
                MovePieceToTile(targetTile);
                return;
            }

            BaseChessPiece piece = clicked.GetComponent<BaseChessPiece>();
            if (piece != null)
            {
                selectedPiece = piece;
                selectedPiece.HighlightMoves();
            }
        }
    }

    void MovePieceToTile(Transform targetTile)
    {
        if (selectedPiece == null || targetTile == null) return;

        foreach (Transform child in targetTile)
        {
            BaseChessPiece existingPiece = child.GetComponent<BaseChessPiece>();
            if (existingPiece != null)
            {
                if (existingPiece.isWhite == selectedPiece.isWhite)
                    return; // Can't move onto friendly piece

                Destroy(existingPiece.gameObject); // Capture enemy
                break;
            }
        }

        Vector3 newPos = targetTile.position;
        newPos.z = -1f;
        selectedPiece.transform.position = newPos;

        string[] coords = targetTile.name.Split('_');
        if (coords.Length == 3 &&
            int.TryParse(coords[1], out int row) &&
            int.TryParse(coords[2], out int col))
        {
            selectedPiece.currentRow = row;
            selectedPiece.currentColumn = col;
        }

        ChessBoardPlacementHandler.Instance.ClearHighlights();
        selectedPiece = null;
    }
}
