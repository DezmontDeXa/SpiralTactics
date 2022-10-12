using UnityEngine;

public class LanguageChanger : MonoBehaviour
{

    [SerializeField] private GameObject textRu, textEn;

    public void Set_RU()
    {
        PlayerPrefs.SetString("GameLanguage", "RU");
        Debug.Log("Language changed to RUSSIAN");

        textRu.SetActive(true);
    }

    public void Set_EN()
    {
        PlayerPrefs.SetString("GameLanguage", "EN");
        Debug.Log("Language changed to ENGLISH");

        textEn.SetActive(true);
    }
}