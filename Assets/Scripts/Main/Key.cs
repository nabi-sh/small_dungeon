using UnityEngine;
using UnityEngine.UI;

public class Key : MonoBehaviour
{
    public PlayerControl playerControl;
    public Image UIKey;
    void Start()
    {
        playerControl = GameObject.Find("Player").GetComponent<PlayerControl>();
        UIKey = GameObject.Find("UI").transform.Find("UIKey").GetComponent<Image>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerControl.hasKey = true;
            UIKey.gameObject.SetActive(true);
            AudioManager.Instance.audioSource.PlayOneShot(AudioManager.Instance.keyPickupSound);
            Destroy(gameObject);
        }
    }
}
