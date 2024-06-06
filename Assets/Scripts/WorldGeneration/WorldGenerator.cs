
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour
{
    [SerializeField] private List<Room> _rooms;
    [SerializeField] private int _maxRooms = 5;

    private int _numRooms = 1;
    private int _numPotentialRooms = 0;
    private bool[,] _grid;
    private Vector2Int _startPosition;

    private float _widthRoom = 36;
    private float _lenghtRoom = 28;
    
    private void Start()
    {
        _grid = new bool[_maxRooms, _maxRooms];
        _startPosition = new Vector2Int(_maxRooms / 2, _maxRooms / 2);

        Room room = BuilRoom(_startPosition);

        RandomNextRoom(_startPosition, room);
    }

    private void Generate(Vector2Int coord, Room parent)
    {
        if (_numRooms >= _maxRooms) { return; }

        _numRooms++;
        Room curRoom = BuilRoom(coord);
        OpenDoors(curRoom, parent);

        RandomNextRoom(coord, curRoom);
    }

    private void RandomNextRoom(Vector2Int coord, Room curRoom)
    {
        List<Vector2Int> nearEmptyCells = GetNearEmptyCells(coord.x, coord.y);
        _numPotentialRooms += nearEmptyCells.Count;

        foreach (var cell in nearEmptyCells)
        {
            _numPotentialRooms--;
            if (Random.Range(0, 2) != 0)
            {
                Generate(cell, curRoom);
            }
        }

        if (_numPotentialRooms == 0 && _numRooms < _maxRooms)
        {
            RandomNextRoom(coord, curRoom);
        }
    }

    private Room BuilRoom(Vector2Int coord)
    {
        _grid[coord.x, coord.y] = true;

        float x = (coord.y - _maxRooms / 2) * _lenghtRoom;
        float z = (coord.x - _maxRooms / 2) * _widthRoom;

        Vector3 roomPos = new Vector3(x, 0, z);

        return Instantiate(_rooms[Random.Range(0, _rooms.Count)], roomPos, Quaternion.identity).GetComponent<Room>();
    }

    private void OpenDoors(Room room, Room parentRoom)
    {
        if (room.gameObject.transform.position.z < parentRoom.gameObject.transform.position.z)
        {
            room.OpenEastDoor();
            parentRoom.OpenWestDoor();
        }
        else if (room.gameObject.transform.position.z > parentRoom.gameObject.transform.position.z)
        {
            room.OpenWestDoor();
            parentRoom.OpenEastDoor();
        }
        else if (room.gameObject.transform.position.x < parentRoom.gameObject.transform.position.x)
        {
            room.OpenSouthDoor();
            parentRoom.OpenNorthDoor();
        }
        else if (room.gameObject.transform.position.x > parentRoom.gameObject.transform.position.x)
        {
            room.OpenNorthDoor();
            parentRoom.OpenSouthDoor();
        }
    }

    private List<Vector2Int> GetNearEmptyCells(int x, int y)
    {
        List<Vector2Int> cells = new List<Vector2Int>();

        if (x > 0 && x < _grid.GetLength(0) - 1)
        {
            if (_grid[x - 1, y] == false) { cells.Add(new Vector2Int(x - 1, y)); }
            if (_grid[x + 1, y] == false) { cells.Add(new Vector2Int(x + 1, y)); }
        }
        
        if (y > 0 && y < _grid.GetLength(1) - 1)
        {
            if (_grid[x, y - 1] == false) { cells.Add(new Vector2Int(x, y - 1)); }
            if (_grid[x, y + 1] == false) { cells.Add(new Vector2Int(x, y + 1)); }
        }

        return cells;
    }
}
