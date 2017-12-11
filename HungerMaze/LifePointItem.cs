using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    class LifePointItem : IItem
    {
        string name;
        Vector position;
        float healedLifePoint;
        float useReduction = 1;

        public LifePointItem(string name, Vector pos, float healedLifePoint, float useReduction = 1)
        {
            this.name = name;
            this.healedLifePoint = healedLifePoint;
            this.useReduction = useReduction;
            this.position = pos;
        }

        public object Position { get { return position; } }

        public float UseItem()
        {
            float healed = healedLifePoint;
            healedLifePoint -= useReduction;
            return healed;
        }
    }
}
