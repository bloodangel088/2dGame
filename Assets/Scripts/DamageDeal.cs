using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageDeal : MonoBehaviour
{
    [SerializeField] private int damage;
    private PlayerHp player;
    [SerializeField] private float cd;
    private float nextAt;

    private void OnTriggerEnter2D(Collider2D info)
    {
        player = info.GetComponent<PlayerHp>();
        
    }
    private void FixedUpdate()
    {


        if (player != null && nextAt <= Time.time)
        {
            player.ChangeHp(-damage);
            nextAt = Time.time + cd;
        }
    }
    private void OnTriggerExit2D(Collider2D info)
    {
        if(info.GetComponent<PlayerHp>()!= null)
        {
            player = null;
        }
    }

}
