
using System.Collections.Generic;
using UnityEngine;

public class WorldGeneration2 : MonoBehaviour
{
    [SerializeField] private Room _startingRoom;
    [SerializeField] private List<Room> _rooms;
    [SerializeField] private int _maxRooms = 10;

    [SerializeField] private GameObject _xHallway;
    [SerializeField] private GameObject _angleHallway;
    [SerializeField] private GameObject _tHallway;
    [SerializeField] private GameObject _straightHallway;

    [SerializeField] private float _lenghtWall = 36;

    private int _heightGrid;
    private int _widthGrid;

    private Dictionary<Vector2Int, Room> _dictBuiltRooms = new Dictionary<Vector2Int, Room>();
    
    private GridMap _gridMap;

    private void Start()
    {
        _heightGrid = _maxRooms;
        _widthGrid = _maxRooms;

        _gridMap = new GridMap(_heightGrid, _widthGrid);

        Room startingRoom = BuildRoom(_heightGrid / 2, _widthGrid / 2, _startingRoom);
        startingRoom.AddConnection(startingRoom);

        RandomRooms();
        
        ConnectRooms();
    }

    private void RandomRooms()
    {
        while (_dictBuiltRooms.Count < _maxRooms)
        {
            int x = Random.Range(0, _gridMap.Map.GetLength(0));
            int z = Random.Range(0, _gridMap.Map.GetLength(1));

            // CheckBug
            //x = CheckBug.GetMap()[_dictBuiltRooms.Count].x;
            //z = CheckBug.GetMap()[_dictBuiltRooms.Count].y;

            if (_gridMap.Map[x, z] is VoidHollway)
            {
                BuildRoom(x, z, _rooms[Random.Range(0, _rooms.Count)]);
            }
        }
    }

    private Room BuildRoom(int x, int z, Room room)
    {
        Vector3 pos = new Vector3(x - _heightGrid / 2, 0, z - _widthGrid / 2) * _lenghtWall;
        Room newRoom = Instantiate(room, pos, Quaternion.identity);

        newRoom.Init(new Vector2Int(x, z), _gridMap.AddRoom(x, z));
        _dictBuiltRooms.Add(newRoom.Coord, newRoom);

        return newRoom;
    }

    private void ConnectRooms()
    {
        foreach (Room room in _dictBuiltRooms.Values)
        {
            Room connectionRoom = SearchNearest(room);

            PaveWay(room, connectionRoom);

            room.AddConnection(connectionRoom);
            connectionRoom.AddConnection(room);
        }

        BuildHallways();
    }

    private Room SearchNearest(Room room)
    {
        float minDist = float.MaxValue;
        Room nearestRoom = room;

        foreach (Room connectRoom in _dictBuiltRooms.Values)
        {
            if (room != connectRoom && !room.IsConnectionWith(connectRoom) && (room.InSystem || connectRoom.InSystem))
            {
                if (Vector2.Distance(room.Coord, connectRoom.Coord) < minDist)
                {
                    minDist = Vector2.Distance(room.Coord, connectRoom.Coord);
                    nearestRoom = connectRoom;
                }
            }
        }

        return nearestRoom;
    }

    private void PaveWay(Room room, Room connectionRoom)
    {
        if (room.Coord.x < connectionRoom.Coord.x)
        {
            room.OpenSouthDoor();
            for (int i = room.Coord.x + 1; i < connectionRoom.Coord.x; i++)
            {
                VerticalStraight(i, room.Coord.y);
            }
        }
        else if (room.Coord.x > connectionRoom.Coord.x)
        {
            room.OpenNorthDoor();
            for (int i = room.Coord.x - 1; i > connectionRoom.Coord.x; i--)
            {
                VerticalStraight(i, room.Coord.y);
            }
        }
        else
        {
            if (room.Coord.y < connectionRoom.Coord.y)
            {
                room.OpenEastDoor();
            }
            else
            {
                room.OpenWestDoor();
            }
        }

        if (room.Coord.y < connectionRoom.Coord.y)
        {
            connectionRoom.OpenWestDoor();
            for (int i = room.Coord.y + 1; i < connectionRoom.Coord.y; i++)
            {
                HorizontalStraight(connectionRoom.Coord.x, i);
            }
        }
        else if (room.Coord.y > connectionRoom.Coord.y)
        {
            connectionRoom.OpenEastDoor();
            for (int i = room.Coord.y - 1; i > connectionRoom.Coord.y; i--)
            {
                HorizontalStraight(connectionRoom.Coord.x, i);
            }
        }
        else
        {
            if (room.Coord.x < connectionRoom.Coord.x)
            {
                connectionRoom.OpenNorthDoor();
            }
            else
            {
                connectionRoom.OpenSouthDoor();
            }
        }
        //_gridMap.AddConnection(connectionRoom.Coord.x, room.Coord.y);
        AngleHallway(room.Coord, connectionRoom.Coord);
    }

