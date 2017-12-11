namespace HungerMaze
{
    abstract class FactoryItem
    {
        abstract public IItem GetItem(Vector pos, float useReduction);
    }
}