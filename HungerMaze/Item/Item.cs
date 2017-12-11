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

        public Vector Position => position;

        public float UseItem()
        {
            float damagePower = damage;
            damage -= useReduction;
            if(damage <= 0)
            {
                return -1;
            }
            return damagePower;
        }
    }
}