public class HeroArmyController : ArmyController
{
    private Hero _hero;
    public HeroArmyController(string name, Hero enemyHero, EnemiesTypes[] types, params int[] amounts)
        : base(name, types, amounts)
    {
        _hero = enemyHero;
    }
    protected virtual void Move()
    {
        //move close to nest
    }
}
