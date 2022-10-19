using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonPush : MonoBehaviour
{
    public bool ButtonPushed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("IndexFinger"))
        {
            ButtonPushed = true;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("IndexFinger"))
        {
            ButtonPushed = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("IndexFinger"))
        {
            ButtonPushed = false;
        }
    }
}
