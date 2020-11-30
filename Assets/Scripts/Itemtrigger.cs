using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType
    {
        Health,Mana
    }

public class Itemtrigger : MonoBehaviour
{
    [SerializeField] private int value;
    [SerializeField]private ItemType Type;
  private void OnTriggerEnter2D(Collider2D info)
    {
        switch (Type)
        {
            case ItemType.Health:
                info.gameObject.GetComponent<PlayerHp>().ChangeHp(value);
                break;
            case ItemType.Mana:
                info.gameObject.GetComponent<PlayerHp>().ChangeMp(value);
                break;
        }
        Destroy(gameObject);
    }
}
