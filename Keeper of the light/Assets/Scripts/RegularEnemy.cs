
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RegularEnemy : MonoBehaviour
{
    [SerializeField] private bool isSleaping;
    [SerializeField] private bool patroling;

    [SerializeField] private float speed;
    [Range(1, 3)] private int status;

    [SerializeField] private List<GameObject> patrolPoints;

    [SerializeField] private GameObject deadEnemySprite;

    private NavMeshAgent agent;
    private SpriteRenderer spriteRenderer;
    private List<Vector3> wayPoints = new();

    private int index = 0;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();

        agent.updateUpAxis = false;
        spriteRenderer.color = Color.white;                 // Поки спить ворог білий

        GetPatrolPoints();

        if (!isSleaping)
            StartCoroutine(Wandering(3f));                  // Якщо не спить то починає бродити
    }

    private void GetPatrolPoints()
    {
        foreach (GameObject patrolPoint in patrolPoints)
        {
            wayPoints.Add(patrolPoint.transform.position);
        }
    }

    private IEnumerator Wandering(float radius)                             // Ворог бродить по карті
    {
        status = 1;
        spriteRenderer.color = Color.gray;                                  // Ворог сірий поки бродить
        agent.speed = speed / 3f;
        while (true)
        {
            Debug.Log(index);
            if (patroling)
            {
                agent.destination = wayPoints[index];

                if ((index + 1) == wayPoints.Count)
                {
                    index = 0;
                }
                else
                {
                    index++;
                }
                
            }
            else
            {
                agent.destination = transform.position + RandomPos(radius);
            }
            yield return new WaitUntil(() => transform.position == agent.destination);
            yield return new WaitForSeconds(0.3f);
            Debug.Log("1111");
        }
    }

    private IEnumerator Wandering(float radius, Vector3 aroundTheSpot)      // Ворог бродить навколо точки
    {
        while (true)
        {
            agent.destination = aroundTheSpot + RandomPos(radius);
            yield return new WaitUntil(() => !agent.hasPath);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private IEnumerator HeardSth(Vector3 targetPos) // Ворог щось почув і йде туди
    {
        status = 2;
        spriteRenderer.color = Color.yellow;        // Ворог жовтий коли щось почув
        StopCoroutine("Wandering");
        agent.speed = speed / 3f * 2f;
        agent.destination = targetPos;
        yield return new WaitForSeconds(2f);

        StartCoroutine(Wandering(2f, targetPos));    // Ворог бродить навколо джерела звуку
        yield return new WaitForSeconds(8f);

        StopCoroutine("Wandering");
        StartCoroutine(Wandering(3f));
        yield break;
    }

    private IEnumerator Aggresive(Vector3 targetPos)                // Ворог заагрений
    {
        status = 3;
        spriteRenderer.color = Color.red;                           // Ворог червоний поки заагрений
        agent.speed = speed * 1.5f;
        agent.destination = targetPos;
        Debug.Log("Agresive");

        yield return new WaitForSeconds(5f);
        StartCoroutine(HeardSth(targetPos));
        yield break;
    }
    private Vector3 RandomPos(float a)      // Метод який повертає випадкову позицію в радіусі
    {
        return new Vector3(Random.Range(-a, a), Random.Range(-a, a), 0);
    }
    public void _HeardSth(Vector3 targetPos)        // Ворог щось почув 
    {
        StopAllCoroutines();
        if (status >= 2)
            StartCoroutine(Aggresive(targetPos));   // Якщо це другий раз коли ворог щось чує то він стає агресивним
        else
            StartCoroutine(HeardSth(targetPos));    // Якщо це перший раз то ворог насторожується
    }

    public void Dead()
    {
        Destroy(gameObject);
        Instantiate(deadEnemySprite, transform.position, Quaternion.identity);
    }
}



