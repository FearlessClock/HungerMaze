using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    interface IItemFactory
    {
        IItem GetItem(eItemType itemType, Vector position);
    }
}
