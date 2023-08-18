using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySounds : MonoBehaviour
{
    [Header("Enemy state sounds")]
    [SerializeField] private List<AudioClip> passiveStatusSounds;
    //[SerializeField] private List<AudioClip> alarmingStatusSounds;
    [SerializeField] private List<AudioClip> agressiveStatusSounds;

    private RegularEnemy _regularEnemy;
    private BossRegularEnemy _bossRegularEnemy;
    private AudioSource _audioSource;

    private void Start()
    {
        if (GetComponent<RegularEnemy>())
        {
            _regularEnemy = GetComponent<RegularEnemy>();
        }
        else
        {
            _bossRegularEnemy = GetComponent<BossRegularEnemy>();
        }
        _audioSource = GetComponent<AudioSource>();

        if ((passiveStatusSounds.Count != 0) && (agressiveStatusSounds.Count != 0))
        {
            if (GetComponent<RegularEnemy>())
            {
                StartCoroutine(SoundsRegular());
            }else
            {
                StartCoroutine(SoundBoss());
            }
        }
    }

    private IEnumerator SoundsRegular()
    {
        yield return new WaitUntil(() => !_regularEnemy.IsSleeping);

        while (true)
        {
            AudioClip currentSound = null;
            if (_regularEnemy.Status == 1)
            {
                PlaySound(passiveStatusSounds, out currentSound);
            }
            else
            {
                PlaySound(agressiveStatusSounds, out currentSound);
                }

            yield return new WaitForSeconds(currentSound.length);
        }
    }
    private IEnumerator SoundBoss()
    {
        yield return new WaitUntil(() => !_bossRegularEnemy.IsSleeping);

        while (true)
        {
            AudioClip currentSound = null;
            if (_bossRegularEnemy.Status == 1)
            {
                PlaySound(passiveStatusSounds, out currentSound);
            }
            else
            {
                PlaySound(agressiveStatusSounds, out currentSound);
            }
            yield return new WaitForSeconds(currentSound.length);
        }
    }


    private void PlaySound(List<AudioClip> sounds, out AudioClip clip)
    {
        clip = sounds[Random.Range(0, sounds.Count)];
        _audioSource.PlayOneShot(clip);
    }



}
