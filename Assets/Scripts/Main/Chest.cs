using UnityEngine;

public class Chest : MonoBehaviour
{
    public PlayerControl playerControl;
    void Start()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerControl.hasKey)
        {
            Debug.Log("WINNN");
        }
    }
}
