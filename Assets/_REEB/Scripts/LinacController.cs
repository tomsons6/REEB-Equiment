using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BNG;
using System.Linq;
using TMPro;
using System;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

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
    GameObject BedSvivle;
    [SerializeField]
    float LinacTurnSpeed = 3f;

    //Lasers
    [SerializeField]
    GameObject SideLaser;
    [SerializeField]
    GameObject PerpedicualrLaser;
    [SerializeField]
    GameObject TopLaser;
    //Sounds
    [SerializeField]
    AudioSource MotorNoise;
    [SerializeField]
    AudioSource ButtonClick;
    //UI
    [SerializeField]
    TMP_Text BedUpDownText;
    float BedUpDownNumber = 0f;
    [SerializeField]
    TMP_Text FineUpDownText;
    float FineUpDownNumber = 0f;
    [SerializeField]
    TMP_Text ForwardBackText;
    float ForwardBackNumber = 0f;
    //Potential UI
    [SerializeField]
    GameObject SensorControls;
    [SerializeField]
    GameObject BedControls;
    [SerializeField]
    Button ToggleControlsBtn;
    //Scanners
    [SerializeField]
    GameObject SmallScanner;
    [SerializeField]
    GameObject FlatScanner1;
    Transform FlatScanner1RotateBase;
    [SerializeField]
    GameObject FlatScanner2;
    Transform FlatScanner2RotateBase;
    GameObject SelectedScanner;
    Vector3 StartTransformSmallSC;

    [SerializeField]
    TMP_Text SelectedScannerText;
    int SensorID = 0;

    //Validation
    [SerializeField]
    float PositionTreshold = .1f;
    [SerializeField]
    float TreatmentRotThreshold = 2f;
    [SerializeField]
    GameObject Tattoo1;
    [SerializeField]
    GameObject Tattoo2;

    Vector3 PatientStartPosition;

    public bool PatientInPosition;
    public bool TreatmentInPosition;
    bool PatientSideInPos;
    bool PatientPerpInPos;
    bool PatientParInPos;
    public bool LaserPositionCorrect = false;
    bool TreatmentPatientXPos;
    bool TreatmentPatientZPos;
    bool TreatmentPatientRot;
    public bool PatientForTreatmentPositioned;

    public delegate void PatientInPlace();
    public static PatientInPlace PatientPositioned;

    // Start is called before the first frame update
    void Start()
    {
        BedUpDownText.text = "Base lift = 0.00 CM";
        FineUpDownText.text = "Small lift = 0.00 CM";
        ForwardBackText.text = "Bed movement = 0.00 CM";
        //SensorMovement.onClick.AddListener(delegate { MoveSensorForward(SmallScanner, StartTransformSmallSC); });
        SensorSelect();
        FlatScanner1RotateBase = FlatScanner1.transform.parent;
        FlatScanner2RotateBase = FlatScanner2.transform.parent;
        PatientStartPosition = Bed.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.K))
        {
            RotateBedLeft();
        }
        if (Input.GetKey(KeyCode.L))
        {
            RotateBedRight();
        }
        if (Input.GetKey(KeyCode.J))
        {
            AparatusMovementRight();
        }
        if (!LaserPositionCorrect)
        {
            LaserValidation();
        }
        else
        {
            TreatmentValidation();
        }
        PatientTreatmentPosition();
    }
    #region Bed Movement
    public void BaseLiftUp()
    {
        if (BedBase.transform.localScale.y <= 2f)
        {
            BedUpDownNumber += .04f;
            BedUpDownText.text = "Base lift = " + BedUpDownNumber.ToString("0.00") + " CM";
            BedBase.transform.localScale = new Vector3(BedBase.transform.localScale.x, BedBase.transform.localScale.y + .0025f, BedBase.transform.localScale.z);
            AudioToggleON();
        }
    }
    public void BaseLiftDown()
    {
        if (BedBase.transform.localScale.y >= 1f)
        {
            BedUpDownNumber -= .04f;
            BedUpDownText.text = "Base lift = " + BedUpDownNumber.ToString("0.00") + " CM";
            BedBase.transform.localScale = new Vector3(BedBase.transform.localScale.x, BedBase.transform.localScale.y - .0025f, BedBase.transform.localScale.z);
            AudioToggleON();
        }
    }
    public void FineLiftMovementUp()
    {
        if (FineLift.transform.localScale.z <= 2f)
        {
            FineUpDownNumber += .01f;
            FineUpDownText.text = "Small lift = " + FineUpDownNumber.ToString("0.00") + " CM";
            FineLift.transform.localScale = new Vector3(FineLift.transform.localScale.x, FineLift.transform.localScale.y, FineLift.transform.localScale.z + .003f);
            AudioToggleON();
        }
    }
    public void FineLiftMovementDown()
    {
        if (FineLift.transform.localScale.z >= 1f)
        {
            FineUpDownNumber -= .01f;
            FineUpDownText.text = "Small lift = " + FineUpDownNumber.ToString("0.00") + " CM";
            FineLift.transform.localScale = new Vector3(FineLift.transform.localScale.x, FineLift.transform.localScale.y, FineLift.transform.localScale.z - .003f);
            AudioToggleON();
        }
    }
    public void BedMovementForward()
    {
        if (Bed.transform.localPosition.y <= 1130f)
        {
            ForwardBackNumber += .025f;
            ForwardBackText.text = "Bed movement = " + ForwardBackNumber.ToString("0.00") + " CM";
            Bed.transform.localPosition = new Vector3(Bed.transform.localPosition.x, Bed.transform.localPosition.y + .35f, Bed.transform.localPosition.z);
            AudioToggleON();
        }
    }
    public void BedMovementBack()
    {
        if (Bed.transform.localPosition.y >= 708f)
        {
            ForwardBackNumber -= .025f;
            ForwardBackText.text = "Bed movement = " + ForwardBackNumber.ToString("0.00") + " CM";
            Bed.transform.localPosition = new Vector3(Bed.transform.localPosition.x, Bed.transform.localPosition.y - .35f, Bed.transform.localPosition.z);
            AudioToggleON();
        }
    }

    public void RotateBedRight()
    {
        Debug.Log(BedSvivle.transform.localRotation.y);
        if (BedSvivle.transform.localRotation.y >= -.15f)
        {
            BedSvivle.transform.Rotate(eulers: -Vector3.up * .05f, Space.Self);
        }
    }
    public void RotateBedLeft()
    {
        Debug.Log(BedSvivle.transform.localRotation.y);
        if (BedSvivle.transform.localRotation.y <= .15f)
        {
            BedSvivle.transform.Rotate(eulers: Vector3.up * .05f, Space.Self);
        }
    }
    #endregion
    #region Linac Movement
    public void AparatusMovementRight()
    {
        LinacAparatusMovement.transform.Rotate(new Vector3(1f, 0f, 0f) * LinacTurnSpeed * Time.deltaTime);
    }
    public void AparatusMovementLeft()
    {
        LinacAparatusMovement.transform.Rotate(new Vector3(-1f, 0f, 0f) * LinacTurnSpeed * Time.deltaTime);
    }

    #endregion
    #region Laser Logic
    public void PerpedicularLaserToggle()
    {
        if (PerpedicualrLaser.activeInHierarchy)
        {
            PerpedicualrLaser.SetActive(false);
        }
        else
        {
            PerpedicualrLaser.SetActive(true);
        }
    }
    public void ToggleLasers()
    {
        if (TopLaser.activeInHierarchy)
        {
            TopLaser.SetActive(false);
            PerpedicualrLaser.SetActive(false);
            SideLaser.SetActive(false);
        }
        else
        {
            TopLaser.SetActive(true);
            PerpedicualrLaser.SetActive(true);
            SideLaser.SetActive(true);
        }
    }
    public void SideLaserToggle()
    {
        if (SideLaser.activeInHierarchy)
        {
            SideLaser.SetActive(false);
        }
        else
        {
            SideLaser.SetActive(true);

        }
    }
    #endregion
    #region Audio
    void AudioToggleON()
    {
        if (!MotorNoise.isPlaying)
        {
            MotorNoise.Play();
        }

    }
    public void AudioToggleOff()
    {
        MotorNoise.Stop();
    }

    public void ButtonClickSound()
    {
        ButtonClick.Play();
    }
    #endregion
    #region Sensor Movement
    public void MoveSensorForward()
    {
        if (StartTransformSmallSC == null)
        {
            StartTransformSmallSC = SelectedScanner.transform.localPosition;
        }
        if (SelectedScanner.transform.localPosition.y >= StartTransformSmallSC.y - 500f)
        {
            SelectedScanner.transform.localPosition = new Vector3(SelectedScanner.transform.localPosition.x, SelectedScanner.transform.localPosition.y - 1f, SelectedScanner.transform.localPosition.z);
        }
    }
    public void MoveSensorBack()
    {
        if (SelectedScanner.transform.localPosition.y <= StartTransformSmallSC.y)
        {
            SelectedScanner.transform.localPosition = new Vector3(SelectedScanner.transform.localPosition.x, SelectedScanner.transform.localPosition.y + 1f, SelectedScanner.transform.localPosition.z);
        }
    }

    public void SensorSelect()
    {
        if (SensorID == 3)
        {
            SensorID = 0;
        }
        switch (SensorID)
        {
            case 0:
                SensorID++;
                SelectedScanner = SmallScanner;
                SelectedScannerText.text = "Selected scanner: " + SelectedScanner.name;
                break;
            case 1:
                SensorID++;
                SelectedScanner = FlatScanner1;
                SelectedScannerText.text = "Selected scanner: " + SelectedScanner.name;
                break;
            case 2:
                SensorID++;
                SelectedScanner = FlatScanner2;
                SelectedScannerText.text = "Selected scanner: " + SelectedScanner.name;
                break;
        }
    }
    public void OpeningSelectedScanner()
    {
        if (SelectedScanner == FlatScanner1)
        {
            OpenScanner1();
        }
        if (SelectedScanner == FlatScanner2)
        {
            OpenScanner2();
        }
    }
    public void ClosingSelectedScanner()
    {
        if (SelectedScanner == FlatScanner1)
        {
            CloseScanner1();
        }
        if (SelectedScanner == FlatScanner2)
        {
            CloseScanner2();
        }
    }
    public void OpenScanner1()
    {
        if (FlatScanner1RotateBase.localRotation.z < 0f)
        {
            //FlatScanner1RotateBase.eulerAngles = new Vector3(FlatScanner1RotateBase.localRotation.x, FlatScanner1RotateBase.localRotation.y + .2f, FlatScanner1RotateBase.localRotation.z ); 
            FlatScanner1RotateBase.Rotate(eulers: Vector3.forward * .2f, Space.Self);
        }
    }
    public void CloseScanner1()
    {
        if (FlatScanner1RotateBase.localRotation.z >= -.705f)
        {
            FlatScanner1RotateBase.Rotate(eulers: -Vector3.forward * .2f, Space.Self);
            // FlatScanner1RotateBase.eulerAngles = new Vector3(FlatScanner1RotateBase.localRotation.x, FlatScanner1RotateBase.localRotation.y - .2f, FlatScanner1RotateBase.localRotation.z);
            //FlatScanner1RotateBase.localRotation = new Quaternion(FlatScanner1RotateBase.localRotation.x, FlatScanner1RotateBase.localRotation.y, FlatScanner1RotateBase.localRotation.z - .2f, FlatScanner1RotateBase.localRotation.w);
        }

    }
    public void OpenScanner2()
    {
        if (FlatScanner2RotateBase.localRotation.x < 0f)
        {
            FlatScanner2RotateBase.Rotate(eulers: Vector3.right * .2f, Space.Self);
        }
    }
    public void CloseScanner2()
    {
        if (FlatScanner2RotateBase.localRotation.x >= -.705f)
        {
            FlatScanner2RotateBase.Rotate(eulers: -Vector3.right * .2f, Space.Self);
        }
    }
    #endregion
    #region Validation

    void LaserValidation()
    {
        if (SideLaser.transform.position.y - PositionTreshold < Tattoo1.transform.position.y && SideLaser.transform.position.y + PositionTreshold > Tattoo1.transform.position.y)
        {
            PatientSideInPos = true;
        }
        else
        {
            PatientSideInPos = false;
        }
        if (PerpedicualrLaser.transform.position.x - PositionTreshold < Tattoo2.transform.position.x && PerpedicualrLaser.transform.position.x + PositionTreshold > Tattoo2.transform.position.x)
        {
            PatientPerpInPos = true;
        }
        else
        {
            PatientPerpInPos = false;
        }
        if (TopLaser.transform.position.z - PositionTreshold < Tattoo2.transform.position.z && TopLaser.transform.position.z + PositionTreshold > Tattoo2.transform.position.z)
        {
            PatientParInPos = true;
        }
        else
        {
            PatientParInPos = false;
        }
        if(PatientParInPos && PatientPerpInPos && PatientSideInPos)
        {
            if (!PatientInPosition)
            {
                PatientInPosition = true;
                LaserPositionCorrect = true;
                PatientPositioned?.Invoke();
            }
        }
        else
        {
            if (PatientInPosition)
            {
                PatientInPosition = false;
                PatientPositioned?.Invoke();
            }
        }

    }

    void TreatmentValidation()
    {
        if(TaskController.instance.SelectedTask != null)
        {
            Debug.Log(LinacAparatusMovement.transform.rotation.eulerAngles.x);
            if (LinacAparatusMovement.transform.rotation.eulerAngles.x > TaskController.instance.SelectedTask.TreatmentRotation -TreatmentRotThreshold && LinacAparatusMovement.transform.rotation.eulerAngles.x < TaskController.instance.SelectedTask.TreatmentRotation + TreatmentRotThreshold)
            {
                TreatmentInPosition = true;
            }
            else
            {
                TreatmentInPosition = false;
            }
        }
    }
    void PatientTreatmentPosition()
    {

        if(BedSvivle.transform.rotation.eulerAngles.y -180f > TaskController.instance.SelectedTask.PatientRotation - TreatmentRotThreshold && BedSvivle.transform.rotation.eulerAngles.y - 180f < TaskController.instance.SelectedTask.PatientRotation + TreatmentRotThreshold )
        {
            TreatmentPatientRot = true;
            Debug.Log("Bed rotation good");
        }
        else
        {
            TreatmentPatientRot = false;
            Debug.Log("Bed rotation bad");
        }
        if(Bed.transform.position.x > PatientStartPosition.x + TaskController.instance.SelectedTask.PatientX - PositionTreshold && Bed.transform.position.x < PatientStartPosition.x + TaskController.instance.SelectedTask.PatientX + PositionTreshold)
        {
            Debug.Log("PatientInXGood");
        }
        else
        {
            Debug.Log("PatienInXBad");
        }
        if (Bed.transform.position.z > PatientStartPosition.z + TaskController.instance.SelectedTask.PatientZ - PositionTreshold && Bed.transform.position.z < PatientStartPosition.z + TaskController.instance.SelectedTask.PatientZ + PositionTreshold)
        {
            Debug.Log("PatientInZGood");
        }
        else
        {
            Debug.Log("PatienInZBad");
        }
    }
    #endregion
    public void ToggleControls()
    {
        if (SensorControls.activeInHierarchy)
        {
            SensorControls.SetActive(false);
            BedControls.SetActive(true);
        }
        else
        {
            BedControls.SetActive(false);
            SensorControls.SetActive(true);
        }
    }
    public void DisableSensorControls()
    {
        if (SensorControls.activeInHierarchy)
        {
            SensorControls.SetActive(false);
            BedControls.SetActive(true);
        }
        ToggleControlsBtn.interactable = false;
    }
}
