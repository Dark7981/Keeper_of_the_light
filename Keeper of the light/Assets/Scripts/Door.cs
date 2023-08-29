using System;
using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int _doorLocks;
    public bool _locked;
    public bool open;

    private Animator _animator;
    private BoxCollider2D _collider;
    private AudioSource _audioSource;

    public bool canPlayAnim;

    [SerializeField] private AudioClip openSound;
    [SerializeField] private AudioClip closeSound;

    public static Action<int,int> doorText;

    private void Start()
    {
        _animator = gameObject.GetComponent<Animator>();
        _collider = gameObject.GetComponent<BoxCollider2D>();
        _audioSource = gameObject.GetComponent<AudioSource>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerBehaviour>())
            Open(collision.GetComponent<PlayerBehaviour>());
    }
    public void Open(PlayerBehaviour _player)
    {
        if (_player.doorKey >= _doorLocks && _locked)
        {
            _player.doorKey -= _doorLocks;
            PlayerPrefs.SetInt("Key", _player.doorKey);
            StartCoroutine("OpenCrt");
            _locked = false;
            open = true;
        } else if (!_locked)
        {
            StartCoroutine("OpenCrt");
        }else
        {
            doorText.Invoke(_player.doorKey,_doorLocks);
        }
    }
    public void OpenDoor()
    {
        if (canPlayAnim)
        {
            _audioSource.Stop();
            _animator.PlayInFixedTime("OpenDoor");
            _audioSource.PlayOneShot(openSound);
            open = true;
        }
    }
    public void CloseDoor()
    {
        if (canPlayAnim)
        {
            _audioSource.Stop();
            _animator.PlayInFixedTime("CloseDoor");
            _audioSource.PlayOneShot(closeSound);
            open = false;
        }
    }
    public void CanPlayAnimationFalse()
    {
        canPlayAnim = false;
    }
    public void CanPlayAnimationTrue()
    {
        canPlayAnim = true;
    }
    private IEnumerator OpenCrt()
    {
        _collider.enabled = false;
        if (open)
        {
            CloseDoor();
            yield return new WaitForSeconds(2f);
            OpenDoor();
        }
        else
        {
            OpenDoor();
            yield return new WaitForSeconds(2f);

            CloseDoor();
        }
        StopCoroutine("OpenCrt");
        _collider.enabled = true;
    }
}
