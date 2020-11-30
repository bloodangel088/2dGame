using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public abstract class EnemyControl : MonoBehaviour
{
    protected Rigidbody2D enemyRb;
    protected Animator animator;
    protected Vector2 startPoint;
    protected EnemyState currentState;

    protected float lastStateChange;
    protected float timeToNextChange;

    [SerializeField] private float maxStateTime;
    [SerializeField] private float minStateTime;
    [SerializeField] private EnemyState[] availableState;

    [Header("Movement")]
    [SerializeField] private float speed;
    [SerializeField] private float range;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask whatIsGround;

    private bool faceRight = true;
    internal bool canmove = true;
    void Start()
    {
        startPoint = transform.position;
        enemyRb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        if (IsGroundEnding())
            Flip();
        if (currentState == EnemyState.Move)
            Move();
    }

    protected void Update()
    {
        if (Time.time - lastStateChange > timeToNextChange)
            GetRandomState();
    }

    protected virtual void Move()
    {
        if (!canmove)
        {
            enemyRb.velocity = Vector2.zero;
            return;
        }
        enemyRb.velocity = transform.right * new Vector2(speed, enemyRb.velocity.y);
    }

    protected void Flip()
    {
        faceRight = !faceRight;
        transform.Rotate(0, 180, 0);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(range * 2, 0.5f, 0));
    }

    private bool IsGroundEnding()
    {
        return !Physics2D.OverlapPoint(groundCheck.position, whatIsGround);
    }

    protected void GetRandomState()
    {
        int state = Random.Range(0, availableState.Length);

        if (currentState == EnemyState.Idle && availableState[state] == EnemyState.Idle)
        {
            GetRandomState();
        }

        timeToNextChange = Random.Range(minStateTime, maxStateTime);
        ChangeState(availableState[state]);
    }

    protected virtual void ChangeState(EnemyState state)
    {
        if (currentState != EnemyState.Idle)
            animator.SetBool(currentState.ToString(), false);

        if (state != EnemyState.Idle)
            animator.SetBool(state.ToString(), true);

        currentState = state;
        lastStateChange = Time.time;
    }
}
public enum EnemyState
{
    Idle,
    Move,
    Attack,
    
}
