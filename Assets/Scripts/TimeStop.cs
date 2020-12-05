using UnityEngine;

public class TimeStop : MonoBehaviour
{
    [SerializeField] private float upVelocity;
    [SerializeField] private float flyTimeSeconds;
    private Menu gameMenu;
    private Rigidbody2D rigidbody;

    private void Start()
    {
        gameMenu = FindObjectOfType<Menu>();
        rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rigidbody.AddForce(new Vector2(0, upVelocity));
        Destroy(gameObject, flyTimeSeconds);
    }

    private void OnDestroy()
    {
        gameMenu.ToggleMenu();
    }
}
