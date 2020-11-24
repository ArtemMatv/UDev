using System.Collections.Generic;

public class VillagerController : NPCBaseController
{
    public VillagerController(string _name)
    {
        this.Health = 100;
        this.Messages = new List<string> { "Hello", "How can I help you?" };
        this.Name = _name;
        this.Role = "Villager";
    }

    protected override void Move()
    {
        // move in the village only
    }

    public override void Interact()
    {
        // open dialog with player
        Talk();
    }
}
