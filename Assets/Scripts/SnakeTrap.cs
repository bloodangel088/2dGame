using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TrapState
{
    Idle,
    PlayerDetected,
    Attack
}

public class SnakeTrap : MonoBehaviour
{
    [SerializeField] private float detectrad;
    [SerializeField] private float attackrad;
    [SerializeField] private TrapState TrapState;
    [SerializeField] private float attackCd;
    [SerializeField] private int damage;
    [SerializeField] private LayerMask layerMask;

    private bool isRightDirection;
    private Animator animator;
    private Transform player;
    private DateTime _lastAttacked;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        player = FindObjectOfType<PlayerHp>().transform;
    }

    private void Update()
    {
        switch (TrapState)
        {
            case TrapState.Idle:
                Idle();
                break;
            case TrapState.PlayerDetected:
                PlayerDetected();
                break;
            case TrapState.Attack:
                Attack();
                break;
        }
    }

    private void Idle()
    {
        //do nothing :)
    }

    private void PlayerDetected()
    {
        bool inRange = (Mathf.Abs(Vector2.Distance(transform.position, player.transform.position)) <= attackrad);
        if (inRange)
        {
            SetState(TrapState.Attack);
        }
        else
        {
            SetState(TrapState.PlayerDetected);
        }
    }

    private void Attack()
    {
        if ((DateTime.Now - _lastAttacked).TotalSeconds >= attackCd)
        {
            player.GetComponent<PlayerHp>().ChangeHp(-damage);
            _lastAttacked = DateTime.Now;
            
        }
        bool inRange = (Physics2D.OverlapCircle(transform.position, attackrad,layerMask) != null);
        if (!inRange)
        {
            SetState(TrapState.PlayerDetected);
            animator.SetBool("Attack", false);
        }
    
    }

    private void FixedUpdate()
    {
        bool inRange = (Mathf.Abs(Vector2.Distance(transform.position, player.transform.position)) <= detectrad);
        if (inRange)
        {
            Turn();
            if (TrapState == TrapState.Idle)
            {
                SetState(TrapState.PlayerDetected);
            }
        }
        else
        {
            animator.SetBool("PlayerDetected", false);
            SetState(TrapState.Idle);
        }
    }

    private void SetState(TrapState state)
    {
        animator.SetBool(state.ToString(), false);
        TrapState = state;
        animator.SetBool(state.ToString(), true);
    }

    private void Turn()
    {
        if (player.position.x - transform.position.x > 0 && !isRightDirection)
            Flip();
        else if (player.transform.position.x - transform.position.x < 0 && isRightDirection)
            Flip();
    }

    private void Flip()
    {
        isRightDirection = !isRightDirection;
        transform.Rotate(0, 180, 0);
    }

    public virtual void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectrad);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackrad);
    }

}