using TMPro;
using UnityEngine;

public class FruitTextManager : MonoBehaviour
{

    [SerializeField] private TMP_Text textR, textG, textB;

    [HideInInspector] public int countR, countG, countB;

    private void Start()
    {
        countR = PlayerPrefs.GetInt("RedFruitsCount", 0);
        countG = PlayerPrefs.GetInt("GreenFruitsCount", 0);
        countB = PlayerPrefs.GetInt("BlueFruitsCount", 0);

        textR.text = countR.ToString();
        textG.text = countG.ToString();
        textB.text = countB.ToString();
    }


    public void UpdateText()
    {
        textR.text = countR.ToString();
        textG.text = countG.ToString();
        textB.text = countB.ToString();
    }

    public void UpdatePrefs()
    {
        PlayerPrefs.SetInt("RedFruitsCount", countR);
        PlayerPrefs.SetInt("GreenFruitsCount", countG);
        PlayerPrefs.SetInt("BlueFruitsCount", countB);
    }
}
