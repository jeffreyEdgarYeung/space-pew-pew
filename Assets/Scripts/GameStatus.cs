using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    int score = 0;

    void Awake()
    {
        int numGameStatus = FindObjectsOfType<GameStatus>().Length;
        if( numGameStatus > 1)
        {
            gameObject.SetActive(false);
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    public int GetScore(){ return score; }

    public void ResetGame(){ Destroy(gameObject); }

    public void AddToScore( int pts ){ score += pts; }
}
