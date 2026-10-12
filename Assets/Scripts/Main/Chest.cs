using UnityEngine;

public class Chest : MonoBehaviour
{
    public PlayerControl playerControl;
    public GameObject winScreen;
    void Start()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && playerControl.hasKey)
        {
            Debug.Log("WINNN");
            winScreen.SetActive(true);
            AudioManager.Instance.audioSource.PlayOneShot(AudioManager.Instance.chestOpenSound);
            Time.timeScale = 0f;
        }
    }
}
