using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreDisplay : MonoBehaviour
{
    [SerializeField] int maxScoreLength;

    TextMeshProUGUI scoreDisplay;
    GameStatus gameStatus;

    // Start is called before the first frame update
    void Start()
    {
        scoreDisplay = GetComponent<TextMeshProUGUI>();
        gameStatus = FindObjectOfType<GameStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreDisplay.text = GetScoreString();
    }

    private string GetScoreString()
    {
        int numZeros = maxScoreLength - gameStatus.GetScore().ToString().Length;

        string sText = "";

        for (int i = 0; i < numZeros; i++)
        {
            sText += "0";
        }
        return sText + gameStatus.GetScore().ToString();
    }
}
