using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using System.Linq;

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
    [SerializeField]
    GameObject ButtonSideLaser;
    ButtonPush BPSideLaser;
    [SerializeField]
    GameObject SideLaser;
    bool SideLaserActive = true;
    bool SideLaserReset = false;
    [SerializeField]
    GameObject ButtonPerpedicularLaser;
    ButtonPush BPPerpedicularLaser;
    [SerializeField]
    GameObject PerpedicualrLaser;
    bool PerpedicualLaserActive = true;
    bool PerpedicualLaserReset =false;
    [SerializeField]
    AudioSource MotorNoise;
    [SerializeField]
    List<ButtonPush> ButtonList;

    [SerializeField]
    AudioSource ButtonClick;


    bool StartLinac = false;

    // Start is called before the first frame update
    void Start()
    {
        //BPUP = ButtonUp.GetComponent<ButtonPush>();

        //BPDOWN = ButtonDown.GetComponent<ButtonPush>();
        //BPStart = ButtonStart.GetComponent<ButtonPush>();
        //BPFineDOWN = ButtonFineDown.GetComponent<ButtonPush>();
        //BPFineUp = ButtonFineUp.GetComponent<ButtonPush>();
        //BPMoveBedForward = ButtonMoveBedForward.GetComponent<ButtonPush>();
        //BPMoveBedBack = ButtonMoveBedBack.GetComponent<ButtonPush>();
        //BPSideLaser = ButtonSideLaser.GetComponent<ButtonPush>();
        //BPPerpedicularLaser = ButtonPerpedicularLaser.GetComponent<ButtonPush>();
        //ButtonList.Add(BPUP);
        //ButtonList.Add(BPDOWN);
        //ButtonList.Add(BPMoveBedForward);
        //ButtonList.Add(BPMoveBedBack);
        //ButtonList.Add(BPFineUp);
        //ButtonList.Add(BPFineDOWN);

    }

    // Update is called once per frame
    void Update()
    {
        //AudioToggleOff();
        ////BaseLift();
        AparatusMovement();
        //FineLiftMovement();
        //BedMovement();
        //LaserToggle();

    }

    public void BaseLiftUp()
    {
        //if (BPUP.ButtonPushed)
        //{
            if (BedBase.transform.localScale.y <= 2f)
            {
                BedBase.transform.localScale = new Vector3(BedBase.transform.localScale.x, BedBase.transform.localScale.y + .001f, BedBase.transform.localScale.z);
                AudioToggleON();
            }
        //}
    }
    public void BaseLiftDown()
    {
        if (BedBase.transform.localScale.y >= 1f)
        {
            BedBase.transform.localScale = new Vector3(BedBase.transform.localScale.x, BedBase.transform.localScale.y - .001f, BedBase.transform.localScale.z);
            AudioToggleON();
        }
    }
    public void AparatusMovement()
    {
        if (StartLinac)
        {
            LinacAparatusMovement.transform.Rotate(new Vector3(1f, 0f, 0f) * LinacTurnSpeed * Time.deltaTime);
        }
    }
    public void StartLinacMachine()
    {
        if (!StartLinac)
        {
            StartLinac = true;
        }
    }
    public void FineLiftMovementUp()
    {
        //if (BPFineUp.ButtonPushed)
        //{
            if (FineLift.transform.localScale.z <= 2f)
            {
                FineLift.transform.localScale = new Vector3(FineLift.transform.localScale.x, FineLift.transform.localScale.y, FineLift.transform.localScale.z + .001f);
                AudioToggleON();
            }
        //}

        //if (BPFineDOWN.ButtonPushed)
        //{

        //}

    }
    public void FineLiftMovementDown()
    {
        if (FineLift.transform.localScale.z >= 1f)
        {
            FineLift.transform.localScale = new Vector3(FineLift.transform.localScale.x, FineLift.transform.localScale.y, FineLift.transform.localScale.z - .001f);
            AudioToggleON();
        }
    }
    public void BedMovementForward()
    {
        //if (BPMoveBedForward.ButtonPushed)
        //{
            if (Bed.transform.localPosition.y <= 1130f)
            {
                Bed.transform.localPosition = new Vector3(Bed.transform.localPosition.x, Bed.transform.localPosition.y + .1f, Bed.transform.localPosition.z);
                AudioToggleON();
            }
        //}

    }
    public void BedMovementBack()
    {
        //if (BPMoveBedBack.ButtonPushed)
        //{
            if (Bed.transform.localPosition.y >= 708f)
            {
                Bed.transform.localPosition = new Vector3(Bed.transform.localPosition.x, Bed.transform.localPosition.y - .1f, Bed.transform.localPosition.z);
                AudioToggleON();
            }
        //}
    }
    void AudioToggleON()
    {
        //if (ButtonList.Any(x => x.ButtonPushed))
        //{
        //    Debug.Log("AnyButton Presed");
            if (!MotorNoise.isPlaying)
            {
                MotorNoise.Play();
            }
        //}
    }
    public void AudioToggleOff()
    {
        //if(ButtonList.All(x => !x.ButtonPushed))
        //{
            MotorNoise.Stop();
        //}
    }
    public void SideLaserToggle()
    {
        //if (!BPSideLaser.ButtonPushed)
        //{
        //    SideLaserReset = false;
        //}
        //if (BPSideLaser.ButtonPushed)
        //{
        //    if (!SideLaserReset)
        //    {
        //        SideLaserReset = true;
                if (SideLaserActive)
                {
                    SideLaser.SetActive(false);
                    SideLaserActive = false;
                }
                else
                {
                    SideLaser.SetActive(true);
                    SideLaserActive = true;
                }
        //    }
        //}

    }
    public void PerpedicularLaserToggle()
    {
        //if (!BPPerpedicularLaser.ButtonPushed)
        //{
        //    PerpedicualLaserReset = false;
        //}
        //if (BPPerpedicularLaser.ButtonPushed)
        //{
        //    if (!PerpedicualLaserReset)
        //    {
        //        PerpedicualLaserReset = true;
                if (PerpedicualLaserActive)
                {
                    PerpedicualrLaser.SetActive(false);
                    PerpedicualLaserActive = false;
                }
                else
                {
                    PerpedicualrLaser.SetActive(true);
                    PerpedicualLaserActive = true;
                }
        //    }

        //}
    }
    public void ButtonClickSound()
    {
        ButtonClick.Play();
    }
}
