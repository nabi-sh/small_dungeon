using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip playerHitSound;
    public AudioClip playerHurtSound;
    public AudioSource audioSource;
    public AudioClip keyPickupSound;
    public AudioClip chestOpenSound;
    public static AudioManager Instance;
    
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
         Instance = this;
         DontDestroyOnLoad(gameObject);

        audioSource = GetComponent<AudioSource>();
        
    }

}
