using System;

public interface GameObject
{
    int X
    {
        get;
    }

    int Y
    {
        get;
    }

    ConsoleColor Color
    {
        get;
    }

    void Draw();
}