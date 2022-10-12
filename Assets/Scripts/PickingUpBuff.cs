using UnityEngine;

public class PickingUpBuff : MonoBehaviour
{

    [SerializeField] private float instantBuffTime = 7f;
    [SerializeField] private float buffTime = 10f;
    private float currentTime;
    private float pigPickingUpDivisorDefault;

    private bool isActive;

    private PickingUp pickingUp;

    private void Start()
    {
        pickingUp = GameObject.FindGameObjectWithTag("Player").GetComponent<PickingUp>();
        pigPickingUpDivisorDefault = pickingUp.pigPickingUpTimeDivisor;
    }

    public void DoBuff(bool isInstant)
    {
        if (isInstant) 
        {
            pickingUp.pigPickingUpTimeDivisor = 1000f;
            currentTime = instantBuffTime;
        }
        else
        {
            pickingUp.pigPickingUpTimeDivisor /= 2f;
            currentTime = buffTime;
        } 

        isActive = true;
    }

    private void StopBuff()
    {
        isActive = false;
        pickingUp.pigPickingUpTimeDivisor = pigPickingUpDivisorDefault;
    }

    private void Update()
    {
        if (currentTime > 0 && isActive) currentTime -= Time.deltaTime;
        else if (currentTime <= 0 && isActive) StopBuff();
    }

}
