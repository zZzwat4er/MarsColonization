using Game_Classes;
using UnityEngine;
using UnityEngine.UI;

public class Boosters : MonoBehaviour
{
    [SerializeField] private GameLogic _;
    [SerializeField] private Button[] btns;
    
    

    private int[] prices = { 1, 1, 1, 1, 1};
    private string[] boostText = { "skip 300 seconds", "get x5 tapping power for 10 minutes", 
        "get x3 tapping power for 20 minutes", "All production rate increased 1000% for 5 active minutes", 
        "All production rate increased 10000% for 30 active seconds"};

    public void onShow()
    {
        update_info();
    }
    
   /*
    *     0) Скип - получить прибыль за 300 секунд в один момент
    *     1/2) Увеличение ручного клика в 5 раз на 10 минут и в 3 раза на 20 минут.
    *     3/4) Ускорение автокликов в 10 раз на 5 минут и в 100 раз на 30 секунд.
    */ 
    public void buttons(int buttonIndex)
    {
        if(prices[buttonIndex] < _.GPrime)
        {
            _.GPrime -= prices[buttonIndex];

            switch (buttonIndex)
            {
                case 0:
                    _.events.Add(new Events(12, _.Buildings));
                    break;
                case 1:
                    _.events.Add(new Events(8, _.Buildings));
                    break;
                case 2:
                    _.events.Add(new Events(9, _.Buildings));
                    break;
                case 3:
                    _.events.Add(new Events(10, _.Buildings));
                    break;
                case 4:
                    _.events.Add(new Events(11, _.Buildings));
                    break;
                default:
                    break;
            }
        }
        update_info();
    }

    private void update_info()
    {
        for (int i = 0; i < btns.Length; ++i)
        {
            if (prices[i] > _.GPrime) btns[i].interactable = false;//если денег не хватает, то выключаем кнопку
            else btns[i].interactable = true;//а если хватает, то включаем
            
            btns[i].GetComponentInChildren<Text>().text =
                "Price: " + BigToShort.Convert(prices[i])
                          + "G+\nEffect: " + boostText[i];
        }
    }

}