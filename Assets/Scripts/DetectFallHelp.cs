using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectFallHelp : MonoBehaviour
{
    public GameObject finishScript;
    public GameObject startActivate;
    public GameObject finishActivate;

    public GameObject allStayOnGreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GFall"))
        {
            startActivate.SetActive(false);
            finishActivate.SetActive(true);
            StartCoroutine(finishScript.GetComponent<FallFinishScript>().startTime());
            allStayOnGreen.SetActive(true);
        }
    }
}
