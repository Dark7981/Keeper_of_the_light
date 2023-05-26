using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SorceOfNoise : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            MakeNoise(10f, "Enemy");
    }
    public void MakeNoise(float distance, string targetTag)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, distance);
        List<GameObject> listeners = new List<GameObject>();
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag(targetTag))
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, colliders[i].transform.position - transform.position, distance);
                if (hit.collider != null)
                    if (hit.collider == colliders[i])
                        listeners.Add(colliders[i].gameObject);
            }
        }

        for (int i = 0; i < listeners.Count; i++)
        {
            listeners[i].GetComponent<RegularEnemy>()._HeardSth(transform.position);
        }
    }
}
