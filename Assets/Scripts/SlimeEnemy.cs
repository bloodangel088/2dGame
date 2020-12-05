using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeEnemy : MonoBehaviour
{
    protected Rigidbody2D enemyRb;
    protected Animator animator;
    [SerializeField]  protected Transform startPoint;
    [SerializeField]  protected Transform endPoint;
    protected SlimeState currentState;
    [SerializeField] private int damage;
    [SerializeField] private float cooldown;
    private float _nextAttack;

    [Header("Movement")]
    [SerializeField] private float speed;

    [SerializeField] private float _attackRange;
    [SerializeField] private float _biteRange;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;
    protected Transform _target;
    private Transform _currentPoint;
    private PlayerHp _playerHp;

    protected bool faceRight = true;
    internal bool canmove = true;
    protected virtual void Start()
    {
        _currentPoint = startPoint;
        _playerHp = FindObjectOfType<PlayerHp>();
        _target = FindObjectOfType<Playermovement>().gameObject.transform;
        enemyRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        ChangeState(SlimeState.Move);
    }

    private void SetNewPoint()
    {
        _currentPoint = _currentPoint == startPoint ? endPoint : startPoint;
    }

    private void FixedUpdate()
    {

        if (Mathf.Abs(transform.position.x - _target.position.x) <= _biteRange)
        {
            ChangeState(SlimeState.Attack);

            return;
        }

        ChangeState(SlimeState.Move);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _attackRange);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, _biteRange);
    }

    protected void Update()
    {
        if (currentState == SlimeState.Idle)
            Idle();
        else if (currentState == SlimeState.Move)
            Move();
        else if (currentState == SlimeState.Attack)
            Attack();
    }

    protected virtual void Idle()
    {

    }

    protected virtual void Move()
    {

        
        if (_currentPoint.position.x < transform.position.x && faceRight)
            Flip();
        else if (_currentPoint.position.x > transform.position.x && !faceRight)
            Flip();

        if (Mathf.Abs(transform.position.x - _currentPoint.position.x) <= 0.3f)
        {
            SetNewPoint();
        }

        enemyRb.velocity = transform.right * new Vector2(speed, 0f);
    }

    protected virtual void Attack()
    {
      
        if (_target.position.x < transform.position.x && faceRight)
            Flip();
        else if (_target.position.x > transform.position.x && !faceRight)
            Flip();

        if ((Mathf.Abs(transform.position.x - _target.position.x ) <= _attackRange) && (Mathf.Abs(transform.position.y - _target.position.y) <= _attackRange))
        {
            animator.SetBool("Attack", true);
            if (Time.time > _nextAttack)
            {
                _playerHp.ChangeHp(-damage);
                _nextAttack = Time.time + cooldown;
            }
        }
        else
        {
            animator.SetBool("Attack", false);
        }

        enemyRb.velocity = transform.right * new Vector2(speed, 0f);
    }

    protected void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0, 180, 0);
    }

    private bool IsGroundEnding()
    {
        return !Physics2D.OverlapPoint(groundCheck.position, whatIsGround);
    }

    protected virtual void ChangeState(SlimeState state)
    {
        if (currentState != SlimeState.Idle)
            animator.SetBool(currentState.ToString(), false);

        if (state != SlimeState.Idle && state != SlimeState.Attack)
            animator.SetBool(state.ToString(), true);

        currentState = state;
    }
}

public enum SlimeState
{
    Idle,
    Move,
    Attack,
}
