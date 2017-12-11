using System;

namespace HungerMaze
{
    class FactoryLifePointItem : FactoryItem
    {
        public override IItem GetItem(Vector pos, float useReduction)
        {
            Random rand = new Random();
            int lfp = rand.Next(1, 10);
            IItem item = new LifePointItem("Apple", pos, lfp, useReduction);
            return item;
        }
    }
}