using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    class ItemFactory : IItemFactory
    {
        Random rand = new Random();
        public IItem GetItem(eItemType itemType, Vector position)
        {
            switch (itemType)
            {
                case eItemType.weakItem:
                    return new Item("Weak Item", position, rand.Next(0, 3));
                case eItemType.mediumItem:
                    return new Item("Medium Item", position, rand.Next(3, 6));
                case eItemType.strongItem:
                    return new Item("Strong Item", position, rand.Next(6, 10));
                default:
                    return new Item("Default Item", position, rand.Next(0, 10));
            }
        }
    }
}
