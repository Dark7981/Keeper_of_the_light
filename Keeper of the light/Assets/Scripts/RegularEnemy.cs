using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RegularEnemy : MonoBehaviour
{
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private bool isSleaping;

    [SerializeField] private float speed;
    [Range(1, 3)] private int status;

    [SerializeField] private GameObject deadEnemy;

    private void Start()
    {
        //transform.eulerAngles = new Vector3(90f, 0, 0);    // Так треба
        //agent.updateRotation = false;
        agent.updateUpAxis= false;
        spriteRenderer.color = Color.white;                 // Поки спить ворог білий
        if (!isSleaping)
            StartCoroutine(Wandering(3f));                  // Якщо не спить то починає бродити
    }
    private IEnumerator Wandering(float radius)                             // Ворог бродить по карті
    {
        status = 1;
        spriteRenderer.color = Color.gray;                                  // Ворог сірий поки бродить
        agent.speed = speed / 3f;
        while (true)
        {
            agent.destination = transform.position + RandomPos(radius);
            yield return new WaitUntil(() => !agent.hasPath);
            yield return new WaitForSeconds(0.3f);
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
        agent.destination = (targetPos - transform.position) * 3;   // Ворог біжить по прямій в сторону джерела звуку
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
        Instantiate(deadEnemy, transform.position, Quaternion.identity);
    }
}



