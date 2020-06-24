using System;
using System.Numerics;
using UnityEngine;
using static System.Math;

namespace Game_Classes
{
    public class Building
    {
        private string name; //Назвние здания
        public bool isActive = false; //true если в процессе заработка денег
        public TimeSpan startWorkAt; //время начала работы здания(нужно для прогресс бара)
        private bool isAvaliable; //true ели куплено
        private BigInteger base_cost, next_cost;//начальная цена, и цена на следующий апгрейд
        private BigInteger base_income;//базовый доход
        private BigInteger income;//доход на данный момент
        private Building dependent;//здание от которого будет зависеть апгрейд
        private float base_time, time; //базовое время на зароботок и текущие
        private float base_risk; //базовый риск
        private const float exp = 1.15f; //экспонента для апгрейда
        private int lvl;//уровень здания
        private float coef, spec_coef;//коэфициенты для подсчета прибыли с данного здания
        public BigInteger nextManagerCost; //цена для покупки следующего менеджера
        public int upgradeCount = 0;//сколько раз апргейднули менеджера
        
        public BigInteger nextIncome()//подсчет дохода при следующем апгрейде
        {
            return (BigInteger) (base_income *
                                 (BigInteger) (coef + spec_coef * (dependent == null ? 0 : (int) (dependent.lvl / 10))
                                 )) * (1+lvl);
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

        public BigFloat Income
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
        

        public bool IsAvaliable
        {
            get => isAvaliable;
            set => isAvaliable = value;
        }
    }
}