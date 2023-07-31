using System.Collections;
using UnityEngine;


public class AnimationEnemyController : MonoBehaviour
{
    [SerializeField] private Transform _regularEnemy;
    private Animator _animator;
    private Vector2 OldPosition;
    private float xRotation;
    private Vector2 positionMagnetic;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        StartCoroutine("CheckPosition");
    }
    private void OnEnable()
    {
        EnemyKillZone.PlayerDead += KillPlayer;
    }
    private void OnDisable()
    {
        EnemyKillZone.PlayerDead -= KillPlayer;
    }
    private void KillPlayer()
    {
        _animator.PlayInFixedTime("RegularEnemyKill");
    }
    private IEnumerator CheckPosition()
    {
        while (true)
        {
            OldPosition[0] = _regularEnemy.position.x;
            OldPosition[1] = _regularEnemy.position.y;
            xRotation = _regularEnemy.rotation.x;

            yield return new WaitForSeconds(0.3f);

            positionMagnetic[0] = (OldPosition[0] - _regularEnemy.position.x);
            positionMagnetic[1] = (OldPosition[1] - _regularEnemy.position.y);

            if (Mathf.Abs(OldPosition[0] - _regularEnemy.position.x) <= 0.1f
            && Mathf.Abs(OldPosition[1] - _regularEnemy.position.y) <= 0.1f ||
            Mathf.Abs(xRotation - _regularEnemy.rotation.x) >= 0.1f || Mathf.Abs(xRotation - _regularEnemy.rotation.x) <= -0.1f)
            {
                _animator.SetBool("Run", false);
                _animator.SetBool("Agressive", false);
            }
            else
                _animator.SetBool("Run", true);
        }
    }
}
