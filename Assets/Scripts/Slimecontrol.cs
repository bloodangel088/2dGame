using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimecontrol : EnemyControl
{
    [SerializeField] private float angerRange;
    private bool isAngry;
    protected Playermovement player;

    protected override void Start()
    {
        base.Start();
        player = FindObjectOfType<Playermovement>();
        StartCoroutine(ScanForPlayer());
    }



    protected IEnumerator ScanForPlayer()
    {
        while (true)
        {
            CheckPlayerInRange();
            yield return new WaitForSeconds(1f);
        }
    }

    protected void CheckPlayerInRange()
    {
        if (player == null)
            return;
        if (Vector2.Distance(transform.position, player.transform.position) < angerRange)
        {
            isAngry = true;
            TurnToPlayer();

            ///
        }
        else
            isAngry = false;
    }

    protected void TurnToPlayer()
    {
        if (player.transform.position.x - transform.position.x > 0 && !faceRight)
            Flip();
        else if (player.transform.position.x - transform.position.x < 0 && faceRight)
            Flip();
    }

}
