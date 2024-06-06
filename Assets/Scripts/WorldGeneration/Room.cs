
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private GameObject _northDoor;
    [SerializeField] private GameObject _southDoor;
    [SerializeField] private GameObject _westDoor;
    [SerializeField] private GameObject _easthDoor;

    private Vector2Int _coord;
    private RoomHallway _roomHallway;
    public Vector2Int Coord => _coord;
    private List<Room> _connections = new List<Room>();

    private bool _inSystem = false;
    public bool InSystem => _inSystem;

    public void Init(Vector2Int coord, RoomHallway room)
    {
        _coord = coord;
        _roomHallway = room;
    }

    public void AddConnection(Room room)
    {
        _connections.Add(room);
        _inSystem = true;
    }

    public bool IsConnectionWith(Room room)
    {
        return _connections.Contains(room);
    }

    public void OpenNorthDoor()
    {
        Destroy(_northDoor);
        _roomHallway.SetConnectUp();
    }

    public void OpenSouthDoor()
    {
        Destroy(_southDoor);
        _roomHallway.SetConnectDown();
    }

    public void OpenWestDoor()
    {
        Destroy(_westDoor);
        _roomHallway.SetConnectLeft();
    }

    public void OpenEastDoor()
    {
        Destroy(_easthDoor);
        _roomHallway.SetConnectRight();
    }
}
