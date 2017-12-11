using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    interface IFactoryFighter
    {
        IFighter GetFighter(eFighterType fighterType, Cell currentCell, int randColor);
    }
}
