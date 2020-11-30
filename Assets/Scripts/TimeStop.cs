using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeStop : MonoBehaviour
{
    [SerializeField] private float upVelocity;
    [SerializeField] private float flyTimeSeconds;
    [SerializeField] private GameObject gameMenu;
    private Rigidbody2D rigidbody;
    private DateTime startTime;

    private void Start()
    {
        gameMenu = FindObjectOfType<Menu>().gameObject;
        rigidbody = GetComponent<Rigidbody2D>();
        startTime = DateTime.Now;
    }

    private void FixedUpdate()
    {
        if ((DateTime.Now - startTime).TotalSeconds >= flyTimeSeconds)
        {
            rigidbody.AddForce(new Vector2(0, upVelocity));
        }
        else
        {
            gameMenu.SetActive(true);
            Destroy(gameObject);
        }
    }
}
