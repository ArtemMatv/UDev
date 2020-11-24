public class MonstersController : EnemyBaseController
{
    public int Amount { get; private set; }
    public EnemiesTypes Type { get; private set; }
    public MonstersController(string name, EnemiesTypes type, int amount)
        : base(name)
    {
        Type = type;
        Amount = amount;
    }
}
