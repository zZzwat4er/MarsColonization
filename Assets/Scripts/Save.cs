using System;
using System.Numerics;
using Game_Classes;
using UnityEngine;


[Serializable]
public class Save{
    public Building[] buildings; // Массив зданий для сейва
    public BigInteger money; // Текущие деньги
    public DateTime savedTime;
    //данные статистики
    public TimeSpan inGameTimeWhole, inGameTimeAfetrReset;
    public BigFloat totalG, totalGAfterReset, totalSpendG, totalSpendGAfterReset;
    public HandClicker handClicker;
    public int tensPower;
    
    public Save (Building[] buildings, BigInteger money, DateTime time, HandClicker handClick, int tensPower){ //, HandClicker handClick){
        this.buildings = buildings;
        this.money = money;
        savedTime = time;
        inGameTimeWhole = Statistics.inGameTimeWhole;
        inGameTimeAfetrReset = Statistics.inGameTimeAfetrReset;
        totalG = Statistics.totalG;
        totalGAfterReset = Statistics.totalGAfterReset;
        totalSpendG = Statistics.totalSpendG;
        totalSpendGAfterReset = Statistics.totalSpendGAfterReset;
        this.handClicker = handClick;
        this.tensPower = tensPower;
    }
}