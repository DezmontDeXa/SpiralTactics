using UnityEngine;

public class CollectorAttack : MonoBehaviour
{

    [SerializeField] private float attackRadius = 20f;
    [SerializeField] private int damage = 1;

    [SerializeField] private float startBuffTime = 10f;
    private float buffTime;

    private bool didAttack = true;

    public void DoBuff()
    {
        buffTime = startBuffTime;
        didAttack = false;
    }

    private void Update()
    {
        if (!didAttack) Attack();

        if (buffTime > 0) buffTime -= Time.deltaTime;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    private void Attack()
    {
        Collider[] sphereCollider = Physics.OverlapSphere(transform.position, attackRadius);
        if (sphereCollider != null)
        {
            for (int i = 0; i < sphereCollider.Length; i++)
            {
                if(sphereCollider[i].TryGetComponent(out Enemy enemy))
                {
                    enemy.Damage(damage);
                    Debug.Log("Attack");
                    didAttack = true;
                } 
            }
            
        }
      
    }
}
