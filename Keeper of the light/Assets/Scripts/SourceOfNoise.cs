using System.Collections.Generic;
using UnityEngine;

public class SorceOfNoise : MonoBehaviour
{
    public void MakeNoise(Vector3 sourcePos,float distance)         // Публічний метод який буде викликатися кожен раз коли щось іздає звук
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(sourcePos, distance);  // Створюється коло, все що попало в коло чує звук
        List<GameObject> listeners = new List<GameObject>();
        for (int i = 0; i < colliders.Length; i++)  // Перевірка по тегу чи може об'єкт почути звук
        {
            if (colliders[i].CompareTag("Enemy"))
            {
                listeners.Add(colliders[i].gameObject);
            }
        }

        for (int i = 0; i < listeners.Count; i++)   // Виконується метод в скрипті ворога який трігерить активацію скрипта поведінки
        {
            listeners[i].GetComponent<RegularEnemy>()._HeardSth(sourcePos);
        }
    }
}
