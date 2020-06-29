using System;
using System.Numerics;
using UnityEngine;
using static System.Math;

namespace Game_Classes
{
    [Serializable]
    public class Building
    {
        public string name; //Назвние здания
        public bool isActive = false; //true если в процессе заработка денег
        public TimeSpan startWorkAt; //время начала работы здания(нужно для прогресс бара)
        public bool isAvaliable; //true ели куплено
        public BigInteger base_cost, next_cost;//начальная цена, и цена на следующий апгрейд
        public BigInteger base_income;//базовый доход
        public BigInteger income;//доход на данный момент
        public Building dependent;//здание от которого будет зависеть апгрейд
        public float base_time, time; //базовое время на зароботок и текущие
        public float base_risk; //базовый риск
        public const float exp = 1.15f; //экспонента для апгрейда
        public int lvl;//уровень здания
        public BigInteger[] nextCommonUpgradeCost;//цена для покупки CommonUpgrade
        public float coef, spec_coef;//коэфициенты для подсчета прибыли с данного здания
        public BigInteger nextManagerCost; //цена для покупки следующего менеджера
        public int upgradeCount = 0;//сколько раз апргейднули менеджера
        
        public BigInteger nextIncome()//подсчет дохода при следующем апгрейде
        {
            return (BigInteger) (base_income * lvl * 
                                 (BigInteger)(coef * 10 + spec_coef * (dependent == null ? 0 : (int) (dependent.lvl / 10)))) / 10;
        }

        //функция пересчитывающая инкам при покупки аппов (изменения полей coef и spec_coef)
        public void recalculateIncome()
        {
            income = (base_income * lvl * 
                                  (BigInteger)(coef * 10 + spec_coef * (dependent == null ? 0 : (int) (dependent.lvl / 10)))) / 10;
        }

        public Building(string name, BigInteger baseCost, BigInteger baseIncome, float baseTime, float baseRisk, Building dependent = null)
        {
            this.nextManagerCost = baseCost*10;
            this.time = baseTime;
            this.isAvaliable = false;
            this.next_cost = baseCost;
            this.name = name;
            this.base_cost = baseCost;
            this.base_income = baseIncome;
            this.base_time = baseTime;
            this.base_risk = baseRisk;
            lvl = 0;
            income = 0;
            spec_coef = 0;
            coef = 1;
            nextCommonUpgradeCost = new [] { baseCost * 10, baseCost * 25, baseCost * 50 } ;
            this.dependent = dependent;
        }
        //(BigInteger)(base_cost * (BigInteger) Math.Pow(exp, lvl));
        public void upgrade()
        {
            income = nextIncome();
            ++lvl;
            next_cost = (base_cost* BigInteger.Pow(115, lvl+1))/BigInteger.Pow(100, lvl+1);
            
        }

        public float Time_
        {
            get => time;
            set => time = value;
        }

        public Building Dependent
        {
            get => dependent;
            set => dependent = value;
        }

        public BigInteger Income
        {
            get => income;
        }

        public int Lvl
        {
            get => lvl;
            
        }

        public string Name
        {
            get => name;
            
        }

        public float baseTime
        {
            get => base_time;
        }

        public BigInteger NextCost
        {
            get => next_cost;
            
        }

        public BigInteger[] NextCommonUpgradeCost
        {
            get => nextCommonUpgradeCost;
            set => nextCommonUpgradeCost = value;
        }

        public bool IsAvaliable
        {
            get => isAvaliable;
            set => isAvaliable = value;
        }
    }
}