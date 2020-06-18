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
        if (Input.GetMouseButton(0))
        {
            /*
             * Если мы тыкаем по экрану, то нам надо будет отсчитывать текущее здание, на которое надо перевести
             * курсор
             * 
             */
            scroll_pos = scrollbar.GetComponent<Scrollbar>().value;//берем координта скрола
            float left_distance, right_distance;//хранят растояние до соседних зданий
            bool left = false, right = false;//true если ближе

            float c_position = Math.Abs(pos[current_building] - scroll_pos);//растояние от скрола до текущего здания
            if (current_building > 0)
            {
                left_distance = Math.Abs(scroll_pos - pos[current_building - 1]);
                if (left_distance < c_position)
                {
                    left = true;
                    current_building--;
                    
                    //если мы ближе к левому зданию, то переключаемся на него
                }

            }

            if (current_building < pos.Length - 1 && !left)
            {
                right_distance = Math.Abs(pos[current_building + 1] - scroll_pos);
                if (right_distance < c_position)
                {
                    right = true;
                    current_building++;
                    //если мы ближе к правому зданию, то переключаемся на него
                }
            }

           
            
        }
        else
        {
            float cpos = scrollbar.GetComponent<Scrollbar>().value;
            if (cpos != pos[current_building])
                scrollbar.GetComponent<Scrollbar>().value =
                    Mathf.Lerp(scrollbar.GetComponent<Scrollbar>().value, pos[current_building],
                        0.1f); //плавно скролит до здания
        }

    }

    public void next_slide(bool is_left) //функция определяет следуещие здание, по нажатию на кнопку
    {
        //проверяем на крайность здание
        if (is_left && current_building > 0) current_building--;
        else if (!is_left && current_building < 7) ++current_building;
 


    }
}
