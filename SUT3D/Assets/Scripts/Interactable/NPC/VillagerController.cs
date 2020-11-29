using System.Collections.Generic;

public class VillagerController : NPCBaseController
{
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
