using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class BallThrow : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject prefab;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        var _direction = Camera.main.ScreenToWorldPoint(Input.mousePosition) - prefab.transform.position;
        rb.velocity = Vector2.zero;
        rb.constraints = RigidbodyConstraints2D.None;
        rb.AddForce(_direction * speed);
    }
}
