using System;
using System.Numerics;
using Game_Classes;
using UnityEngine;


[Serializable]
public class Save{
    public Building[] buildings; // Массив зданий для сейва
    public BigInteger money; // Текущие деньги
    public DateTime savedTime;
    
    public Save (Building[] buildings, BigInteger money, DateTime time){
        this.buildings = buildings;
        this.money = money;
        savedTime = time;
    }
}