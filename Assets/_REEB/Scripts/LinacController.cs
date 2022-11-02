using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
public class LinacController : MonoBehaviour
{
    [SerializeField]
    GameObject BedBase;
    [SerializeField]
    GameObject FineLift;
    [SerializeField]
    GameObject Bed;
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
    GameObject ButtonFineDown;
    ButtonPush BPFineDOWN;
    [SerializeField]
    GameObject ButtonFineUp;
    ButtonPush BPFineUp;
    [SerializeField]
    GameObject ButtonMoveBedForward;
    ButtonPush BPMoveBedForward;
    [SerializeField]
    GameObject ButtonMoveBedBack;
    ButtonPush BPMoveBedBack;
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
        BPFineDOWN = ButtonFineDown.GetComponent<ButtonPush>(); 
        BPFineUp = ButtonFineUp.GetComponent<ButtonPush>(); 
        BPMoveBedForward = ButtonMoveBedForward.GetComponent<ButtonPush>();
        BPMoveBedBack = ButtonMoveBedBack.GetComponent<ButtonPush>();
    }

    // Update is called once per frame
    void Update()
    {
        BaseLift();
        AparatusMovement();
        FineLiftMovement();
        BedMovement();
    }

    void BaseLift()
    {
        if (BPUP.ButtonPushed)
        {
            if (BedBase.transform.localScale.y <= 2f)
            {
                BedBase.transform.localScale = new Vector3(BedBase.transform.localScale.x, BedBase.transform.localScale.y + .001f, BedBase.transform.localScale.z );
            }
        }
        if (BPDOWN.ButtonPushed)
        {
            if (BedBase.transform.localScale.y >= 1f)
            {
                BedBase.transform.localScale = new Vector3(BedBase.transform.localScale.x, BedBase.transform.localScale.y - .001f, BedBase.transform.localScale.z );
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
    void FineLiftMovement()
    {
        if (BPFineUp.ButtonPushed)
        {
            if (FineLift.transform.localScale.z <= 2f)
            {
                FineLift.transform.localScale = new Vector3(FineLift.transform.localScale.x, FineLift.transform.localScale.y, FineLift.transform.localScale.z + .001f);
            }
        }
        if (BPFineDOWN.ButtonPushed)
        {
            if (FineLift.transform.localScale.z >= 1f)
            {
                FineLift.transform.localScale = new Vector3(FineLift.transform.localScale.x, FineLift.transform.localScale.y, FineLift.transform.localScale.z - .001f);
            }
        }
    }
    void BedMovement()
    {
        if (BPMoveBedForward.ButtonPushed)
        {
            if(Bed.transform.localPosition.y <= 1130f)
            {
                Bed.transform.localPosition = new Vector3(Bed.transform.localPosition.x, Bed.transform.localPosition.y + .1f, Bed.transform.localPosition.z);
            }
        }
        if (BPMoveBedBack.ButtonPushed)
        {
            if(Bed.transform.localPosition.y >= 708f)
            {
                Bed.transform.localPosition = new Vector3(Bed.transform.localPosition.x, Bed.transform.localPosition.y - .1f, Bed.transform.localPosition.z);
            }
        }
    }
}
