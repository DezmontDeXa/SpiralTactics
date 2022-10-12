using System;
using System.Collections;
using UnityEngine;

public class PlayersBullet : MonoBehaviour
{

    [HideInInspector] public Transform target;

    [SerializeField] private float speed = 50f;
    [SerializeField] private int damage = 1;
    [SerializeField] private ParticleSystem _onDestroyFx;
    [SerializeField] private float _timeToDestroy = 5f;

    [HideInInspector] public Vector3 targetPosition;

    void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition) Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.Damage(damage);

            var fx = Instantiate(_onDestroyFx, transform.position, transform.rotation);
            fx.Play(true);

            StartCoroutine(AwaitDestroy(_timeToDestroy, gameObject, fx.gameObject));
        }
    }

    private IEnumerator AwaitDestroy(float timeToDestroy, params GameObject[] objects)
    {
        yield return new WaitForSeconds(timeToDestroy);
        foreach (var obj in objects)
            Destroy(obj);
    }
}
