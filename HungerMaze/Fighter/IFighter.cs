using System;
using System.Collections.Generic;

namespace HungerMaze
{
    public interface IFighter
    {
        Cell GetCell { get; }
        Vector GetPosition { get; }
        string getName { get; }
        ConsoleColor Color { get; }
        List<IItem> GetItems { get; }

        void Attack(IFighter enemy);
        void ChangeStance(IStance newStance);
        bool CheckForEnd();
        void LoseHealth(float amount);
        void Move(Cell toCell);
        void React(IItem[] items, Cell[] cells, IFighter[] fighters);
        void AddItem(IItem item);
        void RemoveAllItems();
        bool CanFight();
        bool IsDead();

    }
}