using System.Collections.Generic;
using UnityEngine;

public class SourceOfNoise : MonoBehaviour
{
    public void MakeNoise(Vector3 sourcePos,float distance)         // �������� ����� ���� ���� ����������� ����� ��� ���� ���� ���� ����
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(sourcePos, distance);  // ����������� ����, ��� �� ������ � ���� �� ����
        List<GameObject> listeners = new List<GameObject>();
        for (int i = 0; i < colliders.Length; i++)  // �������� �� ���� �� ���� ��'��� ������ ����
        {
            if (colliders[i].CompareTag("Enemy"))
            {
                listeners.Add(colliders[i].gameObject);
            }
        }

        for (int i = 0; i < listeners.Count; i++)   // ���������� ����� � ������ ������ ���� �������� ��������� ������� ��������
        {
            listeners[i].GetComponent<RegularEnemy>()._HeardSth(sourcePos);
        }
    }
    
}
