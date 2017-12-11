using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HungerMaze
{
    class IFactoryItem
    {
        public IItem GetItem(eItemType type, Vector pos, float useReduction)
        {
            switch (type)
            {
                case eItemType.ATTACK:
                    FactoryItem factoryAttack = new FactoryAttackItem();
                    return factoryAttack.GetItem(pos, useReduction);
                case eItemType.LIFEPOINT:
                    FactoryItem factoryLifePoint = new FactoryLifePointItem();
                    return factoryLifePoint.GetItem(pos, useReduction);
                default:
                    return null;
            }
        }
    }
}
