using UnityEngine;

enum EnemyState
{
    Idle,
    Follow,
    Attack
}

public abstract class Enemy : MonoBehaviour
{
    private EnemyState enemyState;
    protected Rigidbody2D rb;
    protected float speed = 3;
    protected Vector3 homePos;
    public abstract float AttackDis {get; }
    public abstract int Health {get; set; }
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        homePos = transform.position;
    }

    void FixedUpdate()
    {
        switch(enemyState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Follow:
                /* all follow logic is completed 
                 * inside of the Follow() method called by
                 * OnTriggerStay2D
                 */
                break;
        }
    }


    public virtual void Idle()
    {
        if((transform.position - homePos).magnitude >= 0.5f) //if lost player then go home
        {
            rb.MovePosition(transform.position + (homePos - transform.position).normalized * Time.fixedDeltaTime * speed);
        }
    }
    public virtual void Follow(Vector3 playerPos, float speed)
    {
        Vector3 playerDis = playerPos - transform.position;
        Vector3 movingDir = playerDis.normalized;
        rb.MovePosition(transform.position + movingDir * Time.fixedDeltaTime * speed);
        if (playerDis.magnitude <= AttackDis)
        {
            Attack();
            return;
        }
    }
    public abstract void Attack();
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Follow(collision.transform.position, speed);
            enemyState= EnemyState.Follow;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            enemyState = EnemyState.Idle;
        }
    }
}
