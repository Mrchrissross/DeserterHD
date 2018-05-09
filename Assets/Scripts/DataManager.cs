using UnityEngine;

public class DataManager : MonoBehaviour {

    private static int deaths, riskPoints, tempRiskPoints;
    private static string spawnPoint = "PlayerSpawnPoint1";

    public static int Deaths
    {
        get
        {
            return deaths;
        }
        set
        {
            deaths = value;
        }
    }

    public static int Points
    {
        get
        {
            return riskPoints;
        }
        set
        {
            riskPoints = value;
        }
    }

    public static int TempPoints
    {
        get
        {
            return tempRiskPoints;
        }
        set
        {
            tempRiskPoints = value;
        }
    }

    public static string SpawnLocation
    {
        get
        {
            return spawnPoint;
        }
        set
        {
            spawnPoint = value;
        }
    }
}
