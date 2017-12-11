namespace HungerMaze
{
    public interface IItem
    {
        object Position { get; }
        float UseItem();
    }
}