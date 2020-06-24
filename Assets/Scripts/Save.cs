using System;
using Game_Classes;


[Serializable]
public class Save{
    public Building[] buildings; // Массив зданий для сейва
    public BigFloat money; // Текущие деньги
    
    public Save (Building[] buildings, BigFloat money){
        this.buildings = buildings;
        this.money = money;
    }
}