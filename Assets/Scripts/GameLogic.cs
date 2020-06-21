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

public class GameLogic : MonoBehaviour
{
    //Нужные объекты
    private GameObject[] buildingsImage;//массив зданий, количество регулерутеся переменой number_of_buildings
    private Building[] _buildings; //сами объекты зданий
    
    
    /*Информция о зданиях
     ******************************************************************/
    private BigInteger[] base_cost =
    {
        50, 100, 1500, 5000, 10000, 15000, 50000, 100000, 250000, 1000000, 15000000, 50000000, 666666666,
        BigInteger.Parse("20000000000000000")
    }; //базовая цена за здание
    private BigInteger[] base_income =
    {
        1, 10, 200, 2000, 7500, 15000, 50000, 200000, 500000, 1500000, 5000000, 30000000, 666666666,
        BigInteger.Parse("10000000000")
    };//базовый доход здания
    private float[] base_time = { 1, 5, 10, 20, 30, 45, 90, 150, 200, 300, 500, 600, 900, 1200};//базове время для дохода
    private string[] names = { "Convenience Store", "Gas Station", "Chop Shop", "Pharmacy", "Gun Store", 
        "Lawyer Office", "Nightclub", "Laboratory", "Weapon Manufacture", "Police Station", "Red Mine", 
        "Governmental Building", "Gates of Hell", "Planet Core"};
    private int[] depedents = {1, 2, -1, 7, 8, 6, -1, 11, 9, -1, 12, -1, -1, 13}; //-1 нет апгрейда, любой другое - номер здания от которого зависит апгрейд
    private float[] risks = { 0, 0, 5, 5, 10, -10, 5, 10, 15, -20, 0, -30, 30, 0};
    /**********************************************************************/
    
    //Геттеры и сеттеры
    public Building[] Buildings => _buildings; 
    public int NumberOfBuildings => number_of_buildings; 
    public int CurrentBuilding => current_building;
    public BigFloat Money
    {
        get => money;
        set => money = value;
    }
    public BigFloat GPerSecond
    {
        get => gPerSecond;
        set => gPerSecond = value;
    }
    
    

    [Header("Инфа здания")]
    [SerializeField] private Text NameText,  MoneyText, TimeText, MonetsText;
    [SerializeField] private Button BuyButton;
    [SerializeField] private Text HeadText, InfoText, lvlText; 
    [Header("Настройки")]
    [SerializeField] private float animation_duration = 2f;
    [SerializeField] private GameObject last, current, next;//фантомный вспомогательные здания
    [SerializeField] private GameObject buildings_panel;
    [SerializeField] private int number_of_buildings;//количество зданий
    
    //переменные для работы и совершения вычислений
    private int current_building = 0;
    private BigFloat money= 0;
    private BigFloat gPerSecond = 0;
    
    void Awake()
    {
        money = 9666666666;
        _buildings = new Building[number_of_buildings];
        
        current_building = 0;
        buildingsImage = new GameObject[number_of_buildings];//инициализируем
            //Создаем самое первое здание, которое будет стоять по середине экрана
        buildingsImage[0] = Instantiate(current, current.transform.position, Quaternion.identity);
        buildingsImage[0].transform.SetParent(buildings_panel.transform);
        buildingsImage[0].transform.localScale= new Vector3(1, 1, 1);
        _buildings[0] = new Building(names[0], base_cost[0], base_income[0], base_time[0], risks[0]);
        for (int i = 1; i < number_of_buildings; ++i)
        {
            //остальные здания будут справа, как следующие
            buildingsImage[i] = Instantiate(next, next.transform.position, Quaternion.identity) as GameObject;
            buildingsImage[i].transform.SetParent(buildings_panel.transform);
            buildingsImage[i].transform.localScale= new Vector3(1, 1, 1);
            
            //Инициализируем наши объекты зданий
            _buildings[i] = new Building(names[i], base_cost[i], base_income[i], base_time[i], risks[i]);
        }
        

        for (int i = 0; i < number_of_buildings; ++i)
            _buildings[i].Dependent = (depedents[i] == -1 ? null : _buildings[depedents[i]]);

        update_info();
    }

   

    public void update_info()
    {
        NameText.text = _buildings[current_building].Name;
        MoneyText.text = _buildings[current_building].Income + "G";
        TimeText.text = "в течении " + _buildings[current_building].baseTime + " секунд";
        MonetsText.text = (new BigFloat(money)).Round().ToString() + "G";
        HeadText.text = (_buildings[current_building].IsAvaliable ? "Улучшить " : "Купить ");
        
        lvlText.text = _buildings[current_building].Lvl + " lvl.";
        InfoText.text = "Цена: " + _buildings[current_building].NextCost + "G\n" +
                        _buildings[current_building].nextIncome() + "G / " + _buildings[current_building].Time_ + " s";
        
    }

    public void Move(int count)
    {
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

    public void buyBuilding()
    {
       
    }

    public void handClick()
    {
        money += 1;
        update_info();
        
    }
    
    // Update is called once per frame
    void Update()
    {
        money += (gPerSecond * ((int) (Time.deltaTime * 1000000))) / 1000000;
        update_info();
        
    }
}

