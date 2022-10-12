using UnityEngine;

public class DestroyPlatforms : MonoBehaviour
{
    [SerializeField] private float maxDistanceToPlayer = 300;

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Vector3.Distance(player.transform.position, transform.position) >= maxDistanceToPlayer) Destroy(gameObject);
    }
}
