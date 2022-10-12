using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GoButtonSwitch : MonoBehaviour
{
    public bool isLocked;

    [SerializeField] private DraftPigMovement pigMovement;

    private TMP_Text buttonText;
    [HideInInspector] public Button button;

    private void Start()
    {
        buttonText = GetComponentInChildren<TMP_Text>();
        button = GetComponent<Button>();
    }

    public void ChangeText()
    {
        if(!isLocked)
        {
            if (pigMovement.isRunning) buttonText.text = "hold";
            else buttonText.text = "Go";
        }

    }
}
