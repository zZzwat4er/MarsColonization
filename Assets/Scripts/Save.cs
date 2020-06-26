using System;
using Game_Classes;
using UnityEngine;


[Serializable]
public class Save{
    public Building[] buildings; // Массив зданий для сейва
    public BigFloat money; // Текущие деньги
    public DateTime savedTime;
    public TimeSpan inGameTimeWhole, inGameTimeAfetrReset;
    public BigFloat totalG, totalGAfterReset, totalSpendG, totalSpendGAfterReset;
    
    public Save (Building[] buildings, BigFloat money, DateTime time){
        this.buildings = buildings;
        this.money = money;
        savedTime = time;
        inGameTimeWhole = Statistics.inGameTimeWhole;
        inGameTimeAfetrReset = Statistics.inGameTimeAfetrReset;
        totalG = Statistics.totalG;
        totalGAfterReset = Statistics.totalGAfterReset;
        totalSpendG = Statistics.totalSpendG;
        totalSpendGAfterReset = Statistics.totalSpendGAfterReset;
    }
}