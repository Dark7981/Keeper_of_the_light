
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundsDark : MonoBehaviour
{
   
    [SerializeField] private bool isPlay;
    [SerializeField] private List<AudioClip> clipList;



  
    private float sound3d = -0.1f;
    private int numberOfSound;
    private AudioSource _audioSource;
    private AudioHighPassFilter _highPass;
    private AudioLowPassFilter _lowPass;

    void Start()
    {
        _audioSource= GetComponent<AudioSource>();
        _highPass= GetComponent<AudioHighPassFilter>();
        _lowPass= GetComponent<AudioLowPassFilter>();
       StartCoroutine( SoundCheck());
        StartCoroutine(SoundEnvironment());
    }

    private IEnumerator  SoundCheck() 
    {
        while (true)
        { 
            StartCoroutine(Sounds());
            
            Debug.Log("sound");
            
          yield return new WaitForSeconds(3f);
          

        }
    }
   
    private IEnumerator Sounds() 
    {

        if (!_audioSource.isPlaying && isPlay)
        {
            _lowPass.cutoffFrequency = Random.Range(3000, 5000);
            _highPass.cutoffFrequency = Random.Range(3000, 5000);
            numberOfSound = Random.Range(0, clipList.Count);
            _audioSource.PlayOneShot(clipList[numberOfSound]);
            yield return new WaitForSeconds(3f);


        }
    }
    private IEnumerator SoundEnvironment()
    {
        while (isPlay)
        {
            _audioSource.panStereo += sound3d;
            if (_audioSource.panStereo > 0.7)
                sound3d = -0.1f;
            else if (_audioSource.panStereo < -0.7)
                sound3d = 0.1f;
            yield return new WaitForSeconds(1);
        }yield return null;    
    }
}
