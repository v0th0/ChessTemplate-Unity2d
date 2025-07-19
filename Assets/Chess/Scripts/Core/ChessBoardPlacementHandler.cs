using System;
using UnityEngine;

public sealed class ChessBoardPlacementHandler : MonoBehaviour
{
    [SerializeField] private GameObject[] _rowsArray;       // 8 rows
    [SerializeField] private GameObject _highlightPrefab;   // Highlight prefab (green/red)

    private GameObject[,] _chessBoard;                      // 8x8 tile array
    internal static ChessBoardPlacementHandler Instance;

    private void Awake()
    {
        Instance = this;
        GenerateArray();
    }

    private void GenerateArray()
    {
        _chessBoard = new GameObject[8, 8];
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                _chessBoard[i, j] = _rowsArray[i].transform.GetChild(j).gameObject;
            }
        }
    }

    internal GameObject GetTile(int i, int j)
    {
        try
        {
            return _chessBoard[i, j];
        }
        catch (Exception)
        {
            Debug.LogError($"Invalid tile request at ({i}, {j})");
            return null;
        }
    }

    internal void Highlight(int row, int col)
    {
        Transform tile = GetTile(row, col)?.transform;
        if (tile == null) return;

        Instantiate(_highlightPrefab, tile.position, Quaternion.identity, tile);
    }

    internal void Highlight(int row, int col, Color color)
    {
        Transform tile = GetTile(row, col)?.transform;
        if (tile == null) return;

        GameObject highlight = Instantiate(_highlightPrefab, tile.position, Quaternion.identity, tile);
        SpriteRenderer renderer = highlight.GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            renderer.color = color;
        }
    }

    internal void ClearHighlights()
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                GameObject tile = GetTile(i, j);
                if (tile == null) continue;

                for (int k = tile.transform.childCount - 1; k >= 0; k--)
                {
                    Transform child = tile.transform.GetChild(k);
                    if (child.CompareTag("Highlight"))
                        Destroy(child.gameObject);
                }
            }
        }
    }
}
