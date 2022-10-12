using UnityEngine;

public class Book : MonoBehaviour
{
    [SerializeField] private GameObject[] _pages;

    public void PageSwitch(int pageNum)
    {
        for (int i = 0; i < _pages.Length; i++)
            _pages[i].SetActive(i == pageNum-1);
    }
}
