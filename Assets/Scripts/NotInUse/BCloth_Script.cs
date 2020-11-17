using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class BCloth_Script : MonoBehaviour
{
    IEnumerator myobj;
    IEnumerator newobj;
    public GameObject arrow;
    public GameObject Background;
    public Animator BoyCloth;
    public AudioSource ElderlySource;
    public AudioSource BClothA;
    public Text newText;
    public string audioText;
    public ArrayList listWords = new ArrayList();
    public ArrayList helpingWords = new ArrayList();
    public ArrayList updatedSentences = new ArrayList();
    public TextAsset SentencePath;
    public TextAsset HelpingPath;
    public TextAsset AudioPath;


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "player")
        {
            arrow.SetActive(false);
            BoyCloth.SetBool("IsInFront", true);
            checkCollision();

            newobj = ChangeAnim();
            StartCoroutine(newobj);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "player")
        {
            arrow.SetActive(true);
            BoyCloth.SetBool("IsInFront", false);
            checkCollision();
        }
    }

    public void checkCollision()
    {
        var checking = BoyCloth.GetBool("IsInFront");

        if (checking == true)
        {
            Background.SetActive(true);
            ReadString(true);
            BClothA.Stop();

            ElderlySource = GetComponent<AudioSource>();
            ElderlySource.clip = Resources.Load<AudioClip>(audioText);
            ElderlySource.Play();

            checkSentence(true);
            myobj = Getting();
            StartCoroutine(myobj);

        }
        else if (checking == false)
        {
            newText.text = "";
            Background.SetActive(false);
            ReadString(false);
            checkSentence(false);
            ElderlySource.Stop();
            BClothA.Play();
            StopCoroutine(myobj);

        }
    }

    public void ReadString(bool condition)
    {
        if (condition == true)
        {
            var path = SentencePath.text;
            var myText = path.Split('\n');

            foreach (string i in myText)
            {
                var chars = i.Split(";".ToCharArray());

                if (chars[0] == "BCloth")
                {
                    foreach (string word in chars)
                    {
                        if (word != "BCloth")
                        {
                            string cleanSentences = word.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                            listWords.Add(cleanSentences);
                        }
                    }
                }
            }
        }
    }

    public void checkSentence(bool condition)
    {
        ArrayList helpingWordsInThatSentence = new ArrayList();

        if (condition == true)
        {
            foreach (string i in listWords)
            {
                foreach (string word in helpingWords)
                {
                    if (i.Contains(word))
                    {
                        helpingWordsInThatSentence.Add(word);
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
            helpingWords.Clear();
        }

    }

    IEnumerator Getting()
    {
        foreach (string i in updatedSentences)
        {
            newText.text = "<color=green>neighbour:</color>" + i;
            yield return new WaitForSeconds(4);
        }
    }

    IEnumerator ChangeAnim()
    {
        float timing = ElderlySource.clip.length;
        yield return new WaitForSeconds(timing);
        BoyCloth.SetBool("IsInFront", true);
    }

    // Start is called before the first frame update
    void Start()
    {
        Background.SetActive(false);
        ElderlySource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
