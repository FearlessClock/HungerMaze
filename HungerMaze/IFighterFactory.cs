using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    interface IFighterFactory
    {
        void CreateFighter(IStance stance, Vector mazeSize, Cell currentCell, float damage, float life, int randColor);
    }
}
