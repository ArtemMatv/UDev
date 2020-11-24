public class EnemyBaseController : Interactable
{
    public string Name { get; private set; }

    public EnemyBaseController(string name)
    {
        Name = name;
    }
    public override void Interact()
    {
        CalculateOutcome();
    }

    private void CalculateOutcome()
    {
        //calculation
    }
}

