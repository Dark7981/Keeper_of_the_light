using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    //public static LevelController Instance { get; private set; }

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(Instance);
    //        return;
    //    }
    //    else
    //    {
    //        Destroy(gameObject);
    //    }
    //}
    public void CaveTimelineLocation()
    {
        SceneManager.LoadScene(1);
    }
    public void CaveLocation()
    {
        SceneManager.LoadScene(2);
    }
    public void ForestLocation()
    {
        SceneManager.LoadScene(3);
    }

    public void Respawn()
    {
        var sceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneIndex);
        Time.timeScale = 1;
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
