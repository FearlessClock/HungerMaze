namespace HungerMaze
{
    public interface IItem
    {
        float UseItem();
        Vector Position { get; } 
    }
}