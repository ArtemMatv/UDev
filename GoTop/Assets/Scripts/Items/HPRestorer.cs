using UnityEngine;

public class HPRestorer : MonoBehaviour
{
    [SerializeField] private int _healValue;
    private void OnTriggerEnter2D(Collider2D info)
    {
        info.GetComponent<Player_controller>().RestoreHP(_healValue);
        Destroy(gameObject);
    }
}

