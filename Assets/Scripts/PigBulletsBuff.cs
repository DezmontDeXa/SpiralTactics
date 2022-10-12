using UnityEngine;

public class PigBulletsBuff : MonoBehaviour
{

    [SerializeField] private float buffTime = 10;
    private float timeCounter;

    private BulletsCounter bulletsCounter;

    private void Start()
    {
        bulletsCounter = GetComponent<BulletsCounter>();
    }

    private void Update()
    {
        if (timeCounter > 0) timeCounter -= Time.deltaTime;
        else StopBuff();
    }

    public void DoBuff()
    {
        bulletsCounter.InfiniteBullets();
        timeCounter = buffTime;
    }

    private void StopBuff()
    {
        bulletsCounter.DefaultBullets();
    }
}
