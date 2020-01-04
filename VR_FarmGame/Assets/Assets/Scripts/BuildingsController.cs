using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsController : MonoBehaviour {

    [SerializeField] private BuildingType type;
    [SerializeField] private BuildingBase prototype;
    [HideInInspector] public BuildingBase[] buildings;

    private float profit;
    private float price;
    private float basicPrice;

    private void Start()
    {
        switch (type)
        {
            case BuildingType.FARM:
                profit = 1;
                price = 20;
                break;
            case BuildingType.FACTORY:
                profit = 10;
                price = 400;
                break;
            case BuildingType.BANK:
                profit = 100;
                price = 6000;
                break;
            default:
                break;
        }
        basicPrice = price;
    }

    public int GetBuildingsAmount()
    {
        buildings = GetComponentsInChildren<BuildingBase>();
        return buildings.Length;
    }

    public float GetPrototypeProfit()
    {
        return profit;
    }

    public void CreateBuilding(Vector3 newPosition)
    {
        if (GameEngine.FarmValue >= price)
        {
            BuildingBase newBuilding = Instantiate(prototype, this.transform);
            newBuilding.transform.position = newPosition * 10;

            GameEngine.CoordinateX++;
            if(GameEngine.CoordinateX > 4)
            {
                GameEngine.CoordinateX = -4;
                GameEngine.CoordinateZ++;
            }

            GameEngine.FarmValue -= price;
            CurrentPrototypePrice();
        }
        Debug.LogError(type + ": " + price);
    }

    private void CurrentPrototypePrice()
    {
        price = GetBuildingsAmount() * GetBuildingsAmount() * profit + basicPrice;
    }
}
