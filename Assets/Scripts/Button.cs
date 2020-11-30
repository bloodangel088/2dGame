using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Sprite presed;
    [SerializeField] private GameObject door;
    private bool activated;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (activated)
            return;
        GetComponent<SpriteRenderer>().sprite = presed;
        activated = true;
        Destroy(door);
    }
}
