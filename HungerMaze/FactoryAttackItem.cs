using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    class FactoryAttackItem : FactoryItem
    {
        public override IItem GetItem(Vector pos, float useReduction)
        {
            Random rand = new Random();
            int dmg = rand.Next(1, 10);
            IItem item = new AttackItem("Sword", pos, dmg, useReduction);
            return item;
        }
    }
}
