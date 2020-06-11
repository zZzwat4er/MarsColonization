using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Game : MonoBehaviour {

    private int money = 0, point = 1;

    private int[] Base = { 100, 1000, 10000 };//базовая цена за здание
    private int[] costs = { 100, 1000, 10000 }; //цены за здания
    private int[] levels = { 1, 0, 0 }; //уровни зданий
    private int[] points = { 5, 50, 500 }; //количество монет за нажатие на здание
    private bool[] isPurchased = { true, false, false }; //true если здание под индексом i приобретено
    //costs и isPurchased те включают в себя самое первое здание.

    public Text scoreText;//счетчик текущего счета
    public GameObject ShopMenu; //Панель меню
    public GameObject[] Businesses = new GameObject[3];//массив самих зданий


    public void OnClick(int number)
    {

        //при клике на кликер, будет увеличиваться счет монеток
    
        if (isPurchased[number])
        {
            //если здание куплено, то нам доступен бизнес и можем получить деньги.
            money += points[number];
            scoreText.text = money + "$";
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
            }
            money -= costs[number];//отнимаем с текущего счета деньги за здание
            scoreText.text = money + "$";
            levels[number]++;
            costs[number] =(int)(Base[number] * Math.Pow(1.10, levels[number]));
            ShopMenu.GetComponentsInChildren<Button>()[number].GetComponentInChildren<Text>().text = "Улучшить уровень за " + costs[number] + "$";
        }
    }


    public void show_hide_menu()
    {
        //будем показывать или прятать меню покупок, при клике на кнопку SHOP
        if (!ShopMenu.activeSelf)
        {
            Button[] buttons = ShopMenu.GetComponentsInChildren<Button>();
            
            for (int i = 0; i < buttons.Length; ++i)
            {
                Debug.Log(buttons[i].GetComponentInChildren<Text>().text);
                if (isPurchased[i])
                {
                    buttons[i].GetComponentInChildren<Text>().text = "Улучшить уровень за " + costs[i] + "$";
                    
                }
                else
                {
                    buttons[i].GetComponentInChildren<Text>().text = "Купить бизнес за " + costs[i] + "$";
                }
            }
        }

        ShopMenu.SetActive(!ShopMenu.activeSelf);
        
    }
}
