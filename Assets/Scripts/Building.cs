using System;
using System.Numerics;
using UnityEngine;
using static System.Math;

namespace Game_Classes
{
    public class Building
    {
        private string name;
        public bool isActive = false;
        private bool isAvaliable;
        private BigInteger base_cost, next_cost;
        private BigInteger base_income;
        private BigInteger income;
        private Building dependent;
        private float base_time, time;
        private float base_risk;
        private const float exp = 1.15f;
        private int lvl;
        private float coef, spec_coef;
        public BigInteger nextManagerCost;
        public int upgradeCount = 0;
        
        public BigInteger nextIncome()
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