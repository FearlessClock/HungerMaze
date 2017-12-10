using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    public class NormalFighter : IFighter
    {
        float damage;
        float life;

        IStance _stance;

        bool[,] visitedCells;
        Stack<Cell> path = new Stack<Cell>();

        List<Item> inventory = new List<Item>();

        Cell currentCell;

        public bool CanFight()
        {
            if(inventory.Count > 0)
            {
                return true;
            }
            return false;
        }

        internal ConsoleColor color;

        public Vector GetPosition { get { return currentCell.Position; } }

        public Cell GetCell { get { return currentCell; } }

        public float Life { get { return life; } }

        public NormalFighter(IStance stance, Vector mazeSize, Cell currentCell, float damage, float life, int randColor)
        {
            color = (ConsoleColor)randColor;
            this._stance = stance;
            this.damage = damage;
            this.life = life;
            this.currentCell = currentCell;
            this.visitedCells = new bool[mazeSize.x, mazeSize.y];
        }

        public void AddItem(Item item)
        {
            inventory.Add(item);
        }

        public void ChangeStance(IStance newStance)
        {
            _stance = newStance;
        }

        public void React(Item[] items, Cell[] cells, NormalFighter[] fighters)
        {
            _stance.React(this, items, cells, fighters, visitedCells, path);
        }

        public void LoseHealth(float amount)
        {
            life -= amount;
        }

        public void Attack(NormalFighter enemy)
        {
            if (inventory.Count > 0)
            {
                float sumOfDamage = damage;
                foreach (Item d in inventory)
                {
                    sumOfDamage += d.UseItem();
                }

                enemy.LoseHealth(sumOfDamage);
            }
        }

        public void Move(Cell toCell)
        {
            //We know for sure that the cell is empty and moveable
            visitedCells[GetPosition.x, GetPosition.y] = true;
            toCell.CurrentFighter = this;
            currentCell = toCell;
        }

        public bool IsDead()
        {
            if(life > 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckForEnd()
        {
            return currentCell.End;
        }
    }
}
