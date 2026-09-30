using UnityEngine;

public class EnemyEyes : MonoBehaviour
{
    private Enemy enemy;
    void Start()
    {
        enemy  = GetComponentInParent<Enemy>();
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody"))
        {
            // Debug.Log(collision.name);
            enemy.playerPos = collision.transform.position;
            enemy.enemyState= EnemyState.Follow;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBody"))
        {
            enemy.enemyState = EnemyState.Idle;
        }
    }
}
