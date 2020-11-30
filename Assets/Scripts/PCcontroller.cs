using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Playermovement))] 
public class PCcontroller : MonoBehaviour
{
    Playermovement _playermovement;
    float _move;
    bool _jump;
    bool _crawling;

    private void Start()
    {
        _playermovement = GetComponent<Playermovement>();
    }

    void Update()
    {
        _move = Input.GetAxisRaw("Horizontal");
        if (Input.GetButtonUp("Jump"))
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
