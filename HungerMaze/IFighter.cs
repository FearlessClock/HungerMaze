namespace HungerMaze
{
    public interface IFighter
    {
        Cell GetCell { get; }
        Vector GetPosition { get; }

        void Attack(NormalFighter enemy);
        void ChangeStance(IStance newStance);
        bool CheckForEnd();
        void LoseHealth(float amount);
        void Move(Cell toCell);
        void React(IItem[] items, Cell[] cells, NormalFighter[] fighters);
    }
}