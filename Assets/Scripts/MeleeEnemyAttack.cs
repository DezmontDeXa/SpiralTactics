using UnityEngine;

[RequireComponent(typeof(Enemy))]
[RequireComponent(typeof(EnemyMovement))]

public class MeleeEnemyAttack : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    private Animator animator;
    private EnemyMovement enemyMovement;
    private PlayerHealth playerHealth;

    private void Start()
    {
        animator = GetComponent<Animator>();
        enemyMovement = GetComponent<EnemyMovement>();
        playerHealth = enemyMovement.player.GetComponent<PlayerHealth>();
    }

    public void Attack()
    {
        if (!enemyMovement.attackAnimationIsPlaying) 
            animator.SetTrigger("attack");
    }

    public void DoDamage(int value)
    {
        playerHealth.Damage(value);
    }

    public void DoDefaultDamage()
    {
        playerHealth.Damage(_damage);
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
