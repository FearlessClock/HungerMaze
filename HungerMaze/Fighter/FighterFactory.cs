using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    class FighterFactory : IFactoryFighter
    {
        Vector mazeSize;
        Random rand;
        int nmbrOfFightersCreated;
        public FighterFactory(Vector mazeSize)
        {
            this.mazeSize = mazeSize;
            rand = new Random();
            nmbrOfFightersCreated = 0;
        }
        public IFighter GetFighter(eFighterType fighterType, Cell currentCell, int randColor)
        {
            nmbrOfFightersCreated++;
            switch (fighterType)
            {
                case eFighterType.weakFighter:
                    return new NormalFighter(new DefenceStance(), "Weak Fighter " + nmbrOfFightersCreated, mazeSize, currentCell, rand.Next(0, 4), 100, randColor);
                case eFighterType.strongFighter:
                    return new NormalFighter(new DefenceStance(), "Strong Fighter " + nmbrOfFightersCreated, mazeSize, currentCell, rand.Next(4, 10), 100, randColor);
                case eFighterType.normalFighter:
                    return new NormalFighter(new DefenceStance(), "Normal Fighter " + nmbrOfFightersCreated, mazeSize, currentCell, rand.Next(0, 10), 100, randColor);
                case eFighterType.slowFighter:
                    return new NormalFighter(new DefenceStance(), "Slow Fighter " + nmbrOfFightersCreated, mazeSize, currentCell, rand.Next(0, 10), 100, randColor);
                default:
                    return new NormalFighter(new DefenceStance(), "That weird default guy " + nmbrOfFightersCreated, mazeSize, currentCell, rand.Next(0, 10), 100, randColor);
            }
        }
    }
}
