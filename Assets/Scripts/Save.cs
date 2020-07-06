using System;
using System.Collections.Generic;
using System.Numerics;
using Game_Classes;
using UnityEngine;


[Serializable]
public class Save{
    public Building[] buildings; // Массив зданий для сейва
    public BigInteger money; // Текущие деньги
    public BigInteger gPrime; // премиум валюта
    public DateTime savedTime; // время выхода из игры
    public DateTime lastDailyReward; // дата получения последней награды
    //данные статистики
    public TimeSpan inGameTimeWhole, inGameTimeAfetrReset;
    public BigFloat totalG, totalGAfterReset, totalSpendG, totalSpendGAfterReset;
    public HandClicker handClicker;
    public int tensPower; // данные престижа
    public int[] manLVLs; // уровни менеджеров
    public List<Events> events;
    
    public Save (GameLogic _){ //, HandClicker handClick){
        buildings = _.Buildings;
        money = _.Money;
        gPrime = _.GPrime;
        events = _.events;
        manLVLs = new [] {UpgradeManagers.TimeLVL, UpgradeManagers.multiLVL};
        savedTime = DateTime.Now;
        inGameTimeWhole = Statistics.inGameTimeWhole;
        inGameTimeAfetrReset = Statistics.inGameTimeAfetrReset;
        totalG = Statistics.totalG;
        totalGAfterReset = Statistics.totalGAfterReset;
        totalSpendG = Statistics.totalSpendG;
        totalSpendGAfterReset = Statistics.totalSpendGAfterReset;
        handClicker = _.GetComponent<UpgradeHandClick>().HandClicker;
        tensPower = _.TensPower;
        if(DateTime.Now.Hour < 10)
            lastDailyReward = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day - 1, 10, 0, 0);
        else 
            lastDailyReward = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 10, 0, 0);
    }
}