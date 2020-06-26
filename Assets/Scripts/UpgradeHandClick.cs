using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Game_Classes;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeHandClick : MonoBehaviour
{
    [SerializeField] private GameObject gameInfo;//Объект [Game] в котором находятся все компоненты
    private GameLogic _;//в нем нужная информация о зданиях
    private HandClicker _handClicker;//наш хэндкликер
    

  

    public HandClicker HandClicker
    {
        get => _handClicker;
        set => _handClicker = value;
    }

    [SerializeField] private Button[] upgrades;

    private void Start()
    {
        // для _ берем информацию с объекта Game
        _ = gameInfo.GetComponent<GameLogic>();
        if(_handClicker == null)
            _handClicker = new HandClicker(_.Buildings);
        
        for (int i = 0; i < upgrades.Length; ++i)
        {
            //иницилизируем цены на апгрейды хэндкликера
            upgrades[i].GetComponentsInChildren<Text>()[1].text = "Цена: " + _handClicker.Costs[i] + "G\nДоход: " +
                                                                  _handClicker.getNextIncome(i) + "G за клик";
        }
        _.update_info();//обновляем информацию
    }

    public void upgrade(int index)
    {
        //Вызываем фунцию Up. Если возвращает 0, то денег не хватает, иначе делается апгрейд и отнимается баланс
        _.Money -= _handClicker.Up(index, _.Money); 
        update_info();//обновление кнопок хэндкликера
    }

    public void update_info()
    {
        //обновления менюшки кликера
        for (int i = 0; i < upgrades.Length; ++i)
        {
            if (_handClicker.Costs[i] > _.Money) upgrades[i].interactable = false;//если денег не хватает, то выключаем кнопку
            else upgrades[i].interactable = true;//а если хватает, то включаем
            upgrades[i].GetComponentsInChildren<Text>()[0].text = _handClicker.Names[i];//присваиваем имя а затем и инфу о кнопке
            upgrades[i].GetComponentsInChildren<Text>()[1].text = "Цена: " + _handClicker.Costs[i] + "G\nДоход: " +
                                                                  _handClicker.getNextIncome(i) + "G за клик";
        }
    }
}
