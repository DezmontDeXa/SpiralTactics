using UnityEngine;

public class PigMovementDebuff : MonoBehaviour
{

    [SerializeField] private float debuffTime = 20f;
    private float timeCounter;

    private DraftPigMovement pigMovement;

    private void Start()
    {
        pigMovement = GetComponent<DraftPigMovement>();
    }

    private void Update()
    {
        if (timeCounter > 0) timeCounter -= Time.deltaTime;
        else if (pigMovement.underDebuff) Reset();
    }

    public void DoDebuff()
    { 
        Debug.Log("Do debuff movement");
        pigMovement.underDebuff = true;
        timeCounter = debuffTime;
        pigMovement.Stop();
        pigMovement.goButton.isLocked = true;
    }

    private void Reset()
    {
        Debug.Log("reset movement");
        pigMovement.underDebuff = false;
        pigMovement.isLocked = false;
        pigMovement.goButton.isLocked = false;
        pigMovement.SwitchRun();
    }
}
