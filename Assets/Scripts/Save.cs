using System;
using System.Numerics;
using Game_Classes;
using UnityEngine;


[Serializable]
public class Save{
    public Building[] buildings; // Массив зданий для сейва
    public BigInteger money; // Текущие деньги
    public BigInteger gPrime;
    public DateTime savedTime;
    public DateTime lastDailyReward;
    //данные статистики
    public TimeSpan inGameTimeWhole, inGameTimeAfetrReset;
    public BigFloat totalG, totalGAfterReset, totalSpendG, totalSpendGAfterReset;
    public HandClicker handClicker;
    public int tensPower;
    
    public Save (Building[] buildings, BigInteger money, BigInteger gPrime, DateTime time, HandClicker handClick, int tensPower){ //, HandClicker handClick){
        this.buildings = buildings;
        this.money = money;
        this.gPrime = gPrime;
        savedTime = time;
        inGameTimeWhole = Statistics.inGameTimeWhole;
        inGameTimeAfetrReset = Statistics.inGameTimeAfetrReset;
        totalG = Statistics.totalG;
        totalGAfterReset = Statistics.totalGAfterReset;
        totalSpendG = Statistics.totalSpendG;
        totalSpendGAfterReset = Statistics.totalSpendGAfterReset;
        this.handClicker = handClick;
        this.tensPower = tensPower;
        if(DateTime.Now.Hour < 10)
            lastDailyReward = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1, 10, 0, 0);
        else 
            lastDailyReward = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0);
    }
}