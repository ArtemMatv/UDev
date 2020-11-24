using System.Collections.Generic;

public class ArmyController : EnemyBaseController
{
    private List<(EnemiesTypes, int)> _army;
    public ArmyController(string name, EnemiesTypes[] types, params int[] amounts)
        : base(name)
    {
        _army = new List<(EnemiesTypes, int)>();

        for (int i = 0; i < types.Length; i++)
        {
            _army.Add((types[i], amounts[i]));
        }
    }

}

