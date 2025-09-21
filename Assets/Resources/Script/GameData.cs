using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameData : MonoBehaviour
{
    public static GameData instance;
    public float playerRating;

    void Start()
    {
        instance = this;
        playerRating = 5;
    }

    public void AddRating(float rate)
    {
        playerRating += rate;
        playerRating /= 2;
    }

    private void Update() {
        UpdateText();
    }

    void UpdateText()
    {
        GameObject charaRatingTextObject = GameObject.FindGameObjectWithTag("CharaRating");
        if (charaRatingTextObject != null)
        {
            charaRatingTextObject.GetComponent<TMP_Text>().text = playerRating.ToString("0.0");
        }
    }
}
