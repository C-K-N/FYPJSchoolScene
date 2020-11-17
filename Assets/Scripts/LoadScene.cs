using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    public void loadMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void loadOutside()
    {
        SceneManager.LoadScene("Outside");
    }

    public void loadClassroom()
    {
        SceneManager.LoadScene("Classroom");
    }
}
