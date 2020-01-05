using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsController : MonoBehaviour {

    [SerializeField] private BuildingType type;
    [SerializeField] private BuildingBase prototype;
    [HideInInspector] public BuildingBase[] buildings;

    public float Profit { get; private set; }
    public float Price { get; private set; }
    private float basicPrice;

    private void Start()
    {
        switch (type)
        {
            case BuildingType.FARM:
                Profit = 1;
                Price = 20;
                break;
            case BuildingType.FACTORY:
                Profit = 10;
                Price = 400;
                break;
            case BuildingType.BANK:
                Profit = 100;
                Price = 6000;
                break;
            default:
                break;
        }
        basicPrice = Price;
        UIController.Instance.SetBuildingPrice(type, Price);
        UIController.Instance.SetButtonInteractable(type, GameEngine.FarmValue >= Price);
    }

    public int GetBuildingsAmount()
    {
        buildings = GetComponentsInChildren<BuildingBase>();
        return buildings.Length;
    }

    public void CreateBuilding(Vector3 newPosition)
    {
        if (GameEngine.FarmValue >= Price)
        {
            BuildingBase newBuilding = Instantiate(prototype, this.transform);
            newBuilding.transform.position = newPosition * 10;

            GameEngine.CoordinateX++;
            if(GameEngine.CoordinateX > 4)
            {
                GameEngine.CoordinateX = -4;
                GameEngine.CoordinateZ++;
            }

            GameEngine.FarmValue -= Price;
            CurrentPrototypePrice();
        }
    }

    private void CurrentPrototypePrice()
    {
        Price = GetBuildingsAmount() * GetBuildingsAmount() * Profit + basicPrice;
        UIController.Instance.SetBuildingPrice(type, Price);
    }
}
