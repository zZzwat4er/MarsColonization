using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using Game_Classes;
using UnityEngine.Assertions.Must;

public class GameLogic : MonoBehaviour
{
    //Нужные объекты
    private GameObject[] buildingsImage;//массив зданий, количество регулерутеся переменой number_of_buildings
    private Building[] _buildings; //сами объекты зданий
    
    
    
    /*Информция о зданиях
     ******************************************************************/
    private BigInteger[] base_cost =
    {
        100, 1200, 15000, 125000, 1500000, 20000000, 330000000, 5100000000, 75000000000, BigInteger.Parse("1000000000000"), 
        BigInteger.Parse("14000000000000"), BigInteger.Parse("170000000000000"), BigInteger.Parse("2100000000000000"), 
        BigInteger.Parse("20000000000000000")
    }; //базовая цена за здание
    private BigInteger[] base_income =
    {
        1, 10, 60, 500, 1500, 8000, 44000, 250000, 1600000, 10000000, 65000000, 450000000, 3000000000, 20000000000
    };//базовый доход здания

    private float[] base_time = {1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1,};//базове время для дохода
    private string[] names = { "Convenience Store", "Gas Station", "Chop Shop", "Pharmacy", "Gun Store", 
        "Lawyer Office", "Nightclub", "Laboratory", "Weapon Manufacture", "Police Station", "Red Mine", 
        "Governmental Building", "Gates of Hell", "Planet Core"};
    private int[] depedents = {1, 2, -1, 7, 8, 6, -1, 11, 9, -1, 12, -1, -1, 13}; //-1 нет апгрейда, любой другое - номер здания от которого зависит апгрейд
    private float[] risks = { 0, 0, 5, 5, 10, -10, 5, 10, 15, -20, 0, -30, 30, 0};
    /**********************************************************************/
    
    //Геттеры и сеттеры
    public Building[] Buildings => _buildings;
    public int TensPower => tensPower;
    public int NumberOfBuildings => number_of_buildings; 
    public int CurrentBuilding => current_building;
    public BigInteger Money
    {
        get => money;
        set => money = value;
    }

    

    [Header("Инфа здания")]
    [SerializeField] private Text NameText,  MoneyText, TimeText, MonetsText;
    [SerializeField] private Button BuyButton;
    [SerializeField] private Text HeadText, InfoText, lvlText;
    [SerializeField] private Sprite img;
    [Header("Настройки")]
    [SerializeField] private float animation_duration = 2f;
    [SerializeField] private GameObject last, current, next;//фантомный вспомогательные здания
    [SerializeField] private GameObject buildings_panel;
    [SerializeField] private int number_of_buildings;//количество зданий



    [Header("Инфа hand кликера")] [SerializeField]
    private Text handClickInfoText;

        //переменные для работы и совершения вычислений
    private int current_building = 0;
    private BigInteger money= 0;
    private DateTime pauseTime = DateTime.Now;//переменная сохраняющая время паузы для timeSkip если игрок не закроет игру при сворачивании
    private int tensPower = 5;
    
    
    [Header("Message Box")][SerializeField]
    private GameObject msgShower;

    void Awake()
    {
        
       Debug.Log("GameLogic In");
       
       
       
        current_building = 0;//ставим текущие здание как 0
        buildingsImage = new GameObject[number_of_buildings];//инициализируем
            //Создаем самое первое здание, которое будет стоять по середине экрана
        buildingsImage[0] = Instantiate(current, current.transform.position, Quaternion.identity);
        buildingsImage[0].transform.SetParent(buildings_panel.transform);
        buildingsImage[0].transform.localScale= new Vector3(1, 1, 1);
        buildingsImage[0].GetComponent<Image>().sprite = img;//ставим изображение
        for (int i = 1; i < number_of_buildings; ++i)
        {
            //остальные здания будут справа, как следующие
            buildingsImage[i] = Instantiate(next, next.transform.position, Quaternion.identity) as GameObject;
            buildingsImage[i].transform.SetParent(buildings_panel.transform);
            buildingsImage[i].transform.localScale= new Vector3(1, 1, 1);
            buildingsImage[i].GetComponent<Image>().sprite = img;//ставим изображение
            //прикручиваем прогресс бары нашим зданиям
            // buildingsImage[i].GetComponentInChildren<Image>().transform.localScale = Vector3.zero;
            
        }

        //пробуем загрузить сейв и если он есть подкачиваем от туда инфу
        
        Save save = SaveSystem.load();
        if (save != null)
        {
            _buildings = save.buildings;
            money = save.money;
            GetComponent<UpgradeHandClick>().HandClicker = save.handClicker;
            timeSkip(save.savedTime);
            Statistics.statLoad(save);
            return;
        }
        else
        {
            buildingsInit();
        }

        update_info();
        
        Debug.Log("GameLogic Out");
    }

   

