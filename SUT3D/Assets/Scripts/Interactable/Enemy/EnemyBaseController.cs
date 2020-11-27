using UnityEngine;

public abstract class EnemyBaseController : Interactable
{
    [SerializeField] private string _name;
    public string Name => _name;

    public override void Interact()
    {
        CalculateOutcome();
    }

    private void CalculateOutcome()
    {
        //calculation
    }
}

