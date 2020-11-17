using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorAni : MonoBehaviour
{
    public Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            _animator.SetBool("OpenDoor", true);
            _animator.SetBool("CloseDoor", false);
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            _animator.SetBool("OpenDoor", false);
            _animator.SetBool("CloseDoor", true);
            _animator.SetBool("IdleDoor", true);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
