using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour {

    private int score = 0;
    public Text scoreText;

	public void OnClick()
    {
        ++score;
        scoreText.text = score + "$"; 
    }
}
