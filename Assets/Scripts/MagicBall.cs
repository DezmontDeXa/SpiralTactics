using UnityEngine;

public class MagicBall : MonoBehaviour
{
    [SerializeField] private float speed = 60f;
    [SerializeField] private int damage = 2;

    private bool isFlying;

    private Vector3 targetPosition;

    private GameObject player;
    private PlayerHealth playerHealth;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
    }

    public void Fly()
    {
        targetPosition = player.transform.position;
        isFlying = true;
    }

    private void FixedUpdate()
    {
        if (isFlying) transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition) Destroy(gameObject);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out PlayerHealth playerHealth))
        {
            Destroy(gameObject);
            playerHealth.Damage(damage);
        }
    }
}
