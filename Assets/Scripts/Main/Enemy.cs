using UnityEngine;
using UnityEngine.Animations;

// enemy state controls the enemy behaviour. We have two states: idle and following. 
public enum EnemyState
{
    Idle,
    Follow
}


/* Enemy is a common class for all enemy objects. This class described the idle
 * and following states of the enemies. It also includes all necessary logic 
 * for enemy getting hurt or dying. All enemy objects scripts have to be child
 * of this class.
 */
public abstract class Enemy : MonoBehaviour
{
    // death animation is a seperate gameObject that is common to all enemies of this parent class
    public GameObject deathAnimation;
    public EnemyState enemyState;
    protected Animator animator;
    public Rigidbody2D rb;
    // position and speed variables
    protected float speed = 3;
    protected Vector3 homePos;
    public Vector3 playerPos;
    protected Vector3 movingDir;
    // animation/action state bools
    public bool isHurt = false;
    public bool isAttacking = false;
    // time variables
    private float timeSinceEnemyHurt = 0;
    protected float timeSinceEnemyAttack = 100;
    private float recoveryTime = 0.6f;
    // abstract health parameter. Each enemy child class is supposed override this parameter.
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

    // idle behaviour. enemy is supposed to go to homePos the moment he loses the player.
    public virtual void Idle()
    {
        if((transform.position - homePos).magnitude >= 0.5f && !isHurt && !isAttacking) //if lost player then go home
        {
            rb.linearVelocity = (homePos - transform.position).normalized * speed;
        }
    }
    // follow behaviour. when enemy sees the player, it is supposed to follow it. state switch logic is described in the EnemyEyes script
    public virtual void Follow(Vector3 playerPos, float speed) {
        Vector3 playerDis = playerPos - transform.position;
        movingDir = playerDis.normalized;
        if (!isHurt && !isAttacking)
        {
            rb.linearVelocity = movingDir * speed;
        }
    }
    // if enemy is hurt, it is supposed to stop moving, change animationd, and then return to normal behaviour after assigned recoveryTime
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
    // if health <= 0, we play the deathAnimation and destroy the enemy.
    private void CheckIfDead()  
    {
        if (Health <= 0)
        {
            Instantiate(deathAnimation, transform.position, transform.rotation);
            Destroy(gameObject);
        }
    }
    // this is an abstract Attack method that has to be overriden by any child class. Each enemy has to have its unique attack behaviour
    public abstract void Attack(); 
    
}
