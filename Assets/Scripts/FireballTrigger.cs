using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballTrigger : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float damagedealcd;

    private Playermovement castFireball;
    private Animator animator;
    private PlayerHp player;
    private float nextAt;

    private void Start()
    {
        animator = GetComponent<Animator>();
        castFireball = GetComponent<Playermovement>();
    }

    private void OnTriggerEnter2D(Collider2D info)
    {
        player = info.GetComponent<PlayerHp>();
        if (info.isTrigger)
            return;
        Health health = info.GetComponent<Health>();
        if (player != null && nextAt <= Time.time)
        {
            player.ChangeHp(-damage);
            nextAt = Time.time + damagedealcd;
        }
        if (health != null)
        {

            health.TakeDamage(damage);
        
        }
        StartDestroy();
    }
    private void StartDestroy()
    {
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        animator.SetBool("Trigger", true);
        Destroy(gameObject, 0.25f);
    }
}
