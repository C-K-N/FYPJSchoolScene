using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class DetectHelp : MonoBehaviour
{
    public GameObject pointGirlHand;
    public Animator GFall;

    public InputDeviceCharacteristics controllerCharacteristics;
    private InputDevice targetDevice;

    // Start is called before the first frame update
    void Start()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristics, devices);
        targetDevice = devices[0];
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("GirlHand"))
        {
            if (GFall.GetBool("BeginWalk") == true)
            {
                targetDevice.SendHapticImpulse(0, 0.2f, 1.0f);

                if (targetDevice.TryGetFeatureValue(CommonUsages.grip, out float gripValue) && gripValue > 0.2f)
                {
                    GFall.SetBool("PlayerHelp", true);
                    pointGirlHand.SetActive(false);
                }
            }
        }
    }
}
