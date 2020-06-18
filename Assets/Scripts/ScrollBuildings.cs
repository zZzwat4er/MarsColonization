using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBuildings : MonoBehaviour
{

    public GameObject scrollbar;

    private float scroll_pos = 0; //текущая позиция скролла

    private float[] pos;//позиции всех зданий
    
    private int current_building = 0;
    // Start is called before the first frame update
 

    // Update is called once per frame
    private void Update()
    {
  
        pos = new float[transform.childCount];//берем позиции зданий
        float distance = 1f / (pos.Length - 1f);
        for (int i = 0; i < pos.Length; ++i)
        {
            pos[i] = distance * i; //высчитываем дистанции
        }

        float cpos = scrollbar.GetComponent<Scrollbar>().value;
        if (cpos != pos[current_building])
            scrollbar.GetComponent<Scrollbar>().value =
                Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[current_building], 0.1f);//плавно скролит до здания
        

    }

    public void next_slide(bool is_left) //функция определяет следуещие здание, по нажатию на кнопку
    {
        //проверяем на крайность здание
        if (is_left && current_building > 0) current_building--;
        else if (!is_left && current_building < 7) ++current_building;
 


    }
}
