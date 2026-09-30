using UnityEngine;
using UnityEngine.Animations;

public enum EnemyState
{
    Idle,
    Follow,
    Attack
}

public abstract class Enemy : MonoBehaviour
{
    public GameObject deathAnimation;
    public EnemyState enemyState;
    protected Animator animator;
    public Rigidbody2D rb;
    protected float speed = 3;
    protected Vector3 homePos;
    public Vector3 playerPos;
    protected Vector3 movingDir;
    public bool isHurt = false;
    public bool isAttacking = false;
    private float timeSinceEnemyHurt = 0;
    protected float timeSinceEnemyAttack = 100;
    private float recoveryTime = 0.6f;
    public abstract int Health {get; set; }
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        homePos = transform.position;
        animator = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        switch(enemyState)
        {
            case EnemyState.Idle:
                Idle();
                break;
            case EnemyState.Follow:
                Follow(playerPos, speed);
                break;
        }

        timeSinceEnemyAttack += Time.fixedDeltaTime;

        CheckIfEnemyHurt();
    }
    void Update()
    {
        CheckIfDead();
    }

    public virtual void Idle()
    {
        if((transform.position - homePos).magnitude >= 0.5f && !isHurt && !isAttacking) //if lost player then go home
        {
            rb.linearVelocity = (homePos - transform.position).normalized * speed;
        }
    }
    public virtual void Follow(Vector3 playerPos, float speed)
    {
        Vector3 playerDis = playerPos - transform.position;
        movingDir = playerDis.normalized;
        if (!isHurt && !isAttacking)
        {
            rb.linearVelocity = movingDir * speed;
        }
    }
    private void CheckIfEnemyHurt()
    {
        if (isHurt) {
            float progress = animator.GetCurrentAnimatorStateInfo(0).normalizedTime % 1f;
            animator.Play("ThisEnemy_Hurt", 0, progress);
            timeSinceEnemyHurt += Time.fixedDeltaTime;
            if (timeSinceEnemyHurt >= recoveryTime)
            {
                isHurt = false;
                timeSinceEnemyHurt = 0;
            }
        }
    }
    private void CheckIfDead()
    {
        if (Health <= 0)
        {
            Instantiate(deathAnimation, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    public abstract void Attack();
    
}
