using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class FinishRubbish : MonoBehaviour
{
    IEnumerator getText;
    public GameObject allStayOnGreen;
    public GameObject stayOnGreen;
    public GameObject detectEnter;
    public GameObject arrow;
    public GameObject counterText;
    public GameObject counterBg;
    public GameObject Background;
    public TextMeshProUGUI newText;
    public AudioSource audioClip_MeFinish;
    public ArrayList listWords = new ArrayList();
    public ArrayList updatedSentences = new ArrayList();
    public TextAsset SentencePath;
    public TextAsset HelpingPath;
    public TextAsset AudioPath;
    public string filename;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") // Case sensitive!
        {
            detectEnter.SetActive(false);
            arrow.SetActive(false);
            StartCoroutine(startTime());
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            detectEnter.SetActive(true);
            arrow.SetActive(true);
            Background.SetActive(false);
            audioClip_MeFinish.Stop();
            StopCoroutine(getText);
            newText.text = string.Empty;
            gameObject.SetActive(false);
        }
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

                if (chars[0] == "MeFinish")
                {
                    foreach (string word in chars)
                    {
                        if (word != "MeFinish")
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

                    if (Hchars[0] == "MeFinish")
                    {
                        foreach (string HelpW in Hchars)
                        {
                            print("Helping words:" + HelpW);
                            if (i.Contains(HelpW))
                            {
                                helpingWordsInThatSentence.Add(HelpW);
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

            if (chars[0] == "MeFinish")
            {
                foreach (string word in chars)
                {
                    if (word != "MeFinish")
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

        audioClip_MeFinish = this.gameObject.GetComponent<AudioSource>();
        audioClip_MeFinish.clip = Resources.Load<AudioClip>(filename);
    }

    public IEnumerator startTime()
    {
        yield return new WaitForSeconds(1);

        ReadText(true);

        getAudio();
        audioClip_MeFinish.Play();

        Background.SetActive(true);

        FindingHelpWords(true);

        getText = Getting();
        StartCoroutine(getText);

        StartCoroutine(STOPM());

        counterBg.SetActive(false);
        counterText.SetActive(false);
    }

    IEnumerator STOPM()
    {
        getAudio();
        float timing = audioClip_MeFinish.clip.length;
        yield return new WaitForSeconds(timing + 5);
        Background.SetActive(false);
        newText.text = "";

        allStayOnGreen.SetActive(false);
        stayOnGreen.SetActive(false);

        StopCoroutine(getText);
    }

    IEnumerator Getting()
    {
        foreach (string i in updatedSentences)
        {
            newText.text = "<color=green>我:</color>" + i;
            yield return new WaitForSeconds(3);
        }
        updatedSentences.Clear();
    }
}
