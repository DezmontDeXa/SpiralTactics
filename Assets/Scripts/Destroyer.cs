using UnityEngine;

public class Destroyer : MonoBehaviour
{

    [SerializeField] private float time = 5f;

    void Start()
    {
        Destroy(gameObject, time);
    }

}
