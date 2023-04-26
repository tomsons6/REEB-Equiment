using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;

public enum CoordinateToEdit
{
    Task1TreatmentX, Task1TreatmentY, Task1TreatmentZ, Task1PatientX, Task1PatientZ,Task1PatientRot,
    Task2TreatmentX, Task2TreatmentY, Task2TreatmentZ, Task2PatientX, Task2PatientZ, Task2PatientRot, Task3TreatmentX, Task3TreatmentY,
    Task3TreatmentZ, Task3PatientX, Task3PatientZ, Task3PatientRot
};

public class TaskController : MonoBehaviour
{
    [SerializeField]
    GameObject TaskPanel;
    [SerializeField]
    GameObject SettingsPanel;
    [SerializeField]
    GameObject TaskDescription;
    [SerializeField]
    TMP_Text TaskInfo;
    [SerializeField]
    Button Task1Button;
    [SerializeField]
    Button Task2Button;
    [SerializeField]
    Button Task3Button;
    [SerializeField]
    Button SettingsButton;

    [SerializeField]
    GameObject StatusTextRoot;
    [SerializeField]
    GameObject StatusTextPref;

    [SerializeField]
    public TMP_InputField SelectedInputField;
    public Task Task1;
    public Task Task2;
    public Task Task3;

    public Task SelectedTask;

    LinacController LinacController;

    public static TaskController instance;

