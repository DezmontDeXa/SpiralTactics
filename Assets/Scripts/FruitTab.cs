using UnityEngine;

public class FruitTab : MonoBehaviour
{
    
    [SerializeField] private Transform point1, point2;

    [SerializeField] private SkillsManager skillsManager;
    [SerializeField] private FruitTextManager fruitTextManager;

    public bool point1Used, point2Used;

    public bool red, green, blue;

    void Update()
    {
        if (point1Used && point2Used)
        {
            ActivateBuff();
        }
    }

    void ActivateBuff()
    {
        if (blue && green)
        {
            skillsManager.AmmoBuff();
            fruitTextManager.countB--;
            fruitTextManager.countG--;
        } 
        else if (blue && red)
        {
            skillsManager.StaminaBuff();
            fruitTextManager.countB--;
            fruitTextManager.countR--;
        } 
        else if (red && green)
        {
            skillsManager.RandomBuff();
            fruitTextManager.countR--;
            fruitTextManager.countG--;
        } 
        else if (blue)
        {
            skillsManager.PickingUpBuff();
            fruitTextManager.countB -= 2;
        } 
        else if (red)
        {
            skillsManager.MaxStaminaBuff();
            fruitTextManager.countR -= 2;
        } 
        else if (green)
        {
            skillsManager.CollectorBuff();
            fruitTextManager.countG -= 2;
        } 

        point1Used = false;
        point2Used = false;

        fruitTextManager.UpdatePrefs();
        fruitTextManager.UpdateText();

        red = false;
        green = false;
        blue = false;
    }
}
