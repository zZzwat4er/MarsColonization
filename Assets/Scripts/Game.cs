using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.UIElements;
using Button = UnityEngine.UI.Button;
using Image = UnityEngine.UI.Image;
using Random = System.Random;

public class Game : MonoBehaviour {

    private int money = 0, point = 1;

    private int[] Base = { 50, 100, 1500, 5000, 10000, 15000, 50000, 100000};//базовая цена за здание
    private int[] costs = { 50, 100, 1500, 5000, 10000, 15000, 50000, 100000 }; //цены за здания
    private int[] levels = { 0, 0, 0 , 0, 0, 0, 0, 0}; //уровни зданий
    private int[] points = { 1, 10, 200, 2000, 7500, 15000, 50000, 200000 }; //количество монет за нажатие на здание
    private bool[] isPurchased = { false, false, false, false ,false ,false, false, false}; //true если здание под индексом i приобретено
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
        Debug.Log(number);
        Debug.Log("########################################"+number);
        Debug.Log(number);
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
            
            Businesses[number].GetComponentInChildren<Text>().text = points[number] * levels[number] + "$"; //обновляем кнопку, которая показывает прибыль за тык
            
            GameObject.Find("BuildingsPanel").GetComponentInChildren<ShopBuildings>().update_info();
        }
    }

    public int[] get_info(int num)
    {
        //функция чтобы из вне получить информацию об определенном здании
        return new int[] { (isPurchased[num]? 1:0), costs[num], levels[num], points[num],(int) baseClickPeriod[num]} ;
    }

    public void show_hide_menu()
    {
        //будем показывать или прятать меню покупок, при клике на кнопку SHOP
        if (!ShopMenu.activeSelf)
        {
            //если мы открываем меню, то нам надо обновить значения кнопок
                
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
    /*private IEnumerator AutoClickSpeedUp(int mod)
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
    }*/
    
    
    /*
     * eventType = true/false = хорошое/плохое событие соответственно.
     * eventMod:
     *     0) полная остановка или удвоение эфективности на 10 мин (Вероятно изменение именно приносимых денег)
     *     1) Снижение/Увеличение производительности двух построек на 30% на 20 минут
     *     2) Уменьшение/Увеличение позитивных модификаторов зданий в 2 раза (необходима система модификаций)
     *     3) +/- 5% от текущей сумы денег
     *     4) удвоение/полное исчизновение "наркотиков" (Требуется имплементация "наркотиков")
     *     5) октулючить все постройки на 100 тапов(пока хз как сделать каунтер) или усиление тапа в 5 раз на 30 сек
     *     6) Уменьшение шанса позитивного события на 10, 15, 20% (только если eventType == false)
     * todo: тригер для системы эвентов (всплывающее окно с сообщением о том что произощел эвент)
    */
    private IEnumerator playEvent(bool eventType, int eventMod)
    {
        float modification = 0f;
        int time = 0;
        Random rnd = new Random(DateTime.Now.Millisecond);
        switch (eventMod)
        {
            case 0:
                int buildingId = rnd.Next(0, Businesses.Length);
                while(!isPurchased[buildingId]) buildingId = rnd.Next(0, Businesses.Length);
                modification = points[buildingId];
                time = 600;
                points[buildingId] += (eventType)? (int)modification : -(int)modification;
                yield return new WaitForSeconds(time);
                points[buildingId] = (int)modification;
                break;
            case 1:
                int building1Id = rnd.Next(0, Businesses.Length);
                int building2Id = rnd.Next(0, Businesses.Length);
                while(!isPurchased[building1Id]) building1Id = rnd.Next(0, Businesses.Length);
                while(!isPurchased[building2Id] && building1Id == building2Id) building2Id = rnd.Next(0, Businesses.Length);
                modification = 0.3f;
                time = 1200;
                points[building1Id] += points[building1Id] * ((eventType)? (int)modification : -(int)modification);
                points[building2Id] += points[building2Id] * ((eventType)? (int)modification : -(int)modification);
                yield return new WaitForSeconds(time);
                points[building1Id] -= points[building1Id] * ((eventType)? (int)modification : -(int)modification);
                points[building2Id] -= points[building2Id] * ((eventType)? (int)modification : -(int)modification);
                break;
            case 2:
                //todo: система модификаций зданий
                break;
            case 3:
                modification = ((eventType)? (float)(money * 0.05) : (float)(-money * 0.05));
                AddMoney((int)modification);
                break;
            case 4:
                //todo: "наркотики"
                break;
            case 5:
                if (eventType)
                {
                    modification = 5;
                    time = 30; 
                    handClickPowerUp *= (int)modification;
                    yield return new WaitForSeconds(time);
                    handClickPowerUp /= (int)modification;
                }
                else
                {
                    //todo: click counter
                }
                break;
            case 6:
                if (!eventType)
                {
                    rnd.Next(2, 5);
                    //todo: risk system
                }
                break;
            default:
                break;
        }
    }
    
}
