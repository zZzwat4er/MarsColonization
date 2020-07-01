using System;
using Game_Classes;
using UnityEngine;

public class Metamechanics : MonoBehaviour
{

    [SerializeField] private GameLogic _;

    /*
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
     * todo: переделать функцию эвентов
    #1#
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
    }*/

    public void gameReset(int resetBoost)
    {
        _.Money = 0;
        Statistics.totalGAfterReset = 0;
        Statistics.totalSpendGAfterReset = 0;
        Statistics.inGameTimeAfetrReset = new TimeSpan(0, 0, 0);
        _.buildingsInit(resetBoost);
        //todo: improve handclick "base_income"
        _.GetComponent<UpgradeHandClick>().HandClicker = new HandClicker(_.Buildings);
    }

}