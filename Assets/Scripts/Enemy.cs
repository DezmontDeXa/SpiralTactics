using UnityEngine;

public class Enemy : MonoBehaviour
{
    
    [SerializeField] private int health = 1;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Damage(int value)
    {
        health -= value;
        if(health == 0) Death();
    }

    private void Death()
    {
        animator.SetTrigger("death");
        Invoke("DestroyObject", 1f);
    }

    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
