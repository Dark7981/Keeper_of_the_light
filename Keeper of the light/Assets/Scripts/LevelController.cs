using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public AudioSource _soundButton;

    [SerializeField] private AudioClip _sound; 
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
    

    public void SoundButton()
    {
       
        
       
        _soundButton.PlayOneShot(_sound);
        
        
        
        
    }
   
    public void CaveTimelineLocation()
    {
      Invoke("CaveTimelineLocation",1f);
      
        SceneManager.LoadScene(1);
        if (PlayerPrefs.HasKey("_lastScene"))
        {
            PlayerPrefs.DeleteKey("_lastScene");
            
        }
       
    }
    public void CaveLocation()
    {
        SceneManager.LoadScene(2);
        PlayerPrefs.SetInt("_lastScene", 2);
    }
    public void ForestLocation()
    {
        SceneManager.LoadScene(3);
        PlayerPrefs.SetInt("_lastScene", 3);
    }

    public void Continue()
    {
       
        if (PlayerPrefs.HasKey("_lastScene"))
        {
            SceneManager.LoadScene(PlayerPrefs.GetInt("_lastScene"));
            
        }
        
    }

    private IEnumerator SoundPlay()
    {
        yield return new WaitForSeconds(_sound.length);
    }
    public void Respawn()
    {
        var sceneIndex = SceneManager.GetActiveScene().buildIndex;
       StartCoroutine( SoundPlay());
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
        Invoke("Quit", 1f);
        Application.Quit();
    }

    
}