    private void VerticalStraight(int x, int y)
    {
        try
        {
            _gridMap.AddVerticalStraightHallWay(x, y);
        }
        catch
        {
            _dictBuiltRooms[new Vector2Int(x, y)].OpenNorthDoor();
            _dictBuiltRooms[new Vector2Int(x, y)].OpenSouthDoor();
        }
    }

    private void HorizontalStraight(int x, int y)
    {
        try
        {
            _gridMap.AddHorizontalStraightHallWay(x, y);
        }
        catch
        {
            _dictBuiltRooms[new Vector2Int(x, y)].OpenWestDoor();
            _dictBuiltRooms[new Vector2Int(x, y)].OpenEastDoor();
        }
    }

    private void AngleHallway(Vector2Int room, Vector2Int connectionRoom)
    {
        try
        {
            _gridMap.AddConnection(connectionRoom.x, room.y);
        }
        catch
        {
            if (room.x != connectionRoom.x)
                if (room.x > connectionRoom.x)
                    _dictBuiltRooms[new Vector2Int(connectionRoom.x, room.y)].OpenSouthDoor();
                else
                    _dictBuiltRooms[new Vector2Int(connectionRoom.x, room.y)].OpenNorthDoor();
            if (room.y != connectionRoom.y)
                if (room.y > connectionRoom.y)
                    _dictBuiltRooms[new Vector2Int(connectionRoom.x, room.y)].OpenWestDoor();
                else
                    _dictBuiltRooms[new Vector2Int(connectionRoom.x, room.y)].OpenEastDoor();
        }
    }

    private void BuildHallways()
    {
        Hallway[,] map = _gridMap.Map;

        string s = "";
        foreach (var i in _dictBuiltRooms.Keys) { s += $"new Vector2Int{i}, "; }
        Debug.Log(s);
        
        
        for (int x = 0; x < map.GetLength(0); x++)
        {
            for (int z = 0; z < map.GetLength(1); z++)
            {
                switch (map[x, z].Name)
                {
                    case NamesHallway.VerticalStraight:
                        Instantiate(_straightHallway, new Vector3(x - _heightGrid / 2, 0, z - _widthGrid / 2) * _lenghtWall, Quaternion.Euler(new Vector3(0, 90, 0)));
                        break;

                    case NamesHallway.HorizontalStraight:
                        Instantiate(_straightHallway, new Vector3(x - _heightGrid / 2, 0, z - _widthGrid / 2) * _lenghtWall, Quaternion.identity);
                        break;

                    case NamesHallway.Crossroad:
                        Instantiate(_xHallway, new Vector3(x - _heightGrid / 2, 0, z - _widthGrid / 2) * _lenghtWall, Quaternion.identity);
                        break;

                    case NamesHallway.AngleUpRight:
                        Instantiate(_angleHallway, new Vector3(x - _heightGrid / 2, 0, z - _widthGrid / 2) * _lenghtWall, Quaternion.Euler(new Vector3(0, 180, 0)));
                        break;

                    case NamesHallway.AngleUpLeft:
                        Instantiate(_angleHallway, new Vector3(x - _heightGrid / 2, 0, z - _widthGrid / 2) * _lenghtWall, Quaternion.Euler(new Vector3(0, 90, 0)));
                        break;

                    case NamesHallway.AngleDownRight:
                        Instantiate(_angleHallway, new Vector3(x - _heightGrid / 2, 0, z - _widthGrid / 2) * _lenghtWall, Quaternion.Euler(new Vector3(0, -90, 0)));
                        break;

                    case NamesHallway.AngleDownLeft:
                        Instantiate(_angleHallway, new Vector3(x - _heightGrid / 2, 0, z - _widthGrid / 2) * _lenghtWall, Quaternion.identity);
                        break;

                    case NamesHallway.TRight:
                        Instantiate(_tHallway, new Vector3(x - _heightGrid / 2, 0, z - _widthGrid / 2) * _lenghtWall, Quaternion.Euler(new Vector3(0, -90, 0)));
                        break;

                    case NamesHallway.TLeft:
                        Instantiate(_tHallway, new Vector3(x - _heightGrid / 2, 0, z - _widthGrid / 2) * _lenghtWall, Quaternion.Euler(new Vector3(0, 90, 0)));
                        break;

                    case NamesHallway.TUp:
                        Instantiate(_tHallway, new Vector3(x - _heightGrid / 2, 0, z - _widthGrid / 2) * _lenghtWall, Quaternion.Euler(new Vector3(0, 180, 0)));
                        break;

                    case NamesHallway.TDown:
                        Instantiate(_tHallway, new Vector3(x - _heightGrid / 2, 0, z - _widthGrid / 2) * _lenghtWall, Quaternion.identity);
                        break;

                }
            }
        }
    }
}


public static class CheckBug
{
    private static List<Vector2Int> _map = new List<Vector2Int>()
    {
        new Vector2Int(3, 3), new Vector2Int(1, 3), new Vector2Int(0, 4), new Vector2Int(0, 1), new Vector2Int(0, 0), new Vector2Int(3, 0), new Vector2Int(5, 0)
    };

    public static List<Vector2Int> GetMap()
    {
        return _map;
    }
}
