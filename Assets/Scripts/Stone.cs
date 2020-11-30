using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stone : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int damage;
    private Rigidbody2D stoneRb;
   

    private void Start()
    {
        stoneRb = GetComponent<Rigidbody2D>();
    }
    
    private void FixedUpdate()
    {
        stoneRb.AddForce(new Vector2(speed, 0));
        
    }
   void OnTriggerEnter2D(Collider2D collider)
    {
        
        if (stoneRb.velocity.x > 0)
        {
            var Hp = collider.GetComponent<PlayerHp>();
            if (Hp != null)
            Hp.ChangeHp(-damage);
        }
    }
}
