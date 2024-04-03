using System;
using System.Linq;
using UnityEngine;

public class Generator : MonoBehaviour
{
    [SerializeField] private GeneratorSOData _generatorSOData;

    private void OnEnable()
    {
        InputController.Instance.OnClickAction += OnClick;
    }

    private void OnDisable()
    {
        InputController.Instance.OnClickAction -= OnClick;
    }

    private void OnClick(Unit obj)
    {
        if (obj.gameObject == gameObject)
        {
            Vector2Int emptyTilePos = GetRandomEmptyTile();

            Tile tile = BoardManager.Instance.GetTileAtPosition(emptyTilePos);

            if (tile != null)
            {
                Unit spawnUnit = _generatorSOData.SelectObject();
                Unit unit = Instantiate(spawnUnit, tile.transform.position, Quaternion.identity);
                unit.SetCurrentTile(tile);
                tile.SetUnit(unit);
            }
        }
    }

    private Vector2Int GetRandomEmptyTile()
    {
        var tiles = BoardManager.Instance.GetAllTiles();
        var shuffledTiles = tiles.OrderBy(x => Guid.NewGuid()).ToDictionary(x => x.Key, x => x.Value);

        foreach (var kvp in shuffledTiles)
        {
            if (kvp.Value.IsEmpty())
            {
                return kvp.Key;
            }
        }

        return new Vector2Int(-1, -1);
    }
}