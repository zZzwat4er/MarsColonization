using System.Numerics;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Game_Classes
{
    public class HandClicker
    {
        private BigInteger[] costs =
        {
            1000, 
            BigInteger.Parse("1000000000000000000000000000000000")
        };//цены на апгрейд хэндкликера

        private string[] names = {"New Tricks", "Endgame"};//их названя

        private int[] multipliers = {10,  100};//множитель для увеличения цены
        
        private int[] specCoef={0,  0};//специальный коэфициент для формулы
        private int coef; // коэфициент для формулы
        private BigInteger income; // доход с одного клика
        private BigInteger base_income;//базовый доход (по идее не нужен, но в формуле есть)
        private Building[] _buildings; //ссылки на здания
        
        
        public HandClicker(Building[] buildings)
        {
            //начальные значения
            coef = 1;
            _buildings = buildings;
            base_income = 1;
            income = 1;
            
        }

        public BigInteger getNextIncome(int index)
        {
            //здесь считаем всё по формуле данной в гуглдоке
            
            BigInteger res = 0;

            for (int i = 0; i < 14; ++i)
            {
                Debug.Log(i + " " + specCoef[1]);
                res += (specCoef[1] + (index == 1 ? 1 : 0)) * (_buildings[i].Lvl / 10);
            }

            res += (coef + (index == 0 ? 1 : 0) + res);
            return res;

        }

        public BigInteger Up(int index, BigInteger money)
        {
            //делаем апгрейд здания
            if (money >= costs[index]) //если денег достаточно, то делаем по формулам
            {
                income = getNextIncome(index);
                if (index == 0) coef++;
                specCoef[index]++;
                costs[index] *= multipliers[index];
                Debug.Log("Куплено " + income);
                return costs[index]/multipliers[index];//возвращаем сколько мы потратили, если хватило денег
            }
            Debug.Log("Не куплено");
            return 0;//возвращаем 0 если не хватило денег
        }
        
        //геттеры

        public BigInteger Income => income;

        public BigInteger[] Costs => costs;

        public string[] Names => names;
    }
}