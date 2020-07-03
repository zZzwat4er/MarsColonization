using System;
using System.Numerics;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Game_Classes
{
    [Serializable]
    public class HandClicker
    {
        public BigInteger[] costs =
        {
            15,
            1000, 
            BigInteger.Parse("1000000000000000000000000000000000")
        };//цены на апгрейд хэндкликера

        public string[] names = {"MO' Money", "New Tricks", "Endgame"};//их названя

        public float[] multipliers = {1.5f, 10,  50};//множитель для увеличения цены
        
        public int[] specCoef={0, 0,  0};//специальный коэфициент для формулы
        public int coef; //коэфициент для формулы
        public int amt;
        public BigInteger income; // доход с одного клика
        public BigInteger base_income;//базовый доход (по идее не нужен, но в формуле есть)
        public Building[] _buildings; //ссылки на здания
        
        
        public HandClicker(Building[] buildings)
        {
            //начальные значения
            coef = 1;
            _buildings = buildings;
            base_income = 1;
            income = 1;
            amt = 1;

        }

        public BigInteger getNextIncome(int index)
        {
            //здесь считаем всё по формуле данной в гуглдоке
            
            BigInteger res = 0;
            BigInteger lvls = 0;
            for (int i = 0; i < 14; ++i)
            {
                // Debug.Log(i + " " + specCoef[1]);
                // res += (specCoef[2] + (index == 2 ? 1 : 0)) * (_buildings[i].Lvl / 10);
                lvls += _buildings[i].Lvl;
            }
            
            res = (coef + (index == 1 ? 1 : 0) + (specCoef[2] + (index == 2? 1 :0))*lvls/10)*(amt + (index == 0? 1:0));
            return res;
        }

        public BigInteger Up(int index, BigInteger money)
        {
            //делаем апгрейд здания
            if (money >= costs[index]) //если денег достаточно, то делаем по формулам
            {
                income = getNextIncome(index);
                if (index == 1) coef++;
                if (index == 0) amt++;
                specCoef[index]++;
                BigInteger  ret = BigInteger.Parse(costs[index].ToString());
                costs[index] *= (int)(multipliers[index]*10);
                costs[index] /= 10;
                // Debug.Log("Куплено " + income);
                return ret;//возвращаем сколько мы потратили, если хватило денег
            }
            // Debug.Log("Не куплено");
            return 0;//возвращаем 0 если не хватило денег
        }
        
        //геттеры

        public BigInteger Income => income;

        public BigInteger[] Costs => costs;

        public string[] Names => names;
    }
}