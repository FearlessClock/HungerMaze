namespace HungerMaze
{
    public class Item : IItem
    {
        string name;
        Vector position;
        float damage;
        float useReduction = 1;

        public Item(string name, Vector pos, float damage, float useReduction = 1)
        {
            this.name = name;
            this.damage = damage;
            this.useReduction = useReduction;
            this.position = pos;
        }

        public object Position { get { return position; } }

        public float UseItem()
        {
            float damagePower = damage;
            damage -= useReduction;
            return damagePower;
        }
    }
}