using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(Rigidbody2D),typeof(Animator))]
public class Playermovement : MonoBehaviour
{
    private Rigidbody2D _playerRB;
    
    [Header("Horizontal move")]
    [SerializeField] private float _speed;
    private bool _faceRight = true;

    [Header("Jumping")]
    [SerializeField] private float _jumpforse;
    [SerializeField] private float _radius;
    [SerializeField] private bool _aircontroll;
    [SerializeField] private Transform _groundcheck;
    [SerializeField] private LayerMask _ground;
    private bool _grounded;

    [Header("Crowling")]
    [SerializeField] private Collider2D _headcolider;
    [SerializeField] private Transform _cellcheck;
    private bool _canstand;

    [Header("Cast")]
    [SerializeField] private GameObject fireball;
    [SerializeField] private Transform attack2;
    private bool casting;

    private Animator animator;
    private PlayerHp playerHp;

    void Start()
    {
        _playerRB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerHp = GetComponent<PlayerHp>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(_groundcheck.position, _radius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_cellcheck.position, _radius);
    }
   
    void Flip()
    {
        _faceRight = !_faceRight;
        transform.Rotate(0,180,0);
    }

    public void Move(float move, bool jump, bool crawling)
    { 
        if (casting)
            return;

        #region Movement
        if (move != 0 && (_grounded || _aircontroll))
        {
            _playerRB.velocity = new Vector2(_speed * move, _playerRB.velocity.y);
        }

        if (move > 0 && !_faceRight)
        {
            Flip();
        }
        else if (move < 0 && _faceRight)
        {
            Flip();
        }

        #endregion
        #region Jumping
        if (Time.timeScale == 0)
            return;
        _grounded = Physics2D.OverlapCircle(_groundcheck.position, _radius, _ground);

        if (jump && _grounded)
        {
            _playerRB.AddForce(Vector2.up * _jumpforse);
            
        }
        jump = false;
        #endregion
        #region Crawling
        _canstand = !Physics2D.OverlapCircle(_cellcheck.position, _radius, _ground);

        if (crawling)
        {
            _headcolider.enabled = false;
        }
        else if (!crawling && _canstand)
        {
            _headcolider.enabled = true;
        }
        #endregion
        #region Animation
        animator.SetFloat("speed", Mathf.Abs(move));
        animator.SetBool("jump", !_grounded);
        #endregion
    }
    #region Fireball
    public void StartCast()
    {
        if (casting || !playerHp.canAttack || !_grounded || Time.timeScale == 0)
        {
            return;
        }
        casting = true;
        playerHp.ChangeMp (-10);
        animator.SetBool("Cast", true);
    }

    public void castFireball()
    {
       GameObject _fireball = Instantiate(fireball, attack2.position, Quaternion.identity);
        _fireball.GetComponent<Rigidbody2D>().velocity = transform.right * (_speed + 1);
        _fireball.GetComponent<SpriteRenderer>().flipX = _faceRight;
        Destroy(_fireball, 20f);
    }

    private void endCast()
    {
        casting = false;
        animator.SetBool("Cast", false);
    }
    #endregion
}