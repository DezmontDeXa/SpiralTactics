using UnityEngine;

public class DraftPigMovement : MonoBehaviour
{
    
    public float speed;
    [HideInInspector] public bool isRunning;
    [HideInInspector] public int verticalDirection = 0;

    [HideInInspector] public Animator animator;
    public GoButtonSwitch goButton;
    private DraftPigStamina pigStamina;

    public bool isLocked;
    public bool underDebuff;
    public bool isDead;

    void Start()
    {
        animator = GetComponent<Animator>();
        pigStamina = GetComponent<DraftPigStamina>();
    }

    void Update()
    {
        if (pigStamina.stamina <= 0 || isDead)
        {
           Stop();
        }
        else if (!isLocked && !underDebuff)
        {
            goButton.isLocked = false;
            goButton.button.interactable = true;
        } 
    }

    void FixedUpdate()
    {
        if (isRunning) transform.Translate(new Vector3 (verticalDirection, 0, 1) * speed * Time.deltaTime);

        if (transform.position.x >= 88f || transform.position.x <= -100f) verticalDirection = 0;
    }
    public void Stop()
    {
       
        Debug.Log("Stop");
        isRunning = false;
        goButton.ChangeText();
        goButton.isLocked = true;
        goButton.button.interactable = false;

        animator.SetBool("isRunning", false);
        
    }

    public void SwitchRun()
    {
        if (!isLocked)
        {
            if(!isRunning)
            {
                isRunning = true;
                goButton.button.interactable = true;
                goButton.isLocked = false;
            }
            else
            {
                isRunning = false;
            }
            goButton.ChangeText();

            animator.SetBool("isRunning", isRunning);
        }
    }

}
