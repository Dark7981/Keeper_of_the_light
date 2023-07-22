using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class AnimationEnemyController : MonoBehaviour
{
    [SerializeField] private Transform _regularEnemy;
    private Animator _animator;
    private Vector2 OldPosition;
    public float xRotation;
    public Vector2 positionMagnetic;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }
    public void CheckFirstPosition() // Для анімаці
    {
        OldPosition[0] = _regularEnemy.position.x;
        OldPosition[1] = _regularEnemy.position.y;
        xRotation = _regularEnemy.rotation.x;
    }
    public void CheckLastPosition()
    {
        Debug.Log(xRotation - _regularEnemy.rotation.x);
        positionMagnetic[0] = (OldPosition[0] - _regularEnemy.position.x);
        positionMagnetic[1] = (OldPosition[1] - _regularEnemy.position.y);

        if (Mathf.Abs(OldPosition[0] - _regularEnemy.position.x) <= 0.1f
            && Mathf.Abs(OldPosition[1] - _regularEnemy.position.y) <= 0.1f ||
            Mathf.Abs(xRotation - _regularEnemy.rotation.x) >= 0.1f || Mathf.Abs(xRotation - _regularEnemy.rotation.x) <= -0.1f)
        {
            _animator.SetBool("Run", false);
        }
        else
            _animator.SetBool("Run", true);
        
    }
}
