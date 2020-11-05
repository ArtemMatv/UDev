using UnityEngine;

public abstract class LvlEnder : MonoBehaviour
{
    [SerializeField] private Sprite[] _openClosedTextures;
    private bool _opened = false;

    protected virtual void Start()
    {
        GetComponent<SpriteRenderer>().sprite = _openClosedTextures[0];
    }
    public virtual void Open() => _opened = true;
    protected virtual void OnTriggerEnter2D(Collider2D collider)
    {
        if (!_opened)
            return;

        GetComponent<SpriteRenderer>().sprite = _openClosedTextures[1];
        PlayerPrefs.SetInt(GamePrefs.Coins.ToString(), collider.GetComponent<Player_controller>().Coins);
        ServiceManager.Instance.EndLevel();
    }
}
