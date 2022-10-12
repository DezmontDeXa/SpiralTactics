using System.Collections;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    private enum Type { redFruit, greenFruit, blueFruit, chest, goldenChest, water, stone, fioletChest };
    public float pickingUpTime = 2f;

    [SerializeField] private Type type;
    [SerializeField] private string collectorTag = "Collector";
    [SerializeField] private ParticleSystem _beginCarryFx; 
    [SerializeField] private ParticleSystem _digFx;
    [SerializeField] private float _fxTimeToDestroy = 3f;

    private int redFruitsCount, greenFruitsCount, blueFruitsCount;

    [HideInInspector] public bool busy;

    private GameObject player;
    private DraftPigMovement pigMovement;
    private DraftPigStamina pigStamina;
    private BulletsCounter bulletsCounter;
    private PickingUpBuff pickingUpBuff;
    private PigMovementDebuff pigMovementDebuff;
    private PigStaminaBuff pigStaminaBuff;
    private PigBulletsBuff pigBulletsBuff;
    private CollectorAttack collectorAttack;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        bulletsCounter = player.GetComponent<BulletsCounter>();
        pigMovement = player.GetComponent<DraftPigMovement>();
        pigStamina = player.GetComponent<DraftPigStamina>();
        pickingUpBuff = player.GetComponent<PickingUpBuff>();
        pigMovementDebuff = player.GetComponent<PigMovementDebuff>();
        pigStaminaBuff = player.GetComponent<PigStaminaBuff>();
        pigBulletsBuff = player.GetComponent<PigBulletsBuff>();
        collectorAttack = GameObject.FindGameObjectWithTag(collectorTag).GetComponent<CollectorAttack>();

        redFruitsCount = PlayerPrefs.GetInt("RedFruitsCount", 0);
        greenFruitsCount = PlayerPrefs.GetInt("GreenFruitsCount", 0);
        blueFruitsCount = PlayerPrefs.GetInt("BlueFruitsCount", 0);
    }


    public void PickUp()
    {
        gameObject.SetActive(false);
    }
    public void ApplyEffect()
    {
        switch (type)
        {
            case Type.redFruit:
                RedFruit();
                break;
            case Type.greenFruit:
                GreenFruit();
                break;
            case Type.blueFruit:
                BlueFruit();
                break;
            case Type.water:
                RestoreBullets(bulletsCounter.defaultBulletsCount / 2);
                break;
            case Type.chest:
                RestoreStamina();
                //float chance = Random.Range(0f, 1f);
                //if (chance >= 0.5f)
                //else if (chance >= 0.15f) RestoreBullets(bulletsCounter.defaultBulletsCount / 2);
                //else RandomFruit();
                break;
            case Type.goldenChest:
                if (Random.Range(0, 2) == 0)
                {
                    RestoreStamina();
                    RestoreBullets(bulletsCounter.defaultBulletsCount * 2);
                }
                else
                    DebuffMovement();
                //BuffPickingUp(true);
                break;
            case Type.stone:
                BuffPickingUp(false);
                if (Random.Range(0, 2) == 1)
                    InfiniteStamina();
                else
                    InfiniteBullets();
                CollectorBuff();
                break;

            case Type.fioletChest:
                CollectorBuff();
                break;

        }
    }
    public void PlayCarry()
    {
        BegingFx(_beginCarryFx);
    }
    public void PlayDig()
    {
        BegingFx(_digFx);
    }

    private void RestoreStamina(float increment = 1f)
    {
        Debug.Log("restore stamina");
        pigStamina.stamina = pigStamina.defaultStamina * increment;
    }
    private void RestoreBullets(int value)
    {
        Debug.Log("restore bullets");
        bulletsCounter.IncreaseBullets(value);
    }
    private void BuffPickingUp(bool isInstant)
    {
        pickingUpBuff.DoBuff(isInstant);
    }
    private void DebuffMovement()
    {
        pigMovementDebuff.DoDebuff();
        Debug.Log("Debuff movement");
    }
    private void InfiniteStamina()
    {
        pigStaminaBuff.DoBuff();
    }
    private void InfiniteBullets()
    {
        pigBulletsBuff.DoBuff();
    }
    private void CollectorBuff()
    {
        collectorAttack.DoBuff();
    }
    private void RandomFruit()
    {
        int chance = Random.Range(1, 4);
        switch (chance)
        {
            case 1:
                RedFruit();
                break;
            case 2:
                GreenFruit();
                break;
            case 3:
                BlueFruit();
                break;
        }
    }
    private void RedFruit()
    {
        redFruitsCount++;
        PlayerPrefs.SetInt("RedFruitsCount", redFruitsCount);
    }
    private void GreenFruit()
    {
        greenFruitsCount++;
        PlayerPrefs.SetInt("GreenFruitsCount", greenFruitsCount);
    }
    private void BlueFruit()
    {
        blueFruitsCount++;
        PlayerPrefs.SetInt("BlueFruitsCount", blueFruitsCount);
    }  
    private void BegingFx(ParticleSystem fx)
    {
        var fxGo = Instantiate(fx);
        fxGo.transform.position = transform.position;
        StartCoroutine(AwainAndDestroy(_fxTimeToDestroy, fxGo));
    }
    private IEnumerator AwainAndDestroy(float time, ParticleSystem fx)
    {
        yield return new WaitForSeconds(time);
        if(fx!=null)
            Destroy(fx.gameObject);
    }
}
