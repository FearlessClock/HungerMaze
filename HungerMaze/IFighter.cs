namespace HungerMaze
{
    public interface IFighter
    {
        Cell GetCell { get; }
        Vector GetPosition { get; }

        void Attack(Fighter enemy);
        void ChangeStance(IStance newStance);
        bool CheckForEnd();
        void LoseHealth(float amount);
        void Move(Cell toCell);
        void React(Item[] items, Cell[] cells, Fighter[] fighters);
    }
}