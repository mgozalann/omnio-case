using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public event Action<BaseUnit, Tile> OnDropAction;
    public event Action<BaseUnit> OnClickAction;
    
    private static InputHandler _instance;
    public static InputHandler Instance => _instance;
    
    [SerializeField] private float _unitMoveSpeed;

    private Vector3 _initialMousePosition; 

    private BaseUnit _draggedUnit;
    private Camera _camera;
   
    private bool _isDragging = false;
    private bool _hasMoved;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        _camera = Camera.main;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!_isDragging)
            {
                _initialMousePosition = Input.mousePosition;
                _hasMoved = false;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopDragging();
        }

        if (Input.GetMouseButton(0))
        {
            if (_isDragging)
            {
                MoveObjectWithMouse();
            }
            else if (Vector3.Distance(Input.mousePosition, _initialMousePosition) > 0.1f)
            {
                StartDragging();
                _hasMoved = true;
            }
        }
    }

    private Tile GetTilePosByMousePos()
    {
        Vector3 mouseWorldPosition = _camera.ScreenToWorldPoint(Input.mousePosition);

        Vector2Int tileCoordinate = GetTileCoordinateFromPosition(mouseWorldPosition);

        Tile tile = BoardManager.Instance.GetTileAtPosition(tileCoordinate);

        return tile;
    }
    private void StartDragging()
    {
        Tile tile = GetTilePosByMousePos();
        
        if (tile != null && !tile.IsEmpty())
        {
            _draggedUnit = tile.GetUnit();
            _draggedUnit.GetCurrentTile().RemoveUnit();
            _isDragging = true;
        }
    }

    private void StopDragging()
    {

        Tile tile = GetTilePosByMousePos();
        
        if (_hasMoved)
        {
            if (_draggedUnit == null) return;

            if (tile != null)
            {
                OnDropAction?.Invoke(_draggedUnit, tile);
            }
            else
            {
                _draggedUnit.GetCurrentTile().SetUnit(_draggedUnit);
                _draggedUnit.SetCurrentTile(_draggedUnit.GetCurrentTile());
            }
        }
        else
        {

            if (tile != null && !tile.IsEmpty())
            {
                BaseUnit baseUnit = tile.GetUnit();

                if (baseUnit is Generator) //üretim için
                {
                    OnClickAction?.Invoke(baseUnit);
                }
            }
        }
        
        _draggedUnit = null;
        _isDragging = false;
    }

    private void MoveObjectWithMouse()
    {
        Vector3 mousePosition = _camera.ScreenToWorldPoint(Input.mousePosition);
        _draggedUnit.transform.position = Vector3.Lerp(_draggedUnit.transform.position,
            new Vector3(mousePosition.x, mousePosition.y, transform.position.z), _unitMoveSpeed * Time.deltaTime);
    }

    private Vector2Int GetTileCoordinateFromPosition(Vector3 position)
    {
        int x = Mathf.RoundToInt(position.x);
        int y = Mathf.RoundToInt(position.y);

        return new Vector2Int(x, y);
    }
}