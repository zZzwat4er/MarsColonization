using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;

public class Game : MonoBehaviour {

    private int money = 0, point = 1;

    private int[] Base = { 50, 100, 1500, 5000, 10000, 15000, 50000, 100000};//базовая цена за здание
    private int[] costs = { 50, 100, 1500, 5000, 10000, 15000, 50000, 100000 }; //цены за здания
    private int[] levels = { 1, 0, 0 , 0, 0, 0, 0, 0}; //уровни зданий
    private int[] points = { 1, 10, 200, 2000, 7500, 15000, 50000, 200000 }; //количество монет за нажатие на здание
    private bool[] isPurchased = { true, false, false, false ,false ,false, false, false}; //true если здание под индексом i приобретено
    //costs и isPurchased те включают в себя самое первое здание.
    private float[] baseClickPeriod = { 1, 5, 10, 20, 30, 45, 90, 150};//время Автоклика в сикундах
    private bool[] autoClickAbility =  {true, true, true, true, true, true, true, true} ;  // true если здание может совершать автоклик вданный момент

    private int handClickPowerUp = 1;

    public Text scoreText;//счетчик текущего счета
    public GameObject ShopMenu; //Панель меню
    public GameObject[] Businesses;//массив самих зданий

    public void Start()
    {
        for (int i = 0; i < Businesses.Length; ++i)
        {
            Businesses[i].GetComponentInChildren<Text>().text = points[i] + "$";
            if (!isPurchased[i]) Businesses[i].GetComponent<Image>().color = Color.grey;
            else Businesses[i].GetComponent<Image>().color = Color.cyan;
        }
    }

    public void AddMoney(int count)
    {
        money += count;
        scoreText.text = money + "$";
    }
    public void OnClick(int number)
    {

        //при клике на кликер, будет увеличиваться счет монеток
    
        if (isPurchased[number])
        {
            //если здание куплено, то нам доступен бизнес и можем получить деньги.
            AddMoney( points[number]*levels[number] * handClickPowerUp);
            
        }
    }

    public void buyBusiness(int number)
    {
        
        if(money >= costs[number])
        {
            if (!isPurchased[number]) // если здание ранее не покупалось
            {
                //меняем цвет здания, которое покупаем и отмечаем, что купили его
                Businesses[number].GetComponent<Image>().color = Businesses[0].GetComponent<Image>().color;
                isPurchased[number] = true;
                Businesses[number].GetComponent<Image>().color = Color.cyan;
                // позволяем зданию совершать автоклик в момент покупки
               
            }
            AddMoney(-costs[number]);//отнимаем с текущего счета деньги за здание
            
            levels[number]++;//увеличиваем уровень
            costs[number] =(int)(Base[number] * Math.Pow(1.10, levels[number]));//по формуле считаем сумму для следующего апгрейда
            
            ShopMenu.GetComponentsInChildren<Button>()[number].GetComponentInChildren<Text>().text = "Улучшить уровень за " + costs[number] + "$"; //обновляем текст в магазине
            Businesses[number].GetComponentInChildren<Text>().text = points[number] * levels[number] + "$"; //обновляем кнопку, которая показывает прибыль за тык
        }
    }


    public void show_hide_menu()
    {
        //будем показывать или прятать меню покупок, при клике на кнопку SHOP
        if (!ShopMenu.activeSelf)
        {
            //если мы открываем меню, то нам надо обновить значения кнопок
            Button[] buttons = ShopMenu.GetComponentsInChildren<Button>();
            
            for (int i = 0; i < buttons.Length; ++i)
            {
                Debug.Log(buttons[i].GetComponentInChildren<Text>().text);
                if (isPurchased[i])
                {
                    //если здание куплено, то отображаем, что можем улучшить
                    buttons[i].GetComponentInChildren<Text>().text = "Улучшить уровень за " + costs[i] + "$";
                    
                }
                else
                {
                    //если не куплен, то отображаем, что можем купить
                    buttons[i].GetComponentInChildren<Text>().text = "Купить бизнес за " + costs[i] + "$";
                }
            }
        }

        ShopMenu.SetActive(!ShopMenu.activeSelf);//скрываем или показываем меню
        
    }
    //Coroutine отмеряющее время с начала автоклика до момента поступления денег
    private IEnumerator AutoClick(int number)
    {
        autoClickAbility[number] = false;
        OnClick(number);
        yield return new WaitForSeconds(baseClickPeriod[number]);
        autoClickAbility[number] = true;
    }

    private void FixedUpdate()
    {
        //пробегаемся по всем зданиям если оно может сделать автоклик вообще (autoClickAbility[0][i])
        //и может сделать автоклик в данный момент autoClickAbility[i] запускаем coroutine
        for (int i = 0; i < isPurchased.Length; i++)
            if (isPurchased[i] && autoClickAbility[i]) StartCoroutine(AutoClick(i));
    }
    
    //функция ускоряючая автоклик (mod == 0 в 100 раз быстрее на 30с mod == something в 10 раз быстрее на 5 мин)
    private IEnumerator AutoClickSpeedUp(int mod)
    {
        int multiplier = 0;
        int time = 0;
        if (mod == 0)
        {
            multiplier = 100;
            time = 30;
        }
        else
        {
            multiplier = 10;
            time = 300;
        }

        for (int i = 0; i < baseClickPeriod.Length; i++) baseClickPeriod[i] /= multiplier;
        yield return new WaitForSeconds(time);
        for (int i = 0; i < baseClickPeriod.Length; i++) baseClickPeriod[i] *= multiplier;
    }
    //функция усиляющая клик (mod == 0 в 5 раз  на 10мин mod == something в 3 раз быстрее на 20мин)
    private IEnumerator ClickPowerUp(int mod)
    {
        int multiplier = 0;
        int time = 0;
        if (mod == 0)
        {
            multiplier = 5;
            time = 600;
        }
        else
        {
            multiplier = 3;
            time = 1800;
        }

        handClickPowerUp *= multiplier;
        yield return new WaitForSeconds(time);
        handClickPowerUp /= multiplier;
    }
    
}
