using System;
using Game_Classes;
using UnityEngine;


[Serializable]
public class Save{
    public Building[] buildings; // Массив зданий для сейва
    public BigFloat money; // Текущие деньги
    public DateTime savedTime;
    
    public Save (Building[] buildings, BigFloat money, DateTime time){
        this.buildings = buildings;
        this.money = money;
        savedTime = time;
    }
}