using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private int gameScenIndex = 1;

    [SerializeField] private GameObject mainMenu, settingsMenu, skillsMenu, bookButtons, bookPages, backButton;

    [SerializeField] private AudioMixer mixer;
    [SerializeField] private Slider slider;

    private void Start()
    {
        mixer.SetFloat("SoundVol", Mathf.Log10(PlayerPrefs.GetFloat("SoundVolume", 1)) * 20);

        slider.value = PlayerPrefs.GetFloat("SoundVolume", 1);
    }

    public void ResetPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

    public void StartGame()
    {
        SceneManager.LoadScene(gameScenIndex);
    }

   
    public void CloseSettings()
    {
        mainMenu.SetActive(true);
        settingsMenu.SetActive(false);
    }

    public void BackButton()
    {
       mainMenu.SetActive(true);
       settingsMenu.SetActive(false);
       skillsMenu.SetActive(false);
       bookButtons.SetActive(false);
       bookPages.SetActive(false);

       backButton.SetActive(false);
    }

    public void OpenSkills()
    {
        mainMenu.SetActive(false);
        skillsMenu.SetActive(true);

        backButton.SetActive(true);
    } 

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);

        backButton.SetActive(true);
    }
     
    public void OpenBook()
    {
        mainMenu.SetActive(false);
        bookButtons.SetActive(true);
        bookPages.SetActive(true);

        backButton.SetActive(true);
    }


    public void SetVolumeLevel ()
    {
        float sliderValue = slider.value;
        mixer.SetFloat("SoundVol", Mathf.Log10(sliderValue) * 20);
        if (sliderValue == 0)  mixer.SetFloat("SoundVol", -80);

        PlayerPrefs.SetFloat("SoundVolume", sliderValue);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
