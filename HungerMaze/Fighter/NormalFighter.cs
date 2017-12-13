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

        string name;

        bool[,] visitedCells;
        Stack<Cell> path = new Stack<Cell>();

        List<IItem> inventory = new List<IItem>();

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

        public string getName => name;

        ConsoleColor IFighter.Color => color;

        public List<IItem> GetItems => inventory;

        public NormalFighter(IStance stance, string name, Vector mazeSize, Cell currentCell, float damage, float life, int randColor)
        {
            color = (ConsoleColor)randColor;
            this._stance = stance;
            this.name = name;
            this.damage = damage;
            this.life = life;
            this.currentCell = currentCell;
            this.visitedCells = new bool[mazeSize.x, mazeSize.y];
        }

        public void AddItem(IItem item)
        {
            inventory.Add(item);
            ChangeStance(new AttackStance());
        }

        public void ChangeStance(IStance newStance)
        {
            _stance = newStance;
        }

        public void React(IItem[] items, Cell[] cells, IFighter[] fighters)
        {
            _stance.React((IFighter)this, items, cells, fighters, visitedCells, path);
        }

        public void LoseHealth(float amount)
        {
            life -= amount;
        }

        public void Attack(IFighter enemy)
        {
            if (inventory.Count > 0)
            {
                float sumOfDamage = damage;
                IItem[] copyInven = inventory.ToArray<IItem>();
                foreach (Item d in copyInven)
                {
                    float dam = d.UseItem();
                    if (dam == -1)
                    {
                        inventory.Remove(d);
                    }
                    else
                    {
                        sumOfDamage += dam;
                    }
                }

                enemy.LoseHealth(sumOfDamage);
            }
            else
            {
                _stance = new DefenceStance();
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

        public void RemoveAllItems()
        {
            inventory = new List<IItem>();
        }
    }
}