    bool TaskStarted;
    // Start is called before the first frame update
    void Start()
    {
        LinacController = GameObject.FindGameObjectWithTag("Linac").GetComponent<LinacController>();
        LinacController.PatientPositioned += StatusText;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {

    }

    public void SwitchPanels()
    {
        if (TaskPanel.activeInHierarchy)
        {
            TaskPanel.SetActive(false);
            SettingsPanel.SetActive(true);
        }
        else
        {
            TaskPanel.SetActive(true);
            SettingsPanel.SetActive(false);
        }
    }
    public void SelectInputField()
    {
        Debug.Log("SelectInputField");
    }
    public float ReturnCoordinateWrapper(CoordinateToEdit CE)
    {
        Func<float, float> parameter;
        return ReturnCoordinateValue(CE, out parameter);
    }
    public float ReturnCoordinateValue(CoordinateToEdit CE, out Func<float, float> parameter)
    {
        switch (CE)
        {
            case CoordinateToEdit.Task1TreatmentX:
                parameter = returnvalue => Task1.TreatmentX = returnvalue;
                return Task1.TreatmentX;
            case CoordinateToEdit.Task1TreatmentY:
                parameter = returnvalue => Task1.TreatmentY = returnvalue;
                return Task1.TreatmentY;
            case CoordinateToEdit.Task1TreatmentZ:
                parameter = returnvalue => Task1.TreatmentZ = returnvalue;
                return Task1.TreatmentZ;
            case CoordinateToEdit.Task1PatientX:
                parameter = returnvalue => Task1.PatientX = returnvalue;
                return Task1.PatientX;
            case CoordinateToEdit.Task1PatientZ:
                parameter = returnvalue => Task1.PatientZ = returnvalue;
                return Task1.PatientZ;
            case CoordinateToEdit.Task1PatientRot:
                parameter = returnvalue => Task1.PatientRotation = returnvalue;
                return Task1.PatientRotation;
            case CoordinateToEdit.Task2TreatmentX:
                parameter = returnvalue => Task2.TreatmentX = returnvalue;
                return Task2.TreatmentX;
            case CoordinateToEdit.Task2TreatmentY:
                parameter = returnvalue => Task2.TreatmentY = returnvalue;
                return Task2.TreatmentY;
            case CoordinateToEdit.Task2TreatmentZ:
                parameter = returnvalue => Task2.TreatmentZ = returnvalue;
                return Task2.TreatmentZ;
            case CoordinateToEdit.Task2PatientX:
                parameter = returnvalue => Task2.PatientX = returnvalue;
                return Task2.PatientX;
            case CoordinateToEdit.Task2PatientZ:
                parameter = returnvalue => Task2.PatientZ = returnvalue;
                return Task2.PatientZ;
            case CoordinateToEdit.Task2PatientRot:
                parameter = returnvalue => Task2.PatientRotation = returnvalue;
                return Task2.PatientRotation;
            case CoordinateToEdit.Task3TreatmentX:
                parameter = returnvalue => Task3.TreatmentX = returnvalue;
                return Task3.TreatmentX;
            case CoordinateToEdit.Task3TreatmentY:
                parameter = returnvalue => Task3.TreatmentY = returnvalue;
                return Task3.TreatmentY;
            case CoordinateToEdit.Task3TreatmentZ:
                parameter = returnvalue => Task3.TreatmentZ = returnvalue;
                return Task3.TreatmentZ;
            case CoordinateToEdit.Task3PatientX:
                parameter = returnvalue => Task3.PatientX = returnvalue;
                return Task3.PatientX;
            case CoordinateToEdit.Task3PatientZ:
                parameter = returnvalue => Task3.PatientZ = returnvalue;
                return Task3.PatientZ;
            case CoordinateToEdit.Task3PatientRot:
                parameter = returnvalue => Task3.PatientRotation = returnvalue;
                return Task3.PatientRotation;
        }
        float dummyFloat;
        parameter = returnvalue => dummyFloat = returnvalue;
        return 0f;
    }

    public void IncreaseValue()
    {
        float CurrentEditValue;
        if (SelectedInputField != null)
        {
            Func<float, float> parameter;
            CurrentEditValue = ReturnCoordinateValue(SelectedInputField.GetComponent<InputFieldInit>().InputCoordinate, out parameter);
            CurrentEditValue += SelectedInputField.GetComponent<InputFieldInit>().IncreaseValue;
            parameter(CurrentEditValue);
            SelectedInputField.text = CurrentEditValue.ToString();
        }
    }
    public void DecreaseValue()
    {
        float CurrentEditValue;
        if (SelectedInputField != null)
        {
            Func<float, float> parameter;
            CurrentEditValue = ReturnCoordinateValue(SelectedInputField.GetComponent<InputFieldInit>().InputCoordinate, out parameter);
            CurrentEditValue -= SelectedInputField.GetComponent<InputFieldInit>().IncreaseValue; ;
            parameter(CurrentEditValue);
            SelectedInputField.text = CurrentEditValue.ToString();
        }
    }

    public void StatusText()
    {
        if (LinacController.PatientInPosition)
        {
            GameObject TempGo = Instantiate(StatusTextPref,StatusTextRoot.transform);
            TempGo.GetComponent<TMP_Text>().color = Color.green;
            TempGo.GetComponent<TMP_Text>().text = "Status: Patient is aligned to lasers ";
        }
        else
        {
            GameObject TempGo = Instantiate(StatusTextPref, StatusTextRoot.transform);
            TempGo.GetComponent<TMP_Text>().color = Color.red;
            TempGo.GetComponent<TMP_Text>().text = "Status: Patient is NOT aligned to lasers ";
        }
        if (LinacController.LaserPositionCorrect)
        {
            GameObject TempGo = Instantiate(StatusTextPref, StatusTextRoot.transform);
            TempGo.GetComponent<TMP_Text>().text = "Status: Align patient to the treatment card ";
        }
    }

    public void SelectTask(Task SelTask) 
    {
        SelectedTask = SelTask;
        TaskInfo.text = "Treatment position: " + "\nTreatment X position = "
            + SelectedTask.TreatmentX.ToString() + "\nTreatment Y position = "
            + SelectedTask.TreatmentY.ToString() + "\nTreatment Z position = "
            + SelectedTask.TreatmentZ.ToString();
    }
    public void StartTask()
    {
        if (!TaskDescription.activeInHierarchy)
        {
            TaskDescription.SetActive(true);
        }
        Task1Button.interactable = false;
        Task2Button.interactable = false;
        Task3Button.interactable = false;
        SettingsButton.interactable = false;
        LinacController.DisableSensorControls();
        StatusText();
        TaskStarted = true;
    }

    public void CheckResult()
    {
        Task1Button.interactable = true;
        Task2Button.interactable = true;
        Task3Button.interactable = true;
        SettingsButton.interactable = true;
        TaskStarted = false;
    }
}
