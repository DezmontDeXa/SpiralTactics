using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(EnemyMovement))]

public class WizardAttack : MonoBehaviour
{

    [SerializeField] private float spawnPeriod = 20f;
    [SerializeField] private GameObject elementalPrefab;
    [SerializeField] private GameObject magicBallPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform castPoint;

    private float currentPeriod;

    private GameObject currentBall;
    
    private Animator animator;
    private EnemyMovement enemyMovement;
    private PlayerHealth playerHealth;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        playerHealth = enemyMovement.player.GetComponent<PlayerHealth>();

        currentPeriod = spawnPeriod;
    }

    private void Update()
    {
        if (currentPeriod > 0)
        {
            currentPeriod -= Time.deltaTime;
        } 
        else
        {
            Spawn();
            currentPeriod = spawnPeriod;
        }
    }
    
    public void Attack()
    {
        if (!enemyMovement.attackAnimationIsPlaying) animator.SetTrigger("attack");
    }

    public void CastBall()
    {
        currentBall = Instantiate(magicBallPrefab, castPoint.position, Quaternion.identity);
        currentBall.transform.SetParent(this.transform);
    }

    public void ThrowBall()
    {
        currentBall.GetComponent<MagicBall>().Fly();
    }

    public void CancelCast()
    {
        if (currentBall != null){

            Destroy(currentBall);
        }
    }

    private void Spawn()
    {
        Instantiate(elementalPrefab, spawnPoint.position, Quaternion.identity);
    }

    public void LockAttack()
    {
        enemyMovement.attackAnimationIsPlaying = true;
    }

    public void UnlockAttack()
    {
        enemyMovement.attackAnimationIsPlaying = false;
    }
}
