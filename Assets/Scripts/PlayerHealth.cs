using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] private Animator ballAnimator;
    [SerializeField] private AudioSource audioSource;

    private DraftPigMovement pigMovement;

    private void Start()
    {
        pigMovement = GetComponent<DraftPigMovement>();
    }

    public void Damage(int value)
    {
        health -= value;
        if (health <= 0) Death();
    }

    private void Death()
    {
        Debug.Log("PLAYER IS DEAD");
        ballAnimator.SetTrigger("explosion");
        pigMovement.isDead = true;
        audioSource.Play();
        Invoke("LoadMenu", 1.14f);
    }

    private void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }
}
