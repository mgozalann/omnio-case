using System.Collections.Generic;
using UnityEngine;

public class BoardManager : Singleton<BoardManager>
{
    [SerializeField] private int _width, _height;
    
    [SerializeField] private Tile _tilePrefab;
    
    [SerializeField] private Transform _cam;

    private Dictionary<Vector2Int, Tile> _tiles = new Dictionary<Vector2Int, Tile>();
    
    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        _tiles = new Dictionary<Vector2Int, Tile>();
        
        for (int i = 0; i < _width; i++)
        {
            for (int j = 0; j < _height; j++)
            {
                Tile spawnedTile = Instantiate(_tilePrefab, new Vector3(i, j), Quaternion.identity,
                    this.gameObject.transform);
                
                spawnedTile.name = $"Tile {i} {j}";

                _tiles[new Vector2Int(i, j)] = spawnedTile;
            }
        }
        _cam.transform.position = new Vector3((float) _width / 2 - .5f, (float) _height / 2 - .5f, -10);
    }

    public Tile GetTileAtPosition(Vector2Int pos)
    {
        if (_tiles.TryGetValue(pos, out Tile tile))
        {
            return tile;
        }

        return null;
    }
    
    public Dictionary<Vector2Int, Tile> GetAllTiles()
    {
        return _tiles;
    }
}