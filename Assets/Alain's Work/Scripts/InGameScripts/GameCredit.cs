using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCredit : MonoBehaviour
{
    [HideInInspector] public static GameCredit Instance;

    //current money of the player
    [HideInInspector] public static float gameMoney = 10000;

    public void Awake()
    {
        //assigns the one instance
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            //destroys the duplicate gameObject 
            Destroy(gameObject);
        }
    }


    public static void addCurrency(float setMoney)
    {
        gameMoney += setMoney;
    }

    public static void DeductCurrency(float setMoney)
    {
        gameMoney -= setMoney;
    }

    public static void addCurrencyAds(float setMoney)
    {
        gameMoney -= setMoney;
    }

    public static float getCurrency()
    {
        return gameMoney;
    }
    public static void resetCurrency()
    {
        gameMoney = 0;
    }
}

