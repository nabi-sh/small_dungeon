using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

public class PlayerControl : MonoBehaviour
{
    [SerializeField] private int health = 5;
    public int Health { //ENCAPSULATION
        get => health;
        set {
            if (value > 0)
            {
                health = value;
            } else
            {
                health = 0;
            }
        }
    }
    [SerializeField] private float speed = 5f;
    private Vector2 movement;
    public Rigidbody2D rb;
    public Animator animator;
    private float attackCooldown = 0.5f;
    private float timeSinceAttack = 0;
    private BoxCollider2D attackZone;
    private GameObject sword;
    public AudioSource audioSource;
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Start()
    {
        attackZone = transform.Find("Sword").GetComponent<BoxCollider2D>();
        sword = transform.Find("Sword").gameObject;
        // Set sword collider size
        attackZone.size = new Vector2 (0.8f, 1.3f);
        attackZone.offset = new Vector2 (0f, -0.5f);
        sword.SetActive(false);
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
            if (animator.GetFloat("X") > 0)
            {
                animator.SetFloat("AttackX", animator.GetFloat("X"));
                animator.SetFloat("AttackY", 0);
            } else if (animator.GetFloat("X") < 0)
            {
                animator.SetFloat("AttackX", animator.GetFloat("X"));
                animator.SetFloat("AttackY", 0);
            } else if (animator.GetFloat("Y") > 0)
            {
                animator.SetFloat("AttackX", 0);
                animator.SetFloat("AttackY", animator.GetFloat("Y"));
            } else if (animator.GetFloat("Y") <= 0)
            {
                animator.SetFloat("AttackX", 0);
                animator.SetFloat("AttackY", animator.GetFloat("Y"));
            }
            animator.SetBool("isAttacking", true);
            timeSinceAttack = 0;
        }
    }
    private void FixedUpdate()
    {
        timeSinceAttack += Time.fixedDeltaTime;
        if (animator.GetBool("isAttacking") && animator.GetBool("IsHurt"))
        {
            animator.SetBool("isAttacking",false);
            Debug.Log("Attack Hurt annimation conflict fixed");
        }
        if(!animator.GetBool("isAttacking") && !animator.GetBool("IsHurt")) {
            rb.MovePosition(rb.position + movement * Time.fixedDeltaTime * speed);
        }
    }
    public void StartAttack()
    {
        sword.SetActive(true);
        Debug.Log("ATTACK CALLED BY: " + gameObject.name +
              " | INSTANCE ID: " + GetInstanceID());
        
        if (animator.GetFloat("AttackX") > 0)
        {
            attackZone.size = new Vector2 (1.3f, 0.8f);
            attackZone.offset = new Vector2 (0.8f, 0f);
        } else if (animator.GetFloat("AttackX") < 0)
        {
            attackZone.size = new Vector2 (1.3f, 0.8f);
            attackZone.offset = new Vector2 (-0.8f, 0f);
        } else if (animator.GetFloat("AttackY") > 0)
        {
            attackZone.size = new Vector2 (0.8f, 1.3f);
            attackZone.offset = new Vector2 (0f, 0.5f);
        } else if (animator.GetFloat("AttackY") < 0)
        {
            attackZone.size = new Vector2 (0.8f, 1.3f);
            attackZone.offset = new Vector2 (0f, -0.7f);
        }
    }

    public void EndAttack()
    {
        sword.SetActive(false);
        animator.SetBool("isAttacking", false);
    }
    
    public void EndHurt()
    {
        animator.SetBool("IsHurt", false);
    }
}
