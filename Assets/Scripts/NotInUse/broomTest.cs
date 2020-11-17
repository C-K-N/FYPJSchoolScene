using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class broomTest : MonoBehaviour
{
    public Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool("RunOnEntry", true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
