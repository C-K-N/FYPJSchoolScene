using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleanTableAni : MonoBehaviour
{
    public Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            _animator.SetBool("IsInFront", true);
        }

    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _animator.SetBool("IsInFront", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
