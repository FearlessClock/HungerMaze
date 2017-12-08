namespace HungerMaze
{
    public class Item
    {
        float damage;
        float useReduction = 1;

        public float UseItem()
        {
            float damagePower = damage;
            damage -= useReduction;
            return damagePower;
        }
    }
}