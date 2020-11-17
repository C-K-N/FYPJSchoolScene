using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CleanWindowChairScript : MonoBehaviour
{
    public Button buttonYes;
    public Button buttonNo;
    public GameObject detectEnter;
    public GameObject arrow;
    public GameObject Background;
    public TextMeshProUGUI newText;
    public GameObject chair;
    public GameObject questionBoard;
    public AudioSource audioClip_BWindow;

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
        if (other.tag == "Player")
        {
            detectEnter.SetActive(false);
            arrow.SetActive(false);
            buttonYes.interactable = false;
            buttonNo.interactable = false;
            questionBoard.SetActive(true);
            chair.GetComponent<Outline>().enabled = true;

            audioClip_BWindow.Play();
            StartCoroutine(WaitForAudio());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            detectEnter.SetActive(true);
            arrow.SetActive(true);
            Background.SetActive(false);
            newText.text = string.Empty;
            audioClip_BWindow.Stop();
            chair.GetComponent<Outline>().enabled = false;
            gameObject.SetActive(false);
        }
    }

    IEnumerator WaitForAudio()
    {
        float timing = audioClip_BWindow.clip.length;
        yield return new WaitForSeconds(timing);

        buttonYes.interactable = true;
        buttonNo.interactable = true;
    }
}
