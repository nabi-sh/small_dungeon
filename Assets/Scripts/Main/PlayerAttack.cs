using System.Collections.Generic;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private Enemy enemy;
    public List<Enemy> enemies = new List<Enemy>();
    private Animator playerAnimator;
    private float knockback = 500;
    private AudioSource audioSource;
    void OnEnable()
    {
        playerAnimator = GetComponentInParent<Animator>();
        enemies.Clear();
        Debug.Log("List Cleared");
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            enemy = collision.GetComponent<Enemy>();
            if(enemy != null && !enemies.Contains(enemy))
            {
                enemy.Health -= 3;
                audioSource.Play();
                enemy.isHurt = true;
                enemy.rb.linearVelocity = new Vector2 (0, 0);
                enemy.rb.AddForce(new Vector2 (playerAnimator.GetFloat("AttackX"), playerAnimator.GetFloat("AttackY")) * knockback, ForceMode2D.Force);
                enemies.Add(enemy);
                Debug.Log(enemy.name + " health" + enemy.Health);   
            }
        }
    }
}
