
using System;

public class GridMap
{
    private Hallway[,] _map;
    public Hallway[,] Map => _map;

    private TUp _tUp = new TUp();
    private TLeft _tLeft = new TLeft();
    private TRight _tRight = new TRight();
    private TDown _tDown = new TDown();

    private Crossroad _crossroad = new Crossroad();

    private AngleDownRight _angleDownRight = new AngleDownRight();
    private AngleDownLeft _angleDownLeft = new AngleDownLeft();
    private AngleUpRight _angleUpRight = new AngleUpRight();
    private AngleUpLeft _angleUpLeft = new AngleUpLeft();

    private VerticalStraight _verticalStraight = new VerticalStraight();
    private HorizontalStraight _horizontalStraight = new HorizontalStraight();

    private VoidHollway _void = new VoidHollway();

    public GridMap(int height, int width)
    {
        _map = new Hallway[height, width];
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                _map[i, j] = _void;
            }
        }
    }

    public RoomHallway AddRoom(int x, int y)
    {
        RoomHallway room = new RoomHallway();
        _map[x, y] = room;
        return room;
    }

    public void AddVerticalStraightHallWay(int x, int y)
    {
        if (_map[x, y] == _verticalStraight) { return; }

        if (_map[x, y] is RoomHallway)
        {
            throw new Exception("RoomPlace");
        }

        if (_map[x, y] == _void)
        {
            _map[x, y] = _verticalStraight;
        }
        else
        {
            _map[x, y] = Connection(x, y, upSide: true, downSide: true);
        }
    }

    public void AddHorizontalStraightHallWay(int x, int y)
    {
        if (_map[x, y] == _horizontalStraight) { return; }

        if (_map[x, y] is RoomHallway)
        {
            throw new Exception("RoomPlace");
        }

        if (_map[x, y] == _void)
        {
            _map[x, y] = _horizontalStraight;
        }
        else
        {
            _map[x, y] = Connection(x, y, leftSide: true, rightSide: true);
        }
    }

    public void AddConnection(int x, int y)
    {
        if (_map[x, y] is RoomHallway)
        {
            throw new Exception("RoomPlace");
        }
        _map[x, y] = Connection(x, y);
    }

    private Hallway Connection(int x, int y, bool leftSide = false, bool rightSide = false, bool upSide = false, bool downSide = false)
    {
        bool left = leftSide;
        bool right = rightSide;
        bool up = upSide;
        bool down = downSide;

        int numConnection = 0;

        if (y > 0 && _map[x, y - 1].ConnectRight)
        {
            left = true;
        }

        if (y < _map.GetLength(1) - 1 && _map[x, y + 1].ConnectLeft)
        {
            right = true;
        }

        if (x > 0 && _map[x - 1, y].ConnectDown)
        {
            up = true;
        }

        if (x < _map.GetLength(0) - 1 && _map[x + 1, y].ConnectUp)
        {
            down = true;
        }

        if (left) { numConnection++; }
        if (right) { numConnection++; }
        if (up) { numConnection++; }
        if (down) { numConnection++; }

        if (numConnection == 4) { return _crossroad; }

        if (numConnection == 3)
        {
            if (!left) { return _tRight; }
            if (!right) { return _tLeft; }
            if (!up) { return _tDown; }
            if (!down) { return _tUp; }
        }

        if (numConnection == 2)
        {
            if (down && right) { return _angleDownRight; }
            if (down && left) { return _angleDownLeft; }
            if (up && right) { return _angleUpRight; }
            if (up && left) { return _angleUpLeft; }

            if (left && right) { return _horizontalStraight; }
            if (up && down) { return _verticalStraight; }
        }

        return _void;
    }
}
