using System.Collections.Generic;

public class BlacksmithController : VillagerController
{
    private List<ItemBaseController> Items { get; set; }

    public BlacksmithController(string _name) : base(_name)
    {
        this.Health = 100;
        this.Messages = new List<string> { "Hello", "Wanna trade?" };
        this.Name = _name;
        this.Role = "Blacksmith";
    }

    private void Trade()
    {
        // trade with player
    }

    private void Create()
    {
        // create something for player
    }
}
