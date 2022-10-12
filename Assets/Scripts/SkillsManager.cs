using UnityEngine;

public class SkillsManager : MonoBehaviour
{
    [Tooltip("Начальное число патронов")] 
    [SerializeField] private int bulletsCountNewValue = 30;
    
    [Tooltip("Макс. выносливость")] 
    [SerializeField] private float pigMaxStaminaNewValue = 40f;

    [Tooltip("Множитель скорости восст. стамины")] 
    [SerializeField] private float pigStaminaTimeNewValue = 0.8f;

    [Tooltip("Множитель скорости сбора")] 
    [SerializeField] private float pigPickingUpTimeNewValue = 0.5f;

    [Tooltip("Множитель скорости сбора")] 
    [SerializeField] private float collectorPickingUpTimeNewValue = 0.25f;
    
    public void AmmoBuff()
    {
        bulletsCountNewValue = PlayerPrefs.GetInt("DefaultBulletsCount") + 1;

        PlayerPrefs.SetInt("DefaultBulletsCount", bulletsCountNewValue);
    }

    public void StaminaBuff()
    {
        pigStaminaTimeNewValue = PlayerPrefs.GetFloat("DefaultPigStaminaTime") * 1.02f;

        PlayerPrefs.SetFloat("DefaultPigStaminaTime", pigStaminaTimeNewValue);
    }

    public void PickingUpBuff()
    {
        pigPickingUpTimeNewValue = PlayerPrefs.GetFloat("PigPickingUpTimeDivisor") * 0.98f;

        PlayerPrefs.SetFloat("PigPickingUpTimeDivisor", pigPickingUpTimeNewValue);
    }

    public void MaxStaminaBuff()
    {
        pigMaxStaminaNewValue = PlayerPrefs.GetFloat("DefaultPigMaxStamina") * 1.02f;

        PlayerPrefs.SetFloat("DefaultPigMaxStamina", pigMaxStaminaNewValue);
    }

    public void CollectorBuff()
    {
        collectorPickingUpTimeNewValue = PlayerPrefs.GetFloat("CollectorPickingUpTime") * 1.02f;

        PlayerPrefs.SetFloat("CollectorPickingUpTime", collectorPickingUpTimeNewValue);
    }

    public void RandomBuff()
    {
        int chance = Random.Range(1, 6);

        switch (chance)
        {
            case 1:
                AmmoBuff();
                break;
            case 2:
                StaminaBuff();
                break;
            case 3:
                PickingUpBuff();
                break;
            case 4:
                MaxStaminaBuff();
                break;
            case 5:
                CollectorBuff();
                break;
        }
    }
}
