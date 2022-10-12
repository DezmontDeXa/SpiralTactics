using UnityEngine;

public class PickingUp : MonoBehaviour
{
    
    [SerializeField] private float radius;
    [SerializeField] private Transform point;
    [SerializeField] private LayerMask whatIsPickable;

    [Range(0.5f, 5.0f)]
    [Tooltip("Чем больше, тем медленнее сбор предметов")] 
    public float pigPickingUpTimeDivisor;

    private bool isPickingUp;
    private float currentTime;
    private Pickup currentPickup;
    private DraftPigMovement pigMovement;

    private void Start()
    {
        pigMovement = GetComponent<DraftPigMovement>();

        pigPickingUpTimeDivisor = PlayerPrefs.GetFloat("PigPickingUpTimeDivisor", pigPickingUpTimeDivisor);
        PlayerPrefs.SetFloat("PigPickingUpTimeDivisor", pigPickingUpTimeDivisor);
    }

    private void Update()
    {
        Collider[] sphereCollider = Physics.OverlapSphere(point.position, radius, whatIsPickable);
        if (sphereCollider.Length != 0 && !isPickingUp) 
        {
            currentPickup = sphereCollider[0].gameObject.GetComponent<Pickup>();
            StartPickingUp();
        }

        if (currentTime > 0 && isPickingUp) currentTime -= Time.deltaTime;
        else if (currentTime <= 0 && isPickingUp) StopPickingUp();
    }

    private void StartPickingUp()
    {
        if (currentPickup.busy) return;

        isPickingUp = true;
        currentPickup.busy = true;

        currentTime = currentPickup.pickingUpTime * pigPickingUpTimeDivisor;
        
        pigMovement.Stop();
        pigMovement.isLocked = true;
        pigMovement.animator.SetBool("isPickingUp", true);
    }

    private void StopPickingUp()
    {
        isPickingUp = false;

        currentPickup.PickUp();
        currentPickup.ApplyEffect();

        if(!pigMovement.underDebuff)
        {
            pigMovement.isLocked = false;
            pigMovement.goButton.isLocked = false;
            pigMovement.SwitchRun();
        }
        pigMovement.animator.SetBool("isPickingUp", false);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(point.position, radius);
    }
}
