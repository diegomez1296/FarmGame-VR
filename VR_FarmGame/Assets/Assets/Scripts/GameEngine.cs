using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour {

    public static double FarmValue;
    public static int CoordinateX;
    public static int CoordinateZ;

    [SerializeField] private GameObject ground;
    [HideInInspector] private BuildingsController[] buildingsControllers;

    private double moneyPerSec;

    private void Start()
    {
        FarmValue = 0; //LoadPrefs
        UIController.Instance.SetMoneyText(FarmValue);
        CoordinateX = -3;
        CoordinateZ = -4;
        moneyPerSec = 0;

        buildingsControllers = ground.GetComponentsInChildren<BuildingsController>();

        InvokeRepeating("GetProfit", 1, 1);
    }

    private void Update()
    {
        InfoPanelRaycast();

#if CHEATS
        Cheats();
#endif
    }

    private void GetProfit()
    {
        moneyPerSec = 0;

        foreach (var item in buildingsControllers)
        {
            //stats
            moneyPerSec += item.GetBuildingsAmount() * item.Profit;
        }
        FarmValue += moneyPerSec;
        
        UIUpdate();
    }

    private void UIUpdate()
    {
        UIController.Instance.SetBuildingText(BuildingType.FARM, buildingsControllers[0].GetBuildingsAmount());
        UIController.Instance.SetButtonInteractable(BuildingType.FARM, FarmValue >= buildingsControllers[0].Price);

        UIController.Instance.SetBuildingText(BuildingType.FACTORY, buildingsControllers[1].GetBuildingsAmount());
        UIController.Instance.SetButtonInteractable(BuildingType.FACTORY, FarmValue >= buildingsControllers[1].Price);

        UIController.Instance.SetBuildingText(BuildingType.BANK, buildingsControllers[2].GetBuildingsAmount());
        UIController.Instance.SetButtonInteractable(BuildingType.BANK, FarmValue >= buildingsControllers[2].Price);

        UIController.Instance.SetIncreaseText(moneyPerSec);
        UIController.Instance.SetMoneyText(FarmValue);
    }

    public void CreateBuildingOnClick(int type)
    {
        buildingsControllers[type].CreateBuilding(new Vector3(CoordinateX, 0, CoordinateZ));
        UIUpdate();
    }

    private void InfoPanelRaycast()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;

        Debug.DrawRay(ray.origin, ray.direction * 100);

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.GetComponentInParent<BuildingBase>())
            {
                BuildingBase building = hit.collider.GetComponentInParent<BuildingBase>();
                BuildingType type = building.type;
                float profit = buildingsControllers[(int)type].Profit;
                int amount = buildingsControllers[(int)type].GetBuildingsAmount();
                double income = profit * amount;
                double incomePercent = income / moneyPerSec;

                UIController.Instance.ShowInfoPanel(type, profit, amount, income, incomePercent);
            }
            else
            {
                UIController.Instance.InfoPanelVisibility(false);
            }
        }
        else
        {
            UIController.Instance.InfoPanelVisibility(false);
        }
    }

    private void Cheats()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) FarmValue += 1000;
    }
}
