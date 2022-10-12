using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class Localizator : MonoBehaviour
{

    public string id;

    void Awake()
    {
        Localize();
    }

    public void Localize()
    {
        if (PlayerPrefs.HasKey("GameLanguage"))
        {
            string GameLanguage = PlayerPrefs.GetString("GameLanguage");
            ChangeText(LocalizedText(id, GameLanguage));
        }
        else
        {
            ChangeText(LocalizedText(id, "EN"));
        }
    }

    private void ChangeText(string new_text)
    {
        GetComponent<TMP_Text >().text = new_text;
    }
  
    private string LocalizedText(string id, string lang)
    { 
        TextAsset mytxtData=(TextAsset)Resources.Load("localization/localization");
        string loc_txt=mytxtData.text;
        string[] rows = loc_txt.Split('\n');
        for (int i = 1; i < rows.Length; i++)
        {
            string[] cuted_row = Regex.Split(rows[i], ";");
            if(id == cuted_row[0])
            {
                if(lang == "RU")
                {
                    return cuted_row[1];
                }
                else if(lang == "EN")
                {
                    return cuted_row[2];
                }
                break;
            }
        }
        return "translation error";
    }   
}
