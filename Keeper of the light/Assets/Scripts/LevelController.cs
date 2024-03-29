using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    public AudioSource _soundButton;

    [SerializeField] private AudioClip _sound = null;
    public RuneUIInit _runeUI;
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
        PlayerPrefs.DeleteKey("Paneglif");
        Invoke("CaveTimelineLocation",1f);
        PlayerPrefs.SetInt("End", 0);
        SceneManager.LoadScene(1);
        if (PlayerPrefs.HasKey("_lastScene"))
        {
            PlayerPrefs.DeleteKey("_lastScene");
        }
        _runeUI.Describe();
    }
    public void CaveLocation()
    {
        SceneManager.LoadScene(2);
        PlayerPrefs.SetInt("_lastScene", 2);
        _runeUI.Describe();

    }
    public void ForestLocation()
    {
        SceneManager.LoadScene(3);
        PlayerPrefs.SetInt("_lastScene", 3);
    }
    public void Instruction()
    {
         SceneManager.LoadScene(4);
        _runeUI.Describe();
    }
    public void LobbyLocation()
    {
        SceneManager.LoadScene(6);
        _runeUI.Describe();
    }
    public void FirstLocation()
    {
        SceneManager.LoadScene(5);
        _runeUI.Describe();
    }
    public void SecondLocation()
    {
        SceneManager.LoadScene(7);
        _runeUI.Describe();
    }
    public void ThirdLocation()
    {
        SceneManager.LoadScene(8);
        _runeUI.Describe();
    }
    public void Continue()
    {
        PlayerPrefs.SetInt("End", 0);
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
        _runeUI.Describe();
    }
    public void BackToMenu()
    {
        
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PlayerPrefs.SetInt("End", 1);
        _runeUI.Describe();
    }
    public void Quit()
    {
        Invoke("Quit", 1f);
        Application.Quit();
        _runeUI.Describe();
    }

    
}
