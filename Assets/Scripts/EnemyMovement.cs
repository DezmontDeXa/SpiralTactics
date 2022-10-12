using UnityEngine;

[RequireComponent(typeof(Enemy))]

public class EnemyMovement : MonoBehaviour
{

    [SerializeField] private float speed = 20f;
    [SerializeField] private float visionRadius = 150f;
    [SerializeField] private float attackRadius = 20f;
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private bool isMelee;
    [SerializeField] private bool stopWhenAttacking;
    [SerializeField] private Transform raycastPoint;
    [SerializeField] private float raycastLength = 5f;
    [SerializeField] private LayerMask obstacleLayer;

    [HideInInspector] public GameObject player;
    [HideInInspector] public bool attackAnimationIsPlaying;

    private bool isLocked;
    private bool hasObstacle;

    private Animator animator;
    private MeleeEnemyAttack meleeEnemyAttack;
    private WizardAttack wizardAttack;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag(playerTag);
    }
    
    private void Start()
    {
        animator = GetComponent<Animator>();
        if(isMelee) meleeEnemyAttack = GetComponent<MeleeEnemyAttack>();
        else wizardAttack = GetComponent<WizardAttack>();
        
    }

   
    private void FixedUpdate()
    {
        RaycastHit hit;

        Vector3 forward = transform.TransformDirection(Vector3.forward);

        hasObstacle = Physics.Raycast(raycastPoint.position, forward * raycastLength, out hit, obstacleLayer );

        if(hasObstacle) Debug.Log(hit.transform.gameObject.name);

        if(isLocked) return;
      
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if (!hasObstacle && distanceToPlayer <= visionRadius && (distanceToPlayer > attackRadius || !stopWhenAttacking) && !attackAnimationIsPlaying) 
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        else if (distanceToPlayer <= attackRadius)
        {
            if (isMelee) meleeEnemyAttack.Attack();
            else wizardAttack.Attack();
        }

        LookAtPlayer();  
    }

    private void LookAtPlayer()
    {
        transform.LookAt(player.transform);
        Debug.Log("Looking at player");
    }

    public void LockMovement()
    {
        isLocked = true;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, visionRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRadius);

        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(raycastPoint.position, forward * raycastLength);
    }
}
