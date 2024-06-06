
public abstract class Hallway
{
    public abstract string Name { get; }

    public abstract bool ConnectRight { get; }
    public abstract bool ConnectLeft { get; }
    public abstract bool ConnectUp { get; }
    public abstract bool ConnectDown { get; }
}

public class AngleDownLeft : Hallway
{
    public override string Name => NamesHallway.AngleDownLeft;

    public override bool ConnectDown => true;
    public override bool ConnectLeft => true;
    public override bool ConnectRight => false;
    public override bool ConnectUp => false;
}

public class AngleDownRight : Hallway
{
    public override string Name => NamesHallway.AngleDownRight;

    public override bool ConnectDown => true;

    public override bool ConnectLeft => false;
    public override bool ConnectRight => true;
    public override bool ConnectUp => false;
}

public class AngleUpLeft : Hallway
{
    public override string Name => NamesHallway.AngleUpLeft;

    public override bool ConnectDown => false;
    public override bool ConnectLeft => true;
    public override bool ConnectRight => false;
    public override bool ConnectUp => true;
}

public class AngleUpRight : Hallway
{
    public override string Name => NamesHallway.AngleUpRight;

    public override bool ConnectDown => false;
    public override bool ConnectLeft => false;
    public override bool ConnectRight => true;
    public override bool ConnectUp => true;
}

public class Crossroad : Hallway
{
    public override string Name => NamesHallway.Crossroad;

    public override bool ConnectDown => true;
    public override bool ConnectLeft => true;
    public override bool ConnectRight => true;
    public override bool ConnectUp => true;
}

public class HorizontalStraight : Hallway
{
    public override string Name => NamesHallway.HorizontalStraight;

    public override bool ConnectDown => false;
    public override bool ConnectLeft => true;
    public override bool ConnectRight => true;
    public override bool ConnectUp => false;
}

public class VerticalStraight : Hallway
{
    public override string Name => NamesHallway.VerticalStraight;

    public override bool ConnectDown => true;
    public override bool ConnectLeft => false;
    public override bool ConnectRight => false;
    public override bool ConnectUp => true;
}

public class RoomHallway : Hallway
{
    public override string Name => NamesHallway.Room;

    private bool _right = false;
    private bool _left = false;
    private bool _up = false;
    private bool _down = false;

    public void SetConnectRight()
    {
        _right = true;
    }

    public void SetConnectLeft()
    {
        _left = true;
    }

    public void SetConnectUp()
    {
        _up = true;
    }

    public void SetConnectDown()
    {
        _down = true;
    }

    public override bool ConnectDown => _down;
    public override bool ConnectLeft => _left;
    public override bool ConnectRight => _right;
    public override bool ConnectUp => _up;
}

public class TDown : Hallway
{
    public override string Name => NamesHallway.TDown;

    public override bool ConnectDown => true;
    public override bool ConnectLeft => true;
    public override bool ConnectRight => true;
    public override bool ConnectUp => false;
}

public class TLeft : Hallway
{
    public override string Name => NamesHallway.TLeft;

    public override bool ConnectDown => true;
    public override bool ConnectLeft => true;
    public override bool ConnectRight => false;
    public override bool ConnectUp => true;
}

public class TRight : Hallway
{
    public override string Name => NamesHallway.TRight;

    public override bool ConnectDown => true;
    public override bool ConnectLeft => false;
    public override bool ConnectRight => true;
    public override bool ConnectUp => true;
}

public class TUp : Hallway
{
    public override string Name => NamesHallway.TUp;

    public override bool ConnectDown => false;
    public override bool ConnectLeft => true;
    public override bool ConnectRight => true;
    public override bool ConnectUp => true;
}

public class VoidHollway : Hallway
{
    public override string Name => NamesHallway.Void;

    public override bool ConnectDown => false;
    public override bool ConnectLeft => false;
    public override bool ConnectRight => false;
    public override bool ConnectUp => false;
}
