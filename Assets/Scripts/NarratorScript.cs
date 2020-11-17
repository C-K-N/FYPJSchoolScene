using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NarratorScript : MonoBehaviour
{
    IEnumerator getText;
    IEnumerator stopNarration;
    public GameObject startMenu;
    public GameObject Background;
    public TextMeshProUGUI newText;
    public AudioSource audioClip_Narrator;
    public ArrayList listWords = new ArrayList();
    public ArrayList updatedSentences = new ArrayList();
    public TextAsset SentencePath;
    public TextAsset HelpingPath;
    public TextAsset AudioPath;
    public string filename;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(startTime());
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

                if (chars[0] == "Narrator")
                {
                    foreach (string word in chars)
                    {
                        if (word != "Narrator")
                        {
                            string cleanSentences = word.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                            listWords.Add(cleanSentences);

                        }
                    }
                }
            }
        }
    }

    public void FindingHelpWords(bool condition)
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

                    if (Hchars[0] == "Narrator")
                    {
                        foreach (string HelpW in Hchars)
                        {
                            //print(HelpW + "<color=green>:this is HelpW</color>");

                            if (i.Contains(HelpW))
                            {
                                helpingWordsInThatSentence.Add(HelpW);
                               // print("<COLOR=RED>THIS ADDS HelpW: </COLOR>" + HelpW);

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

                        /*foreach (char PD in helping)
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

            if (chars[0] == "Narrator")
            {
                foreach (string word in chars)
                {
                    //print(filename + "This is filename");
                    if (word != "Narrator")
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

        audioClip_Narrator = this.gameObject.GetComponent<AudioSource>();
        audioClip_Narrator.clip = Resources.Load<AudioClip>(filename);
    }

    IEnumerator startTime()
    {
        yield return new WaitForSeconds(3);

        ReadText(true);

        getAudio();
        audioClip_Narrator.Play();

        Background.SetActive(true);

        FindingHelpWords(true);

        getText = Getting();
        StartCoroutine(getText);

        StartCoroutine(STOPN());

    }

    IEnumerator STOPN()
    {
        getAudio();
        float timing = audioClip_Narrator.clip.length;
        yield return new WaitForSeconds(timing + 3);
        Background.SetActive(false);
        newText.text = "";

        startMenu.SetActive(true);

        StopCoroutine(getText);
        //print(timing + "<color=Blue><b>This is audioclip length</b></color>");
    }

    IEnumerator Getting()
    {
        foreach (string i in updatedSentences)
        {
            newText.text = "<color=green>作者:</color>" + i;
            yield return new WaitForSeconds(3);
        }
        updatedSentences.Clear();
        /*foreach (string pg in updatedSentences)
        {
            print(pg + " -here line 175 this is UPDATED SENTENCES AFTER CLEAR()");
        }*/
    }

}
