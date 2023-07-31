
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RegularEnemy : MonoBehaviour
{
    [SerializeField] private bool isSleaping;
    [SerializeField] private bool patroling;

    [SerializeField] private float speed;
    [Range(1, 3)] private int status = 1;
    [SerializeField] private List<GameObject> _patrolPoints;
    [SerializeField] private GameObject deadEnemyPrefab;
    [SerializeField] private Animator _animator;

    private NavMeshAgent agent;
    private SpriteRenderer spriteRenderer;
    private List<Vector3> wayPoints = new();

    private int maxIndex = 0;
    private bool isDead = false;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        agent.updateUpAxis = false;
        spriteRenderer.color = Color.white;                 // ���� ����� ����� ����

        GetPatrolPoints();

        if (!isSleaping)// ���� �� ����� �� ������ �������
        {
            StartCoroutine(Wandering(3f));
        }
                           
    }

    private void GetPatrolPoints()
    {
        foreach (GameObject patrolPoint in _patrolPoints)
        {
            wayPoints.Add(patrolPoint.transform.position);
        }
    }

    private IEnumerator Wandering(float radius)                             // ����� ������� �� ����
    {
        status = 1;                                
        agent.speed = speed / 3f;
        while (true)
        {
            if (patroling)
            {
                agent.destination = wayPoints[maxIndex];

                if ((maxIndex + 1) == wayPoints.Count)
                {
                    maxIndex = 0;
                }
                else
                {
                    maxIndex++;
                }
                
            }
            else
            {
                agent.destination = transform.position + RandomPos(radius);
            }
            yield return new WaitUntil(() => transform.position == agent.destination);
            yield return new WaitForSeconds(0.3f);
        }
    }

    private IEnumerator WanderingAroundTheSpot(float radius, Vector3 aroundTheSpot)      // ����� ������� ������� �����
    {
        while (true)
        {
            agent.destination = aroundTheSpot + RandomPos(radius);
            yield return new WaitUntil(() => !agent.hasPath);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator HeardSth(Vector3 targetPos) // ����� ���� ����� � ��� ����
    {
        status = 2;
        StopCoroutine("Wandering");
        agent.speed = speed / 3f * 2f;
        agent.destination = targetPos;
        yield return new WaitForSeconds(2f);

        StartCoroutine(WanderingAroundTheSpot(2f, targetPos));    // ����� ������� ������� ������� �����
        yield return new WaitForSeconds(8f);

        StopCoroutine("Wandering");
        StartCoroutine(Wandering(3f));
        yield break;
    }

    private IEnumerator Aggresive(Vector3 targetPos)                // ����� ���������
    {
        status = 3;
        _animator.SetBool("Agressive",true);// ����� �������� ���� ���������
        agent.speed = speed * 1.5f;
        agent.destination = targetPos;
        yield return new WaitForSeconds(5f);
        StartCoroutine(HeardSth(targetPos));
        _animator.SetBool("Agressive", false);
        yield break;
    }
    private Vector3 RandomPos(float a)      // ����� ���� ������� ��������� ������� � �����
    {
        return new Vector3(UnityEngine.Random.Range(-a, a), UnityEngine.Random.Range(-a, a), 0);
    }
    public void _HeardSth(Vector3 targetPos)        // ����� ���� ����� 
    {
        StopAllCoroutines();

        if (status >= 2)
            StartCoroutine(Aggresive(targetPos));   // ���� �� ������ ��� ���� ����� ���� �� �� �� ��� ����������
        else
            StartCoroutine(HeardSth(targetPos));    // ���� �� ������ ��� �� ����� �������������
    }

    public void Dead(Transform positionTrap)
    {
        isDead = true;
        var DeadRegularEnemy = Instantiate(deadEnemyPrefab, positionTrap.position, Quaternion.identity);
        DeadRegularEnemy.transform.rotation = gameObject.transform.rotation;
        Destroy(gameObject);
    }

    public int Status
    {
        get { return status; }
    }
    public bool IsSleeping
    {
        get { return isSleaping; }
    }
    public bool IsPatroling
    {
        get { return patroling; }
    }
    public bool IsDead
    {
        get { return isDead; }
    }
}



