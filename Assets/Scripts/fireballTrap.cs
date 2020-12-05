using System.Collections;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;

public class fireballTrap : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject fireball;
    [SerializeField] private Transform[] _firePoints;
    [SerializeField] private float _delayBetweenShots;
    [SerializeField] private bool faceright = true;

    private int _currentSP_index = 0;
    private Transform CurrentSP => _firePoints[_currentSP_index];
    private bool _shooting;

    public void SetNextSP()
    {
        if (_currentSP_index + 1 < _firePoints.Length)
        {
            _currentSP_index++;
        }
        else
        {
            _currentSP_index = 0;
        }
    }

    private void Update()
    {
        if (!_shooting)
        {
            StartCoroutine(SpawnProjectile(fireball, CurrentSP.position));
        }
    }

    private IEnumerator SpawnProjectile(GameObject projectile, Vector3 position)
    {
        _shooting = true;

        GameObject spawned = Instantiate(projectile, position, Quaternion.identity);
        spawned.GetComponent<Rigidbody2D>().velocity = transform.right * speed;
        if (!faceright)
            spawned.GetComponent<Rigidbody2D>().transform.Rotate(new Vector3(0f, 180f, 0f));

        Destroy(spawned, 20f);

        yield return new WaitForSeconds(_delayBetweenShots);
        _shooting = false;
        SetNextSP();
    }
}
