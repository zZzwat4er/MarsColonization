using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SceneLoading : MonoBehaviour
{
    [Header("Остальный объекты")]
    public Image ProgressImg;
    public Text ProgressTxt;
    
    [Header("Загружаемая сцена")]
    public int sceneId;
    void Start()
    {
        StartCoroutine(AsyncLoad());
    }

    IEnumerator AsyncLoad()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        while(!operation.isDone)
        {
            float progress = operation.progress / 0.9f;
            ProgressImg.fillAmount = progress;
            ProgressTxt.text = Convert.ToInt32(progress * 100) + "%";
            yield return null;
        }
        
    }
}
