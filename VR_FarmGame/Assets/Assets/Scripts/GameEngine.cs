using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour {

    public static double FarmValue;
    public static int CoordinateX;
    public static int CoordinateZ;

    [SerializeField] private GameObject ground;
    [HideInInspector] private BuildingsController[] buildingsControllers;

    private int X = -3;
    private int Y = -4;


    private void Start()
    {
        FarmValue = 0; //LoadPrefs
        CoordinateX = -3;
        CoordinateZ = -4;

        buildingsControllers = ground.GetComponentsInChildren<BuildingsController>();

        InvokeRepeating("GetProfit", 1, 1);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) buildingsControllers[0].CreateBuilding(new Vector3(CoordinateX, -0.1f, CoordinateZ));
        if (Input.GetKeyDown(KeyCode.Alpha2)) buildingsControllers[1].CreateBuilding(new Vector3(CoordinateX, -0.2f, CoordinateZ));
        if (Input.GetKeyDown(KeyCode.Alpha3)) buildingsControllers[2].CreateBuilding(new Vector3(CoordinateX, -0.4f, CoordinateZ));
    }

    private void GetProfit()
    {
        double moneyPerSec = 0;

        foreach (var item in buildingsControllers)
        {
            //stats
            moneyPerSec += item.GetBuildingsAmount() * item.GetPrototypeProfit();
        }
        FarmValue += moneyPerSec;

        Debug.Log("Farms: <b>" + buildingsControllers[0].GetBuildingsAmount() + "</b>");
        Debug.Log("Factories: <b>" + buildingsControllers[1].GetBuildingsAmount() + "</b>");
        Debug.Log("Banks: <b>" + buildingsControllers[2].GetBuildingsAmount() + "</b>");
        Debug.Log("Money / s: <b>" + moneyPerSec + "</b>");
        Debug.Log("Farm Value: <b>" + FarmValue +"</b>");
        Debug.Log("");
    }
}
