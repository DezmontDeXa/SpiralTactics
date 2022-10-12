using System.Collections;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private const string PlayerTag = "Player";
    private const string CollectorTag = "Collector";

    [SerializeField] private float _collectorStanTime = 2f;
    [SerializeField] private int _playerDamageAmount = 100500;
    [SerializeField] private GameObject[] _baits;

    private void Awake()
    {
        DisableAllBaits();
        EnableRandomBaits();
    }

    private void DisableAllBaits()
    {
        foreach (var bait in _baits)
            bait.SetActive(false);
    }

    private void EnableRandomBaits()
    {
        _baits[Random.Range(0, _baits.Length)].SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == PlayerTag)
            KillPlayer(collision.collider.gameObject);

        if (collision.collider.tag == CollectorTag)
            StanCollector(collision.collider.gameObject);
    }

    private void KillPlayer(GameObject player)
    {
        player.GetComponent<PlayerHealth>().Damage(_playerDamageAmount);
    }

    private void StanCollector(GameObject collector)
    {
        var movement = collector.GetComponent<CollectorMovement>();
        movement.LockMovement();
        StartCoroutine(AwaitAndUnlockMovement(movement));
    }

    private IEnumerator AwaitAndUnlockMovement(CollectorMovement collectorMovement)
    {
        yield return new WaitForSeconds(_collectorStanTime);
        collectorMovement.UnlockMovement();
    }
}
