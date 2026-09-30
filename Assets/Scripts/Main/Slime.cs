using System.Collections;
using NUnit.Framework;
using UnityEngine;

public class Slime : Enemy // INHERITANCE
{
    private int health = 5;
    private float attackCooldown = 2f;
    private PlayerControl player;
    private float knockback = 500;
    public override int Health { // ENCAPSULATION
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
    public override void Attack() // POLYMORPHISM
    {
        isAttacking = true;
        player.animator.SetBool("IsHurt", true);

        player.animator.SetFloat("HurtX", movingDir.x);
        player.animator.SetFloat("HurtY", movingDir.y);
        
        player.audioSource.Play();
        rb.AddForce(-movingDir * knockback, ForceMode2D.Force);
        player.rb.AddForce(movingDir * knockback, ForceMode2D.Force);
        player.Health -= 1;
        timeSinceEnemyAttack = 0;
        StartCoroutine("EndAttack");
    }
    private IEnumerator EndAttack()
    {
        yield return new WaitForSeconds(1.5f);
        isAttacking = false;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && timeSinceEnemyAttack >= attackCooldown)
        {
            Debug.Log("Attacked" + collision.name);
            player = collision.GetComponent<PlayerControl>();
            Attack(); // ABSTRACTION
        }
    }
}
