using UnityEngine;

public class Camera : MonoBehaviour
{
    public Transform player;
    void FixedUpdate()
    {
        if (player != null) transform.position = player.transform.position + new Vector3(0, 0, -1.3f);
    }
}
