using System.Collections.Generic;

public class AnimalController : NPCBaseController
{
    public AnimalController(string _name)
    {
        this.Health = 100;
        this.Messages = new List<string> { "Ghaf", "Ghaf Ghaf" };
        this.Name = _name;
        this.Role = "Animal";
    }

    protected override void Move()
    {
        // move in the village only
    }
}
