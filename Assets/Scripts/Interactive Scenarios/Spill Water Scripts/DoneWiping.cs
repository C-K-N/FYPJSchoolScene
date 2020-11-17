using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoneWiping : MonoBehaviour
{
    public GameObject startActivate;
    public GameObject finishActivate;
    public GameObject finishWipeScript;
    public GameObject allStayOnGreen;

    private bool runOnce = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        var done = CheckDoneWipe();
        if (done == true && runOnce == false)
        {
            startActivate.SetActive(false);
            finishActivate.SetActive(true);
            StartCoroutine(finishWipeScript.GetComponent<SpillFinishScript>().startTime());
            runOnce = true;
            allStayOnGreen.SetActive(true);
        }
    }

    // Check if done wiping
    bool CheckDoneWipe()
    {
        var check = true;
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            // If done
            if (gameObject.transform.GetChild(i).gameObject.activeInHierarchy)
            {
                check = false;
            }
        }
        return check;
    }
}
