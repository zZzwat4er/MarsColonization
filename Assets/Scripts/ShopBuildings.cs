using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopBuildings : MonoBehaviour
{
    [SerializeField]
    private Button buy_building;

    
    private Button[] buildings;
    
    private GameObject main_camera;
    private Game game;
    [SerializeField]
    private Scrollbar scroll;
    void Start()
    {

        main_camera = GameObject.Find("Main Camera");
        game = main_camera.GetComponent<Game>();
        int n = game.Businesses.Length;
        buildings = new Button[n];
        for (int i = 0; i < n; ++i)
        {
            int x = i;
            buildings[i] = Instantiate(buy_building) as Button;
            buildings[i].transform.SetParent(this.transform);
            buildings[i].transform.localScale = new Vector3(1, 1, 1);
            buildings[i].GetComponent<Button>().onClick.AddListener(() => game.buyBusiness(x));
        }
        update_info();
        scroll.value = 1f;
    }

    public void update_info()
    {
        int n = buildings.Length;
        for (int i = 0; i < n; ++i)
        {
            int[] info = game.get_info(i);
            buildings[i].GetComponentsInChildren<Text>()[0].text =
                (info[0] == 1 ? "Улучшить" : "Купить") + " за " + info[1] + (info[0] == 1 ? "G на " : "G для ") +
                (info[2] + 1) + (info[0] == 1 ? " уровень" : " уровня");
            buildings[i].GetComponentsInChildren<Text>()[1].text =
                "Будет приносить " + info[3]* (info[2] + 1) + "G в " + info[4] + " секунд";
        }
    }
    

}