    public void update_info()
    {
        _buildings[0].recalculateIncome();
        
        //Функция обновляют всю информацию игры
        //Ее нужно всегда вызывать после всяческих изменений
        
        /*изменения зданий*/
        NameText.text = _buildings[current_building].Name;//ставим имя
        MoneyText.text = "+ "+_buildings[current_building].Income + "G/"+_buildings[current_building].Time_+" сек.";//показываем доход со здания
        
        MonetsText.text = (new BigFloat(money)).Round().ToString() + "G";//Текущий счет
        HeadText.text = (_buildings[current_building].IsAvaliable ? "Улучшить " : "Купить ");//кнопка для покупки здания
        
        lvlText.text = _buildings[current_building].Lvl + " lvl.";//показатель уровня
        InfoText.text = "Цена: " + _buildings[current_building].NextCost + "G\n+" +
                        _buildings[current_building].nextIncome() + "G / " + _buildings[current_building].Time_ + " s";

        if (money < _buildings[current_building].NextCost)
        {
            //если недостаточно денег на покупку/апгрейд здания, то выключаем кнопку
            BuyButton.GetComponent<Button>().interactable = false;
        }
        else
        {
            //а тут наоборот включаем 
            BuyButton.GetComponent<Button>().interactable = true;
        }
        
       
        GetComponent<UpgradeBuildings>().update_info();
        
        //информация по хэнд кликеру
        if( GetComponent<UpgradeHandClick>().HandClicker!= null)
        {
            handClickInfoText.text = "+ " + GetComponent<UpgradeHandClick>().HandClicker.Income + "G";
            GetComponent<UpgradeHandClick>().update_info();
        }
        

    }

    public void Move(int count)
    {
        
        /*
         *    Скрол зданий с помощью плагина DoTween
         * 
         */
        if (count == 0 || current_building + count < 0 || current_building+count >= number_of_buildings) return;
        if (buildingsImage[current_building].transform.position != current.transform.position) return;

        if (count > 0)
        {
            buildingsImage[current_building].transform.DOMove(last.transform.position, animation_duration);
        }
        else 
        {
            buildingsImage[current_building].transform.DOMove(next.transform.position, animation_duration);
        }

        current_building += count;
        buildingsImage[current_building].transform.DOMove(current.transform.position, animation_duration);
        if (!_buildings[current_building].IsAvaliable)
            BuyButton.GetComponentInChildren<Text>().text = "Купить";
        update_info();
    }


    public void handClick()
    {
        Statistics.totalG += GetComponent<UpgradeHandClick>().HandClicker.Income;
        Statistics.totalGAfterReset += GetComponent<UpgradeHandClick>().HandClicker.Income;
        money += GetComponent<UpgradeHandClick>().HandClicker.Income;
        update_info();
        
    }

