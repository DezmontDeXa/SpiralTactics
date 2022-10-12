using UnityEngine;

public class DirectionButton : MonoBehaviour
{
  
    [SerializeField] private DraftPigMovement pigMovement;

    public void ChangeDirection(int direction)
    {
        if ((pigMovement.transform.position.x >= 95f && direction == 1) || (pigMovement.transform.position.x <= -100f && direction == -1)) return;
        pigMovement.verticalDirection = direction;
    }
}
