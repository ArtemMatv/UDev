using UnityEngine;

public class MonstersController : EnemyBaseController
{
    [SerializeField] private Detachment _monsters;

    public Detachment Monsters => _monsters;
}
