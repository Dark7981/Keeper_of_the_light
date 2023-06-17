using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public static LevelController Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void Play()
    {
        SceneManager.LoadScene(1);
    }
    public void Respawn()
    {
        SceneManager.LoadScene(1);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
    public void Quit()
    {
        Application.Quit();
    }
    
}
