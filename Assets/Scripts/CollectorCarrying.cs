using UnityEngine;

public class CollectorCarrying : MonoBehaviour
{
    
    [SerializeField] private Transform carryingPoint1, carryingPoint2;
    [SerializeField] private Transform draftPig;

    [SerializeField] private float handOverRadius = 10f;

    [HideInInspector] public bool isCarrying;

    private bool isCarryingFruit;

    private Fruit fruit;

    private Pickup pickupScript;
    
    void Update()
    {
        if(Vector3.Distance(transform.position, draftPig.position) <= handOverRadius && isCarrying)
        {
            HandOverSupply();
        }
    }

    public void StartCarrying(Transform pickup)
    {
        if(!isCarrying)
        {
            pickupScript = pickup.GetComponent<Pickup>();

            fruit = pickup.GetComponentInChildren<Fruit>();

            if (fruit != null)
            {
                pickup.gameObject.SetActive(false);
                fruit.transform.SetParent(carryingPoint2);
                fruit.transform.position = carryingPoint2.position;

                isCarryingFruit = true;
            }
            else
            {
                pickup.SetParent(carryingPoint1);
                pickup.position = carryingPoint1.position;
            }

            pickupScript.enabled = false;
            isCarrying = true;
        }
    }

    private void HandOverSupply()
    {
        Debug.Log("Hand over");

        if (isCarryingFruit)
        {
            Destroy(fruit.gameObject, 0.5f);
            isCarryingFruit  = false;
        } 

        isCarrying = false;
        pickupScript.ApplyEffect();
        pickupScript.PickUp();

        Destroy(pickupScript.gameObject, 0.5f);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, handOverRadius);
    }
}
