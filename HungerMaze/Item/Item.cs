using System;

namespace HungerMaze
{
    public class Item : IItem
    {
        string name;
        Vector position;
        float damage;
        float useReduction = 1;
        ConsoleColor consoleColor;

        public Item(string name, Vector pos, float damage, float useReduction = 1)
        {
            this.name = name;
            this.damage = damage;
            this.useReduction = useReduction;
            this.position = pos;
            this.ConsoleColor = ConsoleColor.Magenta;
        }

        public Vector Position { get => position; set => position = value; }
        public string Name { get => name; }
        public ConsoleColor ConsoleColor { get => consoleColor; set => consoleColor = value; }

        public float UseItem()
        {
            float damagePower = damage;
            damage -= useReduction;
            if(damage <= 0)
            {
                return -1;
            }
            return damagePower;
        }
    }
}