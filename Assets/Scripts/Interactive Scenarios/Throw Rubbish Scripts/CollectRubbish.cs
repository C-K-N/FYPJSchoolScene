using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectRubbish : MonoBehaviour
{
    IEnumerator getText;
    public GameObject stayOnGreen;
    public GameObject detectEnter;
    public GameObject rubbish1;
    public GameObject rubbish2;
    public GameObject rubbish3;
    public GameObject arrow;
    public GameObject counterText;
    public GameObject counterBg;
    public GameObject Background;
    public AudioSource audioClip_Me;
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
            Background.SetActive(true);

            rubbish1.GetComponent<Outline>().enabled = true;
            rubbish2.GetComponent<Outline>().enabled = true;
            rubbish3.GetComponent<Outline>().enabled = true;

            ReadText(true);
            FindHelpingWords(true);

            getText = ShowText();
            StartCoroutine(getText);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            detectEnter.SetActive(true);
            arrow.SetActive(true);
            Background.SetActive(false);
            audioClip_Me.Stop();
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

                if (chars[0] == "Me")
                {
                    foreach (string word in chars)
                    {
                        if (word != "Me")
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


                    if (Hchars[0] == "Me")
                    {
                        foreach (string HelpW in Hchars)
                        {
                            print(HelpW + "<color=green>:this is HelpW</color>");

                            if (i.Contains(HelpW))
                            {
                                helpingWordsInThatSentence.Add(HelpW);
                                print("<COLOR=RED>THIS ADDS HelpW: </COLOR>" + HelpW);

                            }
                        }
                    }

                }
                if (helpingWordsInThatSentence.Count == 0)
                {
                    updatedSentences.Add(i);
                }
                else
                {
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

            if (chars[0] == "Me")
            {
                foreach (string word in chars)
                {
                    print(filename + "This is filename");
                    if (word != "Me")
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

        audioClip_Me = this.gameObject.GetComponent<AudioSource>();
        audioClip_Me.clip = Resources.Load<AudioClip>(filename);
    }

    IEnumerator ShowText()
    {
        getAudio();
        audioClip_Me.Play();

        foreach (string i in updatedSentences)
        {
            newText.text = "<color=green>我:</color>" + i;
            yield return new WaitForSeconds(3);
        }
        updatedSentences.Clear();

        counterText.SetActive(true);
        counterBg.SetActive(true);
        stayOnGreen.SetActive(false);
    }
}
