
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SoundsDark : MonoBehaviour
{

    [SerializeField] private bool isPlay;
    [SerializeField] private List<AudioClip> clipList;
    [SerializeField] private AudioClip mainSound;
    [SerializeField] private AudioClip mainAnotherSound;


  
    private float sound3d = -0.1f;
    private int numberOfSound;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioSource _mainSource;
    [SerializeField] private AudioSource _mainSource2;
    private AudioHighPassFilter _highPass;
    private AudioLowPassFilter _lowPass;

    void Start()
    {
        _audioSource= GetComponent<AudioSource>();
        _highPass= GetComponent<AudioHighPassFilter>();
        _lowPass= GetComponent<AudioLowPassFilter>();
        StartCoroutine(SoundCheck());
        StartCoroutine(SoundEnvironment());
        StartCoroutine(StartMainSound());
        StartCoroutine(StartAnotherMainSound());

    }

    private IEnumerator SoundCheck() 
    {
        while (true)
        { 
            Sounds();         
            yield return new WaitForSeconds(4f);
        }
    }
   
    private void Sounds() 
    {

        if (!_audioSource.isPlaying && isPlay)
        {
            _lowPass.cutoffFrequency = Random.Range(3000, 5000);
            _highPass.cutoffFrequency = Random.Range(3000, 5000);
            numberOfSound = Random.Range(0, clipList.Count);
            _audioSource.PlayOneShot(clipList[numberOfSound]);
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

    private IEnumerator StartMainSound()
    {
        while (true)
        {
            _mainSource.PlayOneShot(mainSound);

            yield return new WaitForSeconds(mainSound.length);
        }
    }

    private IEnumerator StartAnotherMainSound()
    {
        while (true)
        {
            _mainSource2.PlayOneShot(mainAnotherSound);

            yield return new WaitForSeconds(mainSound.length);
        }
    }
}