    private void FixedUpdate()
    {
        // предложение получить престиж
        Debug.Log("tens power " + tensPower);
        Debug.Log("asdf " + BigInteger.Pow(10, tensPower));
        if(money >= BigInteger.Pow(10, tensPower))
        {
            msgShower.GetComponent<MassageBox>().showPrestige(money);
            tensPower++;
        }
        //Здесь считаем прогресс дохода здания
        if(_buildings[current_building].IsAvaliable)//если здание купелно, то
        {
            try
            {
                if (_buildings[current_building].Time_ < 0.6) //для того, чтобы анимация не была сильно быстрой, просто вырубаем ее
                    buildingsImage[current_building].transform.GetChild(0).GetComponent<Image>().fillAmount = 1;
                else
                {
                    //пытаемся вычислисть насколько оно завершило работу и отображаем это на прогресс бар
                    buildingsImage[current_building].transform.GetChild(0).GetComponent<Image>().fillAmount =
                        ((DateTime.Now.Ticks) - _buildings[current_building].startWorkAt.Ticks) /
                        (_buildings[current_building].Time_ * 10000000);
                }
                

            }
            catch (Exception e)
            {
                //отлавливаем ошибки деления на ноль :) Всё норм, так и должно быть
                print("неудачно");
            }
            
        }

        var tick = new TimeSpan(DateTime.Now.Ticks);//смотрим на текущий тик игры;
        for (int i = 0; i < number_of_buildings; ++i)
        {
            if (tick.Ticks >= _buildings[i].startWorkAt.Ticks + ((int) (_buildings[i].Time_ * 10000000f)))
            { // если тик больше чем тик при старте работы здания + нужное время на роботу здания
                money += _buildings[i].Income;
                _buildings[i].startWorkAt = new TimeSpan(tick.Ticks);
         
                
            }
        }
        
        update_info();
    }
    
    
    // функций вызываймая при закрытии приложения (в билде не работает. только в эдиторе)
    private void OnApplicationQuit()
    {
        Statistics.TimeUpdate();
        SaveSystem.save(this);
    }
    // функция вызываемая когда приложение встает на паузу (в андройде равносильно тому что его скрыли)
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus)
        {
            pauseTime = DateTime.Now;
            Statistics.TimeUpdate();
            SaveSystem.save(this);
        }
        else
        {
            timeSkip(pauseTime);
        }
    }
    
    //функция для скипа времени в игре
    private void timeSkip(DateTime savedTime)
    {
        
        
        // высчитываем кол-во секунд с момента выключения игры
        double secondsSinceSave = DateTime.Now.Subtract(savedTime).TotalSeconds;
        if(secondsSinceSave < 1) return;
        BigInteger resInc = 0;
        //проходим через все здания для выщита прибыли от здания за прошедшее время
        for (int i = 0; i < number_of_buildings; i++)
        {
            int countOfTiks =  (int)(secondsSinceSave / Buildings[i].Time_);

            
            if (countOfTiks > 0 && Buildings[i].IsAvaliable)
            {
                money += _buildings[i].Income * (int)(countOfTiks / 2);
                Statistics.totalG += _buildings[i].Income * (int)(countOfTiks / 2);
                Statistics.totalGAfterReset += _buildings[i].Income * (int)(countOfTiks / 2);
                resInc += _buildings[i].Income * (int)(countOfTiks / 2);
            }
        }
        msgShower.GetComponent<MassageBox>().showIncome(resInc, new TimeSpan(0, 0, 0, (int)secondsSinceSave, 0)); //TODO: поменяй тут так, чтобы показывало всё правильно
    }

    public void buildingsInit(int prestigeBonus = 4)
    {
        /*Тут инициализируем объекты и создаем здания*/
        
        money = 0;//инициализация денег
        _buildings = new Building[number_of_buildings];//инициализация здания
        
        current_building = 0;//ставим текущие здание как 0
        for (int i = 0; i < number_of_buildings; ++i)
        {
            BigInteger sell;
            if (prestigeBonus < 24) sell = base_cost[i] * (prestigeBonus - 4) / 20;
            else sell = base_cost[i] * 100 / 99;
            //Инициализируем наши объекты зданий
            _buildings[i] = new Building(names[i], 
                base_cost[i] - sell,
                base_income[i] * (int)((Math.Pow(prestigeBonus - 3, 1.075) - (prestigeBonus - 4)) * 10) / 10,
                base_time[i], risks[i]);
            _buildings[i].Dependent = (depedents[i] == -1 ? null : _buildings[depedents[i]]);
        }

    }

}

