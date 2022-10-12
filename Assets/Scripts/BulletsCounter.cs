using UnityEngine;
using TMPro;

public class BulletsCounter : MonoBehaviour
{
    [HideInInspector] public int defaultBulletsCount;
    public int bulletsCount;

    [HideInInspector] public bool isInfinite;

    [SerializeField] private TMP_Text countText;

    void Start()
    {
        defaultBulletsCount = PlayerPrefs.GetInt("DefaultBulletsCount", 15);
        bulletsCount = defaultBulletsCount;

        PlayerPrefs.SetInt("DefaultBulletsCount", defaultBulletsCount);

        countText.text = bulletsCount.ToString();
    }

    public void DecreaseBullets(int value)
    {
        if (!isInfinite)
        {
            bulletsCount -= value;
            countText.text = bulletsCount.ToString();
        }
    }

    public void IncreaseBullets(int value)
    {
        bulletsCount += value;
        if (!isInfinite) countText.text = bulletsCount.ToString();
    }

    public void InfiniteBullets()
    {
        countText.text ="∞";
        isInfinite = true;
    }

    public void DefaultBullets()
    {
        countText.text = bulletsCount.ToString();
        isInfinite = false;
    }
}
