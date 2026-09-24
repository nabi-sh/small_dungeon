using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerControl : MonoBehaviour
{
    public int health = 5;
    private float speed = 5f;
    private Vector2 movement;
    private Rigidbody2D rb;
    private Animator animator;
    private float attackCooldown = 0.4f;
    private float timeSinceAttack = 0;
    private BoxCollider2D attackZone;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        attackZone = transform.Find("Sword").GetComponent<BoxCollider2D>();
        // Set sword collider size
        attackZone.size = new Vector2 (0.8f, 1.3f);
        attackZone.offset = new Vector2 (0f, -0.5f);
        attackZone.enabled = false;
    }
    private void OnMovement(InputValue input)
    {
        movement = input.Get<Vector2>();
        
        if(movement.x != 0 || movement.y != 0)
        {
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);

            animator.SetBool("IsWalking", true);
        } else
        {
            animator.SetBool("IsWalking", false);
        }
    }
    private void OnAttack()
    {
        if(timeSinceAttack >= attackCooldown)
        {
            StartCoroutine("Attack");
            timeSinceAttack = 0;
        }
    }
    private void FixedUpdate()
    {
        timeSinceAttack += Time.fixedDeltaTime;
        if(animator.GetBool("isAttacking") == false) {
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime * speed);
        }
    }
    private IEnumerator Attack()
    {
        animator.SetBool("isAttacking", true);
        attackZone.enabled = true;

        if (animator.GetFloat("X") > 0)
        {
            attackZone.size = new Vector2 (1.3f, 0.8f);
            attackZone.offset = new Vector2 (0.8f, 0f);
        } else if (animator.GetFloat("X") < 0)
        {
            attackZone.size = new Vector2 (1.3f, 0.8f);
            attackZone.offset = new Vector2 (-0.8f, 0f);
        } else if (animator.GetFloat("Y") > 0)
        {
            attackZone.size = new Vector2 (0.8f, 1.3f);
            attackZone.offset = new Vector2 (0f, 0.5f);
        } else if (animator.GetFloat("Y") < 0)
        {
            attackZone.size = new Vector2 (0.8f, 1.3f);
            attackZone.offset = new Vector2 (0f, -0.7f);
        }

        yield return new WaitForSeconds(0.4f);
        attackZone.enabled = false;
        animator.SetBool("isAttacking", false);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        return;
    }
}
