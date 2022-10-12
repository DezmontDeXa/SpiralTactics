using UnityEngine;

public class PigStaminaBuff : MonoBehaviour
{
    
    [SerializeField] private float buffTime = 10;
    private float timeCounter;

    private DraftPigStamina pigStamina;

    private void Start()
    {
        pigStamina = GetComponent<DraftPigStamina>();
    }

    private void Update()
    {
        if (timeCounter > 0) timeCounter -= Time.deltaTime;
        else StopBuff();
    }

    public void DoBuff()
    {
        pigStamina.isInfinite = true;
        timeCounter = buffTime;
    }

    private void StopBuff()
    {
        pigStamina.isInfinite = false;
    }
}
