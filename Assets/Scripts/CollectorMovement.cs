using UnityEngine;

public class CollectorMovement : MonoBehaviour
{
    [SerializeField] private float speed = 30f;

    private bool isRunning;
    private bool isLocked;

    private Vector3 pointPosition;

    private Animator animator;

    private void Start()
    {
        animator = GetComponentInChildren<Animator>();
    }

    
    private void Update()
    {
        if (isRunning) transform.position = Vector3.MoveTowards(transform.position, pointPosition, speed * Time.deltaTime);

        if (transform.position == pointPosition) isRunning = false;

        animator.SetBool("isRunning", isRunning);
    }

    public void RunToPoint(Vector3 position)
    {
        if(isLocked) return;

        pointPosition = new Vector3(position.x, transform.position.y, position.z);
        isRunning = true;
        //transform.parent.LookAt(pointPosition);

    }

    public void LockMovement()
    {
        isRunning = false;
        isLocked = true;
    }
    
    public void UnlockMovement()
    {
        isLocked = false;
    }

}
