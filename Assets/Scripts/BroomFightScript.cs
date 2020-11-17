using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BroomFightScript : MonoBehaviour
{
    IEnumerator AnimC;
    IEnumerator getText;
    public GameObject stayOnGreen;
    public GameObject detectEnter;
    public GameObject arrow;
    public GameObject Background;
    public Animator BBroomFL;
    public Animator BBroomFR;
    public Animator Teacher;
    public AudioSource audioClip_FightT;
    public AudioSource audioClip_FightB;
    public TextMeshProUGUI newText;
    public ArrayList listWords = new ArrayList();
    public ArrayList updatedSentences = new ArrayList();
    public ArrayList NEWupdatedSentences = new ArrayList();
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
            Teacher.SetBool("WalkNow", true);
            BBroomFL.SetBool("StopFightingRight", true);
            BBroomFR.SetBool("StopFightingLeft", true);
            Background.SetActive(true);

            ReadText(true);
            FindHelpingWords(true);

            NEWReadText(true);
            NEWFindHelpingWords(true);

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
            Teacher.SetBool("WalkNow", false);
            BBroomFL.SetBool("StopFightingRight", false);
            BBroomFR.SetBool("StopFightingLeft", false);
            Background.SetActive(false);
            audioClip_FightT.Stop();
            audioClip_FightB.Stop();
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

                if (chars[0] == "FightT")
                {
                    foreach (string word in chars)
                    {
                        if (word != "FightT")
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

                    if (Hchars[0] == "FightT")
                    {
                        foreach (string HelpW in Hchars)
                        {
                            //print(HelpW + "<color=green>:this is HelpW</color>");

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

    public void NEWReadText(bool condition)
    {
        if (condition == true)
        {
            var NEWpath = SentencePath.text;
            var NEWmyText = NEWpath.Split('\n');

            foreach (string i in NEWmyText)
            {
                var NEWchars = i.Split(";".ToCharArray());

                if (NEWchars[0] == "FightB")
                {
                    foreach (string word in NEWchars)
                    {
                        if (word != "FightB")
                        {
                            string cleanSentences = word.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                            listWords.Add(cleanSentences);
                        }
                    }
                }
            }
        }
    }

    public void NEWFindHelpingWords(bool condition)
    {
        ArrayList NEWhelpingWordsInThatSentence = new ArrayList();

        if (condition == true)
        {
            var NEWpath = HelpingPath.text;
            var myNEWText = NEWpath.Split('\n');

            foreach (string NEWi in listWords)
            {
                print("checkSentence 1st foreach " + "<color=Blue>" + NEWi + "</color>");
                foreach (string NEWword in myNEWText)
                {
                    print(NEWword + " this prints the helping words (word)");
                    var NEWchars = NEWword.Split(";".ToCharArray());

                    if (NEWchars[0] == "FightB")
                    {
                        foreach (string NEWHelpW in NEWchars)
                        {
                            //print(NEWHelpW + "<color=green>:this is HelpW</color>");

                            if (NEWi.Contains(NEWHelpW))
                            {
                                NEWhelpingWordsInThatSentence.Add(NEWHelpW);
                                //print("<COLOR=RED>THIS ADDS HelpW: </COLOR>" + NEWHelpW);
                            }
                        }
                    }

                }
                if (NEWhelpingWordsInThatSentence.Count == 0)
                {
                    NEWupdatedSentences.Add(NEWi);
                }
                else
                {
                    foreach (string helping in NEWhelpingWordsInThatSentence)
                    {
                        int start = NEWi.IndexOf(helping);
                        int wordcount = helping.Length;

                        StringBuilder sb = new StringBuilder(NEWi, 50);
                        sb.Insert(start, "<color=red>");
                        sb.Insert(start + wordcount + 11, "</color>");

                        NEWupdatedSentences.Add(sb.ToString());
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

                    NEWhelpingWordsInThatSentence.Clear();
                }
            }
            listWords.Clear();
        }

    }

    public void ReadAFile()
    {
        var audio_path = AudioPath.text;
        var myText2 = audio_path.Split('\n');

        foreach (string i in myText2)
        {
            var chars = i.Split(";".ToCharArray());

            if (chars[0] == "FightT")
            {
                foreach (string word in chars)
                {
                    filename = word;
                    print(filename + "This is filename");
                    if (word != "FightT")
                    {
                        filename = word.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                    }
                }
            }
        }
    }

    public void OtherReadAFile()
    {
        var audio_path = AudioPath.text;
        var myText2 = audio_path.Split('\n');

        foreach (string i in myText2)
        {
            var chars = i.Split(";".ToCharArray());

            if (chars[0] == "FightB")
            {
                foreach (string word in chars)
                {
                    filename = word;
                    print(filename + "This is filename");
                    if (word != "FightB")
                    {
                        filename = word.Replace("\r\n", "").Replace("\r", "").Replace("\n", "");
                    }
                }
            }
        }
    }

    public void getAudio()
    {
        ReadAFile();

        audioClip_FightT = this.gameObject.GetComponent<AudioSource>();
        audioClip_FightT.clip = Resources.Load<AudioClip>(filename);
        //audioClip_FightT.Play();

    }

    public void addAudio()
    {
        OtherReadAFile();

        audioClip_FightB = this.gameObject.GetComponent<AudioSource>();
        audioClip_FightB.clip = Resources.Load<AudioClip>(filename);
        //audioClip_FightB.Play();
    }

    IEnumerator ShowText()
    {
        float timingTurn = 5.5f;
        yield return new WaitForSeconds(timingTurn);
        getAudio();
        audioClip_FightT.Play();
        foreach (string i in updatedSentences)
        {
            //newText.text = Changespeaker() + i;
            
            newText.text = "<color=green>老师:</color>" + i;
            yield return new WaitForSeconds(4);
        }

        yield return new WaitForSeconds(2);
        addAudio();
        audioClip_FightB.Play();
        foreach (string p in NEWupdatedSentences)
        {
            newText.text = "<color=green>亮亮:</color>" + p;
            yield return new WaitForSeconds(2);
        }
            updatedSentences.Clear();
        NEWupdatedSentences.Clear();
        stayOnGreen.SetActive(false);
    }

    IEnumerator ChangeAni()
    {
        //float timing = audioClip_FightA.length;
        yield return new WaitForSeconds(2);
        Teacher.SetBool("DoneTalking", true);

        print("timing" + "<color=Blue><b>This is audioclip length</b></color>");
    }
    
}


