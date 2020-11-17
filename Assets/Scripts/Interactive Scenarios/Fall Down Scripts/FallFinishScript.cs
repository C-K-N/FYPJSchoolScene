using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using TMPro;

public class FallFinishScript : MonoBehaviour
{
    IEnumerator AnimC;
    IEnumerator getText;

    public GameObject allStayOnGreen;
    public GameObject stayOnGreen;
    public GameObject detectEnter;
    public GameObject arrow;
    public GameObject Background;
    public Animator GFallFinish;
    public AudioSource audioClip_GFallFinish;
    public TextMeshProUGUI newText;
    public ArrayList listWords = new ArrayList();
    public ArrayList updatedSentences = new ArrayList();
    public TextAsset SentencePath;
    public TextAsset HelpingPath;
    public TextAsset AudioPath;
    public string filename;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            detectEnter.SetActive(false);
            arrow.SetActive(false);

            StartCoroutine(startTime());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            detectEnter.SetActive(true);
            arrow.SetActive(true);
            GFallFinish.SetBool("StartTalking", false);
            Background.SetActive(false);
            audioClip_GFallFinish.Stop();
            StopCoroutine(getText);
            newText.text = string.Empty;
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

    public void ReadText(bool condition)
    {
        if (condition == true)
        {
            var path = SentencePath.text;
            var myText = path.Split('\n');

            foreach (string i in myText)
            {
                var chars = i.Split(";".ToCharArray());

                if (chars[0] == "GFallFinish")
                {
                    foreach (string word in chars)
                    {
                        if (word != "GFallFinish")
                        {
                            string cleanSentences = word.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                            listWords.Add(cleanSentences);

                        }
                    }
                }
            }
        }
    }

    public void FindHelpingWords(bool condition)
    {
        ArrayList helpingWordsInThatSentence = new ArrayList();

        if (condition == true)
        {
            var Hpath = HelpingPath.text;
            var myHText = Hpath.Split('\n');

            foreach (string i in listWords)
            {
                print("checkSentence 1st foreach " + "<color=Blue>" + i + "</color>");
                foreach (string word in myHText)
                {
                    print(word + " this prints the helping words (word)");
                    var Hchars = word.Split(";".ToCharArray());

                    if (Hchars[0] == "GFallFinish")
                    {
                        foreach (string HelpW in Hchars)
                        {
                            print(HelpW + "<color=green>:this is HelpW</color>");

                            if (i.Contains(HelpW))
                            {
                                helpingWordsInThatSentence.Add(HelpW);
                                //print("<COLOR=RED>THIS ADDS HelpW: </COLOR>" + HelpW);

                            }
                        }
                    }

                }
                if (helpingWordsInThatSentence.Count == 0)
                {
                    updatedSentences.Add(i);
                    //print(i + "helping word sentence count is 0");
                }
                else
                {
                    print("helping word sentence count is not 0 but is- " + helpingWordsInThatSentence.Count);
                    foreach (string helping in helpingWordsInThatSentence)
                    {
                        int start = i.IndexOf(helping);
                        int wordcount = helping.Length;

                        StringBuilder sb = new StringBuilder(i, 50);
                        sb.Insert(start, "<color=red>");
                        sb.Insert(start + wordcount + 11, "</color>");

                        updatedSentences.Add(sb.ToString());
                        /*
                        foreach (char PD in helping)
                        {
                            print(PD + " -here line 153 this is VARIABLE HELPING");
                        }
                        foreach (string p in updatedSentences)
                        {
                            print(p + " -here line 152 this is UPDATED SENTENCES");
                        }*/
                    }

                    helpingWordsInThatSentence.Clear();
                }
            }
            listWords.Clear();
        }

    }

    public void ReadAudioFiles()
    {
        var audio_path = AudioPath.text;
        var myAText = audio_path.Split('\n');

        foreach (string i in myAText)
        {
            var chars = i.Split(";".ToCharArray());

            if (chars[0] == "GFallFinish")
            {
                foreach (string word in chars)
                {
                    //print(filename + "This is filename");
                    if (word != "GFallFinish")
                    {
                        filename = word.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                    }
                }
            }
        }
    }

    public void getAudio()
    {
        ReadAudioFiles();

        audioClip_GFallFinish = this.gameObject.GetComponent<AudioSource>();
        audioClip_GFallFinish.clip = Resources.Load<AudioClip>(filename);
    }

    public IEnumerator startTime()
    {
        yield return new WaitForSeconds(0);

        GFallFinish.SetBool("BeginWalk",false);
        GFallFinish.SetBool("Finished", false);

        ReadText(true);

        Background.SetActive(true);

        FindHelpingWords(true);

        getText = Getting();
        StartCoroutine(getText);

        StartCoroutine(STOPM());
    }

    IEnumerator STOPM()
    {
        getAudio();
        float timing = audioClip_GFallFinish.clip.length;
        yield return new WaitForSeconds(timing + 3);
        Background.SetActive(false);
        newText.text = "";

        GFallFinish.SetBool("Finished", true);

        allStayOnGreen.SetActive(false);
        stayOnGreen.SetActive(false);

        StopCoroutine(getText);
    }

    IEnumerator Getting()
    {
        getAudio();
        audioClip_GFallFinish.Play();
        foreach (string i in updatedSentences)
        {
            newText.text = "<color=green>珊珊:</color>" + i;
            yield return new WaitForSeconds(4);
        }
        updatedSentences.Clear();
    }
}
