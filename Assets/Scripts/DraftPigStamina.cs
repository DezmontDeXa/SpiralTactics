using UnityEngine;
using UnityEngine.UI;

public class DraftPigStamina : MonoBehaviour
{
    [HideInInspector] public float defaultStamina;
    [HideInInspector] public float stamina;

    [HideInInspector] public bool isInfinite;

    [SerializeField] private Slider slider;

    private float timeDivisor;

    private DraftPigMovement pigMovement;

    void Start()
    {
        pigMovement = GetComponent<DraftPigMovement>();

        

        defaultStamina = PlayerPrefs.GetFloat("DefaultPigStamina", 20f);
        timeDivisor = PlayerPrefs.GetFloat("DefaultPigStaminaTime", 0.4f);

        PlayerPrefs.SetFloat("DefaultPigStamina", defaultStamina);
        PlayerPrefs.SetFloat("DefaultPigStaminaTime", timeDivisor);
        

        stamina = defaultStamina;
        slider.maxValue = stamina;
    }

    
    void Update()
    {
        if(!isInfinite)
        {
            if (pigMovement.isRunning) stamina -= Time.deltaTime;
            else if (stamina < slider.maxValue) stamina += Time.deltaTime * timeDivisor;
        }

        slider.value = stamina;
    }
}
