using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    private LvLManager lvlManager;

    [SerializeField] private int maxMp;
    [SerializeField] private int maxHp;
    [SerializeField] private Slider Hp;
    [SerializeField] private TMP_Text Hp_text;
    [SerializeField] private Slider Mp;
    [SerializeField] private TMP_Text Mp_text;

    private Animator animator;
    public bool canAttack =>(currentMp >=10);
    private int currentHp;
    private int currentMp;

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHp = maxHp;
        currentMp = maxMp;
        RefreshHud();
        lvlManager = LvLManager.Instanse;
    }
    private void RefreshHud()
    {
        Hp.value = currentHp;
        Hp_text.text = $"{((currentHp<0)?0:currentHp)}/{maxHp}";
        Mp.value = currentMp;
        Mp_text.text = $"{currentMp}/{maxMp}";
    }
    public void ChangeHp(int value)
    {
        if(value < 0)
        {
            animator.SetBool("Hit", true); 
        }
        

        currentHp += value;
        if (currentHp> maxHp)
        {
            currentHp = maxHp;
        }
        else if(currentHp <=0)
        {
            Death();
        }
        RefreshHud();
        
    }

    public void EndHit()
    {
        animator.SetBool("Hit", false);
    }
        
    public void ChangeMp(int value)
    {
        currentMp += value;
        if (currentMp > maxMp)
        {
            currentMp = maxMp;
        }
        RefreshHud();

    }
    public void Death()
    {
        lvlManager.Restart();
        //animator.SetBool("Death", true);
        //Destroy(gameObject, 1f);
    }

    private void RestartLvL()
    {
        lvlManager.Restart();
    }
}
