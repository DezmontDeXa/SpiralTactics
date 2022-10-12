using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {

    [SerializeField] private Canvas canvas;
    [SerializeField] private Transform dragPoint1, dragPoint2;
    [SerializeField] private FruitTab fruitTab;
    [SerializeField] private FruitTextManager fruitTextManager;
    [SerializeField] private float dropDistance = 1f;

    private bool in1Cell, in2Cell;
    private bool green, red, blue;
    private Vector2 defaultPosition;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private FruitButtonType fruitButtonType;
    private Button button;

    private void Awake() 
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        fruitButtonType = GetComponent<FruitButtonType>();
        button = GetComponent<Button>();
    }

    private void Start()
    {
        defaultPosition = rectTransform.localPosition;

        if (fruitButtonType.type == FruitButtonType.Type.green) green = true;
        else if (fruitButtonType.type == FruitButtonType.Type.red) red = true;
        else blue = true;
    }

    public void OnBeginDrag(PointerEventData eventData) 
    {
        canvasGroup.alpha = .6f;
        canvasGroup.blocksRaycasts = false;
        if (in1Cell)
        {
            in1Cell = false;
            fruitTab.point1Used = false;
        }
        else if (in2Cell)
        {
            in2Cell = false;
            fruitTab.point2Used = false;
        }
        ResetColor();
    }

    public void OnDrag(PointerEventData eventData) 
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData) 
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;

        float distanceTo1Point = Vector2.Distance(transform.position, dragPoint1.position);
        float distanceTo2Point = Vector2.Distance(transform.position, dragPoint2.position);

        if (distanceTo1Point <= dropDistance) DropToPoint(1);
        else if (distanceTo2Point <= dropDistance) DropToPoint(2);
        else DropToPoint(0);   
    }

    private void DropToPoint(int index)
    {
        if (index == 1 && !fruitTab.point1Used) 
        {
            transform.position = dragPoint1.position;
            fruitTab.point1Used = true;
            in1Cell = true;
            in2Cell = false;

            SetColor();
            CheckIfActivated();
        }
        else if (index == 2 && !fruitTab.point2Used)
        {
            transform.position = dragPoint2.position;
            fruitTab.point2Used = true;
            in2Cell = true;
            in1Cell = false;

            SetColor();
            CheckIfActivated();
        } 
        else
        {
            rectTransform.localPosition = defaultPosition;

            if (fruitTab.point1Used && in1Cell)
            {
                fruitTab.point1Used = false;
                in1Cell = false;
            } 
            else if (fruitTab.point2Used && in2Cell)
            {
                fruitTab.point2Used = false;
                in2Cell = false;
            } 
            ResetColor();
        } 
    }

    private void SetColor()
    {
        if (green) fruitTab.green = true;
        else if (red) fruitTab.red = true;
        else fruitTab.blue = true;
    }
    private void ResetColor()
    {
        if (green) fruitTab.green = false;
        else if (red) fruitTab.red = false;
        else fruitTab.blue = false;

        //rectTransform.localPosition = defaultPosition;
    }

    private void LockDragging()
    {
        button.interactable = false;
        this.enabled = false;

        rectTransform.localPosition = defaultPosition;
    }

    private void CheckIfActivated()
    {
        if ((transform.position == dragPoint1.position || transform.position == dragPoint2.position) && (fruitTab.point1Used && fruitTab.point2Used))
        {
            rectTransform.localPosition = defaultPosition;
            in1Cell = false;
            in2Cell = false;
        } 
    }

    private void Update()
    {
        if (in1Cell || in2Cell) CheckIfActivated();

        if (red && fruitTextManager.countR == 0)
        {
            LockDragging();
        } 
        else if (green && fruitTextManager.countG == 0)
        {
            LockDragging();
        } 
        else if (blue && fruitTextManager.countB == 0)
        {
            LockDragging();
        }

        if(fruitTab.red && red && fruitTextManager.countR == 1 && !in1Cell && !in2Cell) LockDragging();
        else if(fruitTab.green && green && fruitTextManager.countG == 1 && !in1Cell && !in2Cell) LockDragging();
        else if(fruitTab.blue && blue && fruitTextManager.countB == 1 && !in1Cell && !in2Cell) LockDragging();
    }
}
