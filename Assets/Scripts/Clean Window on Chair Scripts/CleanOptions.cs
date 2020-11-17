using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CleanOptions : MonoBehaviour
{
    IEnumerator getText;
    public GameObject stayOnGreen;
    public GameObject Background;
    public TextMeshProUGUI newText;
    public GameObject questionBoard;
    public AudioSource audioClip_BWindow;
    public ArrayList listWords = new ArrayList();
    public ArrayList updatedSentences = new ArrayList();
    public TextAsset SentencePath;
    public TextAsset HelpingPath;
    public TextAsset AudioPath;
    private string filename;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // SELECTED YES
    public void ChooseYes()
    {
        questionBoard.SetActive(false);
        Background.SetActive(true);
        ReadTextY(true);
        FindHelpingWordsY(true);

        getText = ShowTextY();
        StartCoroutine(getText);
    }

    // SELECTED NO
    public void ChooseNo()
    {
        questionBoard.SetActive(false);
        Background.SetActive(true);
        ReadTextN(true);
        FindHelpingWordsN(true);

        getText = ShowTextN();
        StartCoroutine(getText);
    }

    // CHOSE YES FUNCTIONS
    void ReadTextY(bool condition)
    {
        if (condition == true)
        {
            var path = SentencePath.text;
            var myText = path.Split('\n');

            foreach (string i in myText)
            {
                var chars = i.Split(";".ToCharArray());

                if (chars[0] == "BWindowY")
                {
                    foreach (string word in chars)
                    {
                        if (word != "BWindowY")
                        {
                            string cleanSentences = word.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                            listWords.Add(cleanSentences);

                        }
                    }
                }
            }
        }
    }

    void FindHelpingWordsY(bool condition)
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

                    if (Hchars[0] == "BWindowY")
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

    void ReadAudioFilesY()
    {
        var audio_path = AudioPath.text;
        var myAText = audio_path.Split('\n');

        foreach (string i in myAText)
        {
            var chars = i.Split(";".ToCharArray());

            if (chars[0] == "BWindowY")
            {
                foreach (string word in chars)
                {
                    //print(filename + "This is filename");
                    if (word != "BWindowY")
                    {
                        filename = word.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                    }
                }
            }
        }
    }

    void getAudioY()
    {
        ReadAudioFilesY();

        audioClip_BWindow = this.gameObject.GetComponent<AudioSource>();
        audioClip_BWindow.clip = Resources.Load<AudioClip>(filename);
    }

    IEnumerator ShowTextY()
    {
        print(updatedSentences);
        getAudioY();
        audioClip_BWindow.Play();

        foreach (string i in updatedSentences)
        {
            newText.text = "<color=green>作者:</color>" + i;
            yield return new WaitForSeconds(3.5f);
        }
        updatedSentences.Clear();
        stayOnGreen.SetActive(false);
    }

    // CHOSE NO FUNCTIONS

    void ReadTextN(bool condition)
    {
        if (condition == true)
        {
            var path = SentencePath.text;
            var myText = path.Split('\n');

            foreach (string i in myText)
            {
                var chars = i.Split(";".ToCharArray());

                if (chars[0] == "BWindowN")
                {
                    foreach (string word in chars)
                    {
                        if (word != "BWindowN")
                        {
                            string cleanSentences = word.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                            listWords.Add(cleanSentences);

                        }
                    }
                }
            }
        }
    }

    void FindHelpingWordsN(bool condition)
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

                    if (Hchars[0] == "BWindowN")
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

    void ReadAudioFilesN()
    {
        var audio_path = AudioPath.text;
        var myAText = audio_path.Split('\n');

        foreach (string i in myAText)
        {
            var chars = i.Split(";".ToCharArray());

            if (chars[0] == "BWindowN")
            {
                foreach (string word in chars)
                {
                    //print(filename + "This is filename");
                    if (word != "BWindowN")
                    {
                        filename = word.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                    }
                }
            }
        }
    }

    void getAudioN()
    {
        ReadAudioFilesN();

        audioClip_BWindow = this.gameObject.GetComponent<AudioSource>();
        audioClip_BWindow.clip = Resources.Load<AudioClip>(filename);
    }

    IEnumerator ShowTextN()
    {
        getAudioN();
        audioClip_BWindow.Play();

        foreach (string i in updatedSentences)
        {
            newText.text = "<color=green>作者:</color>" + i;
            yield return new WaitForSeconds(3.5f);
        }
        updatedSentences.Clear();
        stayOnGreen.SetActive(false);
    }
}
