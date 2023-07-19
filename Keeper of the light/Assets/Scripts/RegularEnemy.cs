
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
    [SerializeField] private List<GameObject> patrolPoints;
    [SerializeField] private GameObject deadEnemySprite;
    [SerializeField] private Animator _animator;

    [SerializeField] private List<AudioClip> passiveSounds;
    [SerializeField] private List<AudioClip> agressiveSounds;

    private NavMeshAgent agent;
    private AudioSource _audioSource;
    private SpriteRenderer spriteRenderer;
    private List<Vector3> wayPoints = new();

    private int maxIndex = 0;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        _audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _animator = GetComponentInChildren<Animator>();
        agent.updateUpAxis = false;
        spriteRenderer.color = Color.white;                 // ���� ����� ����� ����

        GetPatrolPoints();

        if (!isSleaping)// ���� �� ����� �� ������ �������
        {
            StartCoroutine(Wandering(3f));
            _animator.SetBool("Run", true);
        }
                           
    }

    private void GetPatrolPoints()
    {
        foreach (GameObject patrolPoint in patrolPoints)
        {
            wayPoints.Add(patrolPoint.transform.position);
        }
    }

    private IEnumerator Wandering(float radius)                             // ����� ������� �� ����
    {
        status = 1;
        StartCoroutine(EnemySounds());
        spriteRenderer.color = Color.gray;                                  // ����� ���� ���� �������
        agent.speed = speed / 3f;
        while (true)
        {
            Debug.Log(maxIndex);
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

    private IEnumerator Wandering(float radius, Vector3 aroundTheSpot)      // ����� ������� ������� �����
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
        spriteRenderer.color = Color.yellow;        // ����� ������ ���� ���� �����
        StopCoroutine("Wandering");
        agent.speed = speed / 3f * 2f;
        agent.destination = targetPos;
        yield return new WaitForSeconds(2f);

        StartCoroutine(Wandering(2f, targetPos));    // ����� ������� ������� ������� �����
        yield return new WaitForSeconds(8f);

        StopCoroutine("Wandering");
        StartCoroutine(Wandering(3f));
        yield break;
    }

    private IEnumerator Aggresive(Vector3 targetPos)                // ����� ���������
    {
        status = 3;
        StartCoroutine(EnemyAgressiveSounds());
        spriteRenderer.color = Color.red;                           // ����� �������� ���� ���������
        agent.speed = speed * 1.5f;
        agent.destination = targetPos;
        Debug.Log("Agresive");

        yield return new WaitForSeconds(5f);
        StartCoroutine(HeardSth(targetPos));
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

    private IEnumerator EnemySounds()
    {
        _audioSource.Stop();
        StopCoroutine(EnemyAgressiveSounds());
        AudioClip currentSuond = null;

        while (true)
        {
            Debug.Log("2222");
            currentSuond = passiveSounds[UnityEngine.Random.Range(0, passiveSounds.Count)];
            _audioSource.PlayOneShot(currentSuond);

            yield return new WaitForSeconds(currentSuond.length);
        }
    }

    private IEnumerator EnemyAgressiveSounds()
    {
        StopCoroutine(EnemySounds());
        AudioClip currentSuond = null;

        while (true)
        {
            Debug.Log("1111");
            currentSuond = agressiveSounds[UnityEngine.Random.Range(0, agressiveSounds.Count)];
            _audioSource.PlayOneShot(currentSuond);

            yield return new WaitForSeconds(currentSuond.length);
        }
    }

    public void Dead()
    {
        Destroy(gameObject);
        Instantiate(deadEnemySprite, transform.position, Quaternion.identity);
    }
}



