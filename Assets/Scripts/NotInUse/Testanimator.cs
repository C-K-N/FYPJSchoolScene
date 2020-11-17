using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class Testanimator : MonoBehaviour
{
    IEnumerator myobj;
    public GameObject arrow;
    public GameObject Background;
    public Animator BoyCloth;
    public AudioSource BoyClothA;
    public AudioSource BackgroundAudio;
    public string audioText;
    public Text newText;
    public ArrayList listWords = new ArrayList();
    public ArrayList updatedSentences = new ArrayList();
    //public ArrayList helpingWords = new ArrayList();
    public TextAsset SentencePath;
    public TextAsset HelpingPath;


    private void OnTriggerEnter(Collider other)
    {
        arrow.SetActive(false);
        BoyCloth.SetBool("IsInFront", true);
        Background.SetActive(true);
        ReadTextFile(true);
        checkHelpWords(true);
        myobj = Getting();
        StartCoroutine(myobj);
        BoyClothA.Play();
    }

    private void OnTriggerExit(Collider other)
    {
        arrow.SetActive(true);
        BoyCloth.SetBool("IsInFront", false);
        Background.SetActive(false);
        BoyClothA.Stop();
        StopCoroutine(myobj);
        newText.text = "";
    }

    // Start is called before the first frame update
    void Start()
    {
        Background.SetActive(false);
        newText.text = "";
        BoyClothA.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadTextFile(bool condition)
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

                            /*foreach (string d in chars)
                            {
                                print(d + "3");
                            }*/

                        }
                    }
                }
            }
        }
    }

    public void checkHelpWords(bool condition)
    {
        ArrayList helpingWordsInThatSentence = new ArrayList();

        if (condition == true)
        {
            var Hpath = HelpingPath.text;
            var myHText = Hpath.Split('\n');

            foreach (string i in listWords)
            {
                print("checkSentence 1st foreach " + "<color=Blue>"+i+"</color>");
                foreach (string word in myHText)
                {
                    print(word + " this prints the helping words (word)");
                    var Hchars = word.Split(";".ToCharArray());
                    
                   // print(hchars[0] + ": prints HCHARS[0]");
                   // print(hchars[1] + ": prints HCHARS[1]");

                    if (Hchars[0] == "BCloth")
                    {
                        foreach (string HelpW in Hchars)
                        {
                            print(HelpW + "<color=green>:this is HelpW</color>");

                            if (i.Contains(HelpW))
                            {
                                helpingWordsInThatSentence.Add(HelpW);
                                print("<COLOR=RED>THIS ADDS HelpW: </COLOR>" + HelpW);

                               /* foreach (string g in helpingWordsInThatSentence)
                                {
                                    print(g + (1+1));
                                }*/
                            }
                        } 
                    }
                        
                }
                if (helpingWordsInThatSentence.Count == 0)
                {
                    updatedSentences.Add(i);
                    print(i + "helping word sentence count is 0");
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

                        foreach (char PD in helping)
                        {
                            print(PD + " -here line 153 this is VARIABLE HELPING");
                        }
                        foreach (string p in updatedSentences)
                        {
                            print(p + " -here line 152 this is UPDATED SENTENCES");
                        }
                    }

                    helpingWordsInThatSentence.Clear();
                }
            }
            listWords.Clear();
            //helpingWords.Clear();
        }

    }

    IEnumerator Getting()
    {
        foreach (string i in updatedSentences)
        {
            newText.text = "<color=green>neighbour:</color>" + i;
            yield return new WaitForSeconds(2);
        }
        updatedSentences.Clear();
        /*foreach (string pg in updatedSentences)
        {
            print(pg + " -here line 175 this is UPDATED SENTENCES AFTER CLEAR()");
        }*/
    }
}
