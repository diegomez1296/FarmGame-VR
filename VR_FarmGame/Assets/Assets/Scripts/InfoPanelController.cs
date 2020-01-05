using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoPanelController : MonoBehaviour {

    [SerializeField] private Text type;
    [SerializeField] private Text profit;
    [SerializeField] private Text amount;
    [SerializeField] private Text income;
    [SerializeField] private Text incomePercent;

    public void SetInfoPanel(BuildingType type, float profit, int amount, double income, double incomePercent)
    {
        switch (type)
        {
            case BuildingType.FARM:
                this.type.color = new Color32(255, 166, 8, 255);
                break;
            case BuildingType.FACTORY:
                this.type.color = new Color32(44, 190, 238, 255);
                break;
            case BuildingType.BANK:
                this.type.color = new Color32(192, 192, 192, 255);
                break;
            default:
                break;
        }

        this.type.text = type + "";
        this.profit.text = "Profit: " + profit;
        this.amount.text = "Amount: " + amount;
        this.income.text = "Income: " + income;
        this.incomePercent.text = System.Math.Round(incomePercent*100,2) + "%";
    }
}
