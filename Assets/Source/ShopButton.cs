using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Visuals")]
    [SerializeField] private string defaultText = "button";
    [SerializeField] private string highlightedText = "> button <";
    [SerializeField] private int defaultFontSize = 100;
    [SerializeField] private int highlightedFontSize = 120;
    [SerializeField] private Color defaultTextColor = Color.gray;
    [SerializeField] private Color highlightedTextColor = Color.white;
    [Header("Mechanics")]
    [SerializeField] private int unitID;
    [SerializeField] private ShopButtonType type;

    private TextMeshProUGUI text;
    private Button button;

    private bool isMouseOver = false;

    private float price = 0f;

    void Awake()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick);
    }

    private void Start()
    {
        price = UnitManager.Instance.GetPrice(unitID);
    }

    private void Update()
    {
        if (isMouseOver)
        {
            text.text = highlightedText + "\n(" + price + ")";
            text.fontSize = highlightedFontSize;
            text.color = highlightedTextColor;
        }
        else
        {
            text.text = defaultText + "\n(" + price + ")";
            text.fontSize = defaultFontSize;
            text.color = defaultTextColor;
        }
    }

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        isMouseOver = true;

        AudioManager.Instance.PlaySound("ButtonHover");
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        isMouseOver = false;
    }

    private void OnButtonClick()
    {
        if (type == ShopButtonType.Buy)
        {
            UnitManager.Instance.Buy(unitID);
        }
        else if (type == ShopButtonType.Sell)
        {
            UnitManager.Instance.Sell(unitID);
        }

        AudioManager.Instance.PlaySound("ButtonClick");
    }
}

public enum ShopButtonType
{
    Buy,
    Sell
}