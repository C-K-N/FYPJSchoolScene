using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.XR;

public class MenuScript : MonoBehaviour
{
    public GameObject menu;
    private bool menuShow = false;
    private int count = 0;
    //public GameObject HidePanel;

    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice targetDevice;

    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);

        if (devices.Count > 0)
        {
            targetDevice = devices[0];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (targetDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool menuButtonValue) && menuButtonValue)
        {
            count += 1;
            if (count == 1) // Ensure only when first pressed it sets active
            {
                menu.SetActive(!menuShow);
                menuShow = !menuShow;
            }
        }
        else
            count = 0;
    }

    /*public void OpenCloseMenu()
    {
        if (HidePanel.activeInHierarchy == true)
        {
            HidePanel.SetActive(false);
            print("set to false");
        }
        else if (HidePanel.activeInHierarchy == false)
        {
            HidePanel.SetActive(true);
            print("set to true");
        }
    }

    public void changeSMain()
    {
        SceneManager.LoadScene(0);
        HidePanel.SetActive(false);
    }

    public void changeSTable()
    {
        SceneManager.LoadScene(1);
        HidePanel.SetActive(false);
    }

    public void changeSATable()
    {
        SceneManager.LoadScene(2);
        HidePanel.SetActive(false);
    }

    public void changeSInSweep()
    {
        SceneManager.LoadScene(3);
        HidePanel.SetActive(false);
    }

    public void changeSWindow()
    {
        SceneManager.LoadScene(4);
        HidePanel.SetActive(false);
    }

    public void changeSWhiteboard()
    {
        SceneManager.LoadScene(5);
        HidePanel.SetActive(false);
    }

    public void changeSOutSweep()
    {
        SceneManager.LoadScene(6);
        HidePanel.SetActive(false);
    }

    public void changeSFight()
    {
        SceneManager.LoadScene(7);
        HidePanel.SetActive(false);
    }*/
}
