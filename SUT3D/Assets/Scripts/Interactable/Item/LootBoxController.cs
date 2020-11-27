using UnityEngine;

public class LootBoxController : ItemBaseController
{
    [SerializeField] private int _experience;
    [SerializeField] private int _gold;
    [SerializeField] private SlotTakerItem _item;

    public int Experience => _experience;
    public int Gold => _gold;
    public SlotTakerItem Item => _item;

    protected void Open()
    {
        switch (Random.Range(0, 3))
        {
            case 0:
                //return _experience
                break;
            case 1:
                //return _gold
                break;
            case 2:
                //return _item
                break;
            default:
                break;
        }
    }

    public override void Interact()
    {
        Open();
    }
}