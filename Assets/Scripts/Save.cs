using System;
using System.Numerics;
using Game_Classes;
using UnityEngine;


[Serializable]
public class Save{
    public Building[] buildings; // Массив зданий для сейва
    public BigInteger money; // Текущие деньги
    public DateTime savedTime;
    //public HandClicker handClicker; TODO: раскоментить
    
    public Save (Building[] buildings, BigInteger money, DateTime time){ //, HandClicker handClick){
        this.buildings = buildings;
        this.money = money;
        savedTime = time;
        //this.handClicker = handClick; TODO: тут
    }
}