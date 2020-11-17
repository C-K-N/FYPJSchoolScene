using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectUserEnter : MonoBehaviour
{
    public GameObject stayOnGreen;
    public GameObject trigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            stayOnGreen.SetActive(true);
            trigger.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
