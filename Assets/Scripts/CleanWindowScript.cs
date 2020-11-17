using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CleanWindowScript : MonoBehaviour
{
    IEnumerator AnimC;
    IEnumerator getText;
    public GameObject stayOnGreen;
    public GameObject detectEnter;
    public GameObject arrow;
    public GameObject Background;
    public Animator GCloth;
    public AudioSource audioClip_GClothA;
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
            GCloth.SetBool("TimeToTurn", true);
            Background.SetActive(true);
            ReadText(true);
            FindHelpingWords(true);
            getText = ShowText();
            StartCoroutine(getText);

            AnimC = ChangeAni();
            StartCoroutine(AnimC);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            detectEnter.SetActive(true);
            arrow.SetActive(true);
            GCloth.SetBool("TimeToTurn", false);
            Background.SetActive(false);
            audioClip_GClothA.Stop();
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

                if (chars[0] == "GCloth")
                {
                    foreach (string word in chars)
                    {
                        if (word != "GCloth")
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

                    if (Hchars[0] == "GCloth")
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

            if (chars[0] == "GCloth")
            {
                foreach (string word in chars)
                {
                    //print(filename + "This is filename");
                    if (word != "GCloth")
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

        audioClip_GClothA = this.gameObject.GetComponent<AudioSource>();
        audioClip_GClothA.clip = Resources.Load<AudioClip>(filename);
    }

    IEnumerator ShowText()
    {
        getAudio();
        audioClip_GClothA.Play();
        foreach (string i in updatedSentences)
        {
            newText.text = "<color=green>花花:</color>" + i;
            yield return new WaitForSeconds(5.6f);
        }
        updatedSentences.Clear();
        stayOnGreen.SetActive(false);
    }

    IEnumerator ChangeAni()
    {
        getAudio();
        float timing = audioClip_GClothA.clip.length;
        yield return new WaitForSeconds(timing);
        GCloth.SetBool("ChangeAni", true);

        //print(timing + "<color=Blue><b>This is audioclip length</b></color>");
    }
}
