using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackScreen : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var animator = GetComponent<Animator>();
        animator.PlayInFixedTime("BlackScreen_out");
    }
}
