using System;

namespace HungerMaze
{
    public interface IItem
    {
        float UseItem();
        Vector Position { get; set; }
        string Name { get; }
        ConsoleColor ConsoleColor { get; set; }
    }
}