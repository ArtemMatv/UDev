using UnityEngine;

public class MPRestorer : MonoBehaviour
{
    [SerializeField] private int _mpValue;
    private void OnTriggerEnter2D(Collider2D info)
    {
        info.GetComponent<Player_controller>().RestoreMP(_mpValue);
        Destroy(gameObject);
    }
}

