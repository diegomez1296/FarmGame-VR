using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public static UIController Instance;

    [SerializeField] private Text money;
    [SerializeField] private Text increase;
    [SerializeField] private Text farms;
    [SerializeField] private Button farmBuyButton;
    [SerializeField] private Text factories;
    [SerializeField] private Button factoryBuyButton;
    [SerializeField] private Text banks;
    [SerializeField] private Button bankBuyButton;

    [SerializeField] private InfoPanelController infoPanel;

    private void Awake()
    {
        if (Instance == null) Instance = GetComponent<UIController>();
    }

    private void Start()
    {
        InfoPanelVisibility(false);
    }

    public void SetMoneyText(double amount)
    {
        money.text = "Money: " + amount;
    }

    public void SetIncreaseText(double amount)
    {
        increase.text = amount + "/s";
    }

    public void SetBuildingText(BuildingType type, float amount)
    {
        switch (type)
        {
            case BuildingType.FARM:
                farms.text = "Farms: " + amount;
                break;
            case BuildingType.FACTORY:
                factories.text = "Factories: " + amount;
                break;
            case BuildingType.BANK:
                banks.text = "Banks: " + amount;
                break;
            default:
                break;
        }    
    }

    //Price
    public void SetBuildingPrice(BuildingType type, float amount)
    {
        switch (type)
        {
            case BuildingType.FARM:
                farmBuyButton.GetComponentInChildren<Text>().text = amount + "";
                break;
            case BuildingType.FACTORY:
                factoryBuyButton.GetComponentInChildren<Text>().text = amount + "";
                break;
            case BuildingType.BANK:
                bankBuyButton.GetComponentInChildren<Text>().text = amount + "";
                break;
            default:
                break;
        }        
    }

    //Color
    public void SetButtonInteractable(BuildingType type, bool canBuy)
    {
        switch (type)
        {
            case BuildingType.FARM:
                ButtonOptions(farmBuyButton, canBuy);
                break;
            case BuildingType.FACTORY:
                ButtonOptions(factoryBuyButton, canBuy);
                break;
            case BuildingType.BANK:
                ButtonOptions(bankBuyButton, canBuy);
                break;
            default:
                break;
        }
    }

    private void ButtonOptions(Button btn, bool canBuy)
    {
        btn.interactable = canBuy;
        var colors = btn.colors;
        colors.normalColor = canBuy == true ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.5f);
        colors.disabledColor = canBuy == true ? new Color(0, 1, 0, 0.5f) : new Color(1, 0, 0, 0.5f);
        colors.highlightedColor = canBuy == true ? new Color(0, 1, 0, 1f) : new Color(1, 0, 0, 1f);
        colors.pressedColor = canBuy == true ? new Color(0, 1, 0, 1f) : new Color(1, 0, 0, 1f);
        btn.colors = colors;
    }

    public void InfoPanelVisibility(bool value)
    {
        infoPanel.gameObject.SetActive(value);
    }

    public void ShowInfoPanel(BuildingType type, float profit, int amount, double income, double incomePercent)
    {
        infoPanel.SetInfoPanel(type, profit, amount, income, incomePercent);
        InfoPanelVisibility(true);
    }
}
