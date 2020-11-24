public class Enemy
{
    public string Name { get; set; }
    public int Health { get; set; }
    public int Damage { get; set; }

    public EnemiesTypes _type;

    public Enemy(EnemiesTypes type)
    {
        //some settings on type
    }
}

public enum EnemiesTypes
{
    //types
}
