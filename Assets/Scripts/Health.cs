using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private int Hp;
    [SerializeField] private EnemyControl enemy;
    
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    public void TakeDamage(int damage)
    {
        Hp -= damage;
        if(animator != null)
            animator.SetBool("Hit", true);
       
        if (Hp <= 0)
            DeathAnimator();
    }
    

    public void EndAnimation()
    {
        if (animator != null)
            animator.SetBool("Hit", false);
    }

    public void Death()
    {
        Destroy(gameObject);
    }

    public void DeathAnimator()
    {
        if (enemy != null)
        { 
            enemy.canmove = false;
        }
        else
            Death();

        if (animator != null)
            animator.SetBool("Death", true);
        
    } 
}
