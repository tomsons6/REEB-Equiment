using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
public class LinacController : MonoBehaviour
{
    [SerializeField]
    GameObject BedBase;
    [SerializeField]
    GameObject LinacAparatusMovement;
    [SerializeField]
    float LinacTurnSpeed = 1.5f;
    [SerializeField]
    GameObject ButtonUp;
    ButtonPush BPUP;
    [SerializeField]
    GameObject ButtonDown;
    ButtonPush BPDOWN;
    [SerializeField]
    GameObject ButtonStart;
    ButtonPush BPStart;


    bool StartLinac = false;

    // Start is called before the first frame update
    void Start()
    {
        BPUP = ButtonUp.GetComponent<ButtonPush>();
        BPDOWN = ButtonDown.GetComponent<ButtonPush>();
        BPStart = ButtonStart.GetComponent<ButtonPush>();
    }

    // Update is called once per frame
    void Update()
    {
        BedMovement();
        AparatusMovement();
    }

    void BedMovement()
    {
        if (BPUP.ButtonPushed)
        {
            if (BedBase.transform.localScale.z <= 2f)
            {
                BedBase.transform.localScale = new Vector3(BedBase.transform.localScale.x, BedBase.transform.localScale.y, BedBase.transform.localScale.z + .001f);
            }
        }
        if (BPDOWN.ButtonPushed)
        {
            if (BedBase.transform.localScale.z >= 1f)
            {
                BedBase.transform.localScale = new Vector3(BedBase.transform.localScale.x, BedBase.transform.localScale.y, BedBase.transform.localScale.z - .001f);
            }
        }
    }
    void AparatusMovement()
    {
        if (BPStart.ButtonPushed && !StartLinac)
        {
            StartLinac = true;
        }
        if (StartLinac)
        {
            LinacAparatusMovement.transform.Rotate(new Vector3(1f, 0f, 0f) * LinacTurnSpeed * Time.deltaTime);
        }
    }
}
