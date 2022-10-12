using Player.Collector;
using UnityEngine;

public class CollectorPickingUp : MonoBehaviour
{

    [SerializeField] private float radius;
    [SerializeField] private LayerMask whatIsPickable;
    [SerializeField] private Transform point;

    private float currentTime;

    [Tooltip("Чем больше, тем быстрее сбор")] 
    [Range(0.1f, 2.0f)] 
    public float collectorPickingUpTimeDivisor;

    private bool isPickingUp;

    private CollectorMovement collectorMovement;
    private CollectorCarrying collectorCarrying;
    private Pickup currentPickup;
    private Animator animator;
    private AnimationEventHandlers _animationEvents;

    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        _animationEvents = GetComponentInChildren<AnimationEventHandlers>();
        collectorMovement = GetComponent<CollectorMovement>();
        collectorCarrying = GetComponent<CollectorCarrying>();

        collectorPickingUpTimeDivisor = PlayerPrefs.GetFloat("CollectorPickingUpTime", 0.5f);

        PlayerPrefs.SetFloat("CollectorPickingUpTime", collectorPickingUpTimeDivisor);
    }

    private void OnEnable()
    {
        _animationEvents.OnDig += OnDig;
    }

    private void OnDisable()
    {
        _animationEvents.OnDig -= OnDig;
    }

    private void Update()
    {
        Collider[] sphereCollider = Physics.OverlapSphere(point.position, radius, whatIsPickable);

        if (sphereCollider.Length != 0 && !isPickingUp && !collectorCarrying.isCarrying) 
        {
            currentPickup = sphereCollider[0].gameObject.GetComponent<Pickup>();
            Debug.Log("Collector picking up");
            StartPickingUp();
        }


        if (currentTime > 0 && isPickingUp)
        {
            currentTime -= Time.deltaTime;
        }
        else if (currentTime <= 0 && isPickingUp) StopPickingUp();
    }

    private void OnDig()
    {
        currentPickup?.PlayDig();
    }

    private void StartPickingUp()
    {
        if (currentPickup.busy) return;

        isPickingUp = true;

        currentPickup.busy = true;
        currentPickup.PlayDig();

        currentTime = currentPickup.pickingUpTime * collectorPickingUpTimeDivisor;
        collectorMovement.LockMovement();
        animator.SetBool("isPickingUp", true);
    }

    private void StopPickingUp()
    {
        isPickingUp = false;

        currentPickup.PlayCarry();

        collectorCarrying.StartCarrying(currentPickup.transform);
        collectorMovement.UnlockMovement();
        animator.SetBool("isPickingUp", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(point.position, radius);
    }
}
