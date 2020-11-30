using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class fireballTrap : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject fireball;
    [SerializeField] private Transform attack1;
    [SerializeField] private Transform attack2;
    [SerializeField] private float cd;
    private float nextat;
    private float nextAt;
    private bool faceright = true;


    private void Update()
    {
        if (nextat <= Time.time)
            CastFireball1();
        if (nextAt <= Time.time)
            CastFireball2();
        
    }

    private void CastFireball1()
    {

        GameObject _fireball = Instantiate(fireball, attack1.position, Quaternion.identity);
        _fireball.GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        _fireball.GetComponent<SpriteRenderer>().flipX = faceright;
        nextat = Time.time + cd;
        nextAt = Time.time + 2f;
        Destroy(_fireball, 20f);
    }

    private void CastFireball2()
    {
        GameObject _fireball = Instantiate(fireball, attack2.position, Quaternion.identity);
        _fireball.GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        _fireball.GetComponent<SpriteRenderer>().flipX = faceright;
        Destroy(_fireball, 20f);
        nextAt = Time.time + cd;
    }
}
