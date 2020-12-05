using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Playermovement))] 
public class PCcontroller : MonoBehaviour
{
    [SerializeField] private GameObject _menuEffect;
    [SerializeField] private Transform _menuEffectSP;
    private bool _menuActivated;

    Playermovement _playermovement;
    float _move;
    bool _jump;
    bool _crawling;
    bool canMove = true;

    public void ResetMenuActivated()
    {
        _menuActivated = false;
        canMove = true;
    }

    private void Start()
    {
        _playermovement = GetComponent<Playermovement>();
    }

    public void SpawnTimeStop()
    {
        Instantiate(_menuEffect, _menuEffectSP.position, Quaternion.identity);
    }

    void Update()
    {
        if(!_menuActivated && Input.GetKeyDown(KeyCode.Escape))
        {
            _menuActivated = true;
            canMove = false;
            GetComponent<Animator>().Play("Attack1");
        }

        if (!canMove)
        {
            _jump = false;
            _move = 0;
            _crawling = false;
            return;
        }

        _move = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            _jump = true;
        }
        _crawling = Input.GetKey(KeyCode.LeftControl);
        if (Input.GetButton("Fire1"))
        {
            _playermovement.StartCast();
        }
    }

    private void FixedUpdate()
    {
        _playermovement.Move(_move, _jump, _crawling);
        _jump = false;
    }
}
