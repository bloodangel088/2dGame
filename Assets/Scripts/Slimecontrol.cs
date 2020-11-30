using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slimecontrol : MonoBehaviour
{
    [SerializeField] private float idleTime;

    private bool inAttackRange;

    [Header("Attack")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private int damage;
    [SerializeField] private float attackRange;
    [SerializeField] private LayerMask enemies;



}
