using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DetectRubbish : MonoBehaviour
{
    public TextMeshProUGUI countText;
    public GameObject finishScript;
    public GameObject startActivate;
    public GameObject finishActivate;
    public AudioSource audioSource;
    public AudioClip success;

    public GameObject allStayOnGreen;

    private int rubbishThrown;

    // Start is called before the first frame update
    void Start()
    {
        rubbishThrown = 0;
        countText.text = "垃圾收集量: " + rubbishThrown.ToString() + "/3";
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        // If enter dustbin
        if (other.gameObject.CompareTag("Rubbish")) // Case sensitive!
        {
            rubbishThrown += 1;
            countText.text = "垃圾收集量: " + rubbishThrown.ToString() + "/3";
            other.GetComponent<Outline>().enabled = false;  // Disable highlight script when thrown
            audioSource.PlayOneShot(success);

            if (rubbishThrown == 3)
            {
                startActivate.SetActive(false);
                finishActivate.SetActive(true);
                StartCoroutine(finishScript.GetComponent<FinishRubbish>().startTime());  // Play the script after finish throwing all
                allStayOnGreen.SetActive(true);
            }
            else
            {
                startActivate.SetActive(true);
                finishActivate.SetActive(false);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        // If exit dustbin
        if (other.gameObject.CompareTag("Rubbish")) // Case sensitive!
        {
            rubbishThrown -= 1;
            countText.text = "垃圾收集量: " + rubbishThrown.ToString() + "/3";
            other.GetComponent<Outline>().enabled = true;

            if (rubbishThrown == 3)
            {
                startActivate.SetActive(false);
                finishActivate.SetActive(true);
                StartCoroutine(finishScript.GetComponent<FinishRubbish>().startTime());  // Play the script after finish throwing all
                allStayOnGreen.SetActive(true);
            }
            else
            {
                startActivate.SetActive(true);
                finishActivate.SetActive(false);
            }
        }
    }
}