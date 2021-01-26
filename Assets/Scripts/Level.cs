using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] float loadDelay = 3f;

    public void LoadStartMenu()
    {
        SceneManager.LoadScene("Start Menu");
    }

    public void LoadGame()
    {
        StartCoroutine(WaitAndLoad("Game"));

        if ( FindObjectsOfType<GameStatus>().Length > 0 )
        {
            FindObjectOfType<GameStatus>().ResetGame();
        }
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad("Game Over"));
    }

    IEnumerator WaitAndLoad( string sceneName)
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(sceneName);
    }
}
