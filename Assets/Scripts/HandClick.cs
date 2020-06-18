using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HandClick : MonoBehaviour
{
    public Button HandClickButton;

    private int money = 1;//деньги за тык

    private float time_to_wait = 0.2f;//время на обновление кнопки 

    private bool is_active = false;
    // Start is called before the first frame update
    void Start()
    {
        HandClickButton.GetComponent<Image>().color = Color.yellow;
        HandClickButton.GetComponentInChildren<Text>().text = "Взять" + money + "$";
    }

    public void handClick()
    {
        if (!is_active)
        {
            GetComponent<Game>().AddMoney(money);
            
            StartCoroutine(Waiting());
        }
    }

    private IEnumerator Waiting()
    {
        is_active = true;
        HandClickButton.GetComponent<Image>().color = Color.gray;
        yield return new WaitForSeconds(time_to_wait);
        is_active = false;
        HandClickButton.GetComponent<Image>().color = Color.yellow;
    }
}
