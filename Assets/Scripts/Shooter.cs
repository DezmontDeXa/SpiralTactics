using UnityEngine;
using UnityEngine.UI;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform shootPosition;
    [SerializeField] private BulletsCounter bulletsCounter;

    [SerializeField] private string enemiesTag = "Enemy";

    private Vector3 closestEnemyPosition;

    private Animator animator;

    private Transform closestEnemy;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private Transform GetClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemiesTag);
        Transform bestTarget = null;
        float closestDistanceSqr = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach(GameObject potentialTarget in enemies)
        {
            Vector3 directionToTarget = potentialTarget.transform.position - currentPosition;
            float dSqrToTarget = directionToTarget.sqrMagnitude;
            if(dSqrToTarget < closestDistanceSqr)
            {
                closestDistanceSqr = dSqrToTarget;
                bestTarget = potentialTarget.transform;
            }
        }
        return bestTarget;
        
    }

    public void StartShooting()
    {
        if(bulletsCounter.bulletsCount <= 0) return;
        
        if(bulletsCounter.bulletsCount > 0)
        {
            closestEnemy = GetClosestEnemy();
        
            if(closestEnemy != null)
            {
                animator.SetTrigger("shoot");
                closestEnemyPosition = closestEnemy.position;

                LookAtEnemy();
            } 
            else Debug.Log("NO ENEMIES");
        }
      
    }

    public void Shoot()
    {
        if(bulletsCounter.bulletsCount <= 0) return;
        
        closestEnemy = GetClosestEnemy();

        if(closestEnemy != null)
        {
            GameObject bulletObject;
            bulletObject = Instantiate(bullet, shootPosition.position, Quaternion.identity);
            bulletObject.GetComponent<PlayersBullet>().targetPosition = closestEnemyPosition;

            bulletsCounter.DecreaseBullets(1);
        } 
      
    }

    private void LookAtEnemy()
    {
        transform.LookAt(closestEnemy);
    }
}
