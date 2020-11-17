using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowPointCloth : MonoBehaviour
{
    public GameObject arrow;
    public GameObject spill;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Cloth")
        {
            arrow.SetActive(false);
            spill.GetComponent<Outline>().enabled = true;
            gameObject.SetActive(false);
        }
    }
}
