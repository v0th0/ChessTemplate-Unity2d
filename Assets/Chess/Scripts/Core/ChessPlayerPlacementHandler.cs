using UnityEngine;

public class ChessPlayerPlacementHandler : MonoBehaviour
{
    [SerializeField] public int row;
    [SerializeField] public int column;

    private void Start()
    {
        GameObject tile = ChessBoardPlacementHandler.Instance.GetTile(row, column);
        if (tile != null)
        {
            transform.position = tile.transform.position;
            transform.position = new Vector3(transform.position.x, transform.position.y, -1f); // Ensure visible
        }
    }
}
