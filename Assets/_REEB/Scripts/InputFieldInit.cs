using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InputFieldInit : MonoBehaviour
{
    private TMP_InputField m_InputField;
    private bool m_IsInitialized = false;
    public CoordinateToEdit InputCoordinate;
    private EventTrigger m_EventTrigger;
    public float IncreaseValue;

    // Start is called before the first frame update
    void Awake ()
    {
        Init ();
    }

    void OnEnable ()
    {
        Init();
    }
    private void Start()
    {
        m_InputField.text = TaskController.instance.ReturnCoordinateWrapper(InputCoordinate).ToString();
    }

    private void Init ()
    {
        if (m_IsInitialized == false || m_InputField == null)
        {
            m_InputField = gameObject.GetComponent<TMP_InputField> ();

            // m_EventTrigger = gameObject.TryGetComponent(out EventTrigger et) ? et : gameObject.AddComponent<EventTrigger>();
            // m_EventTrigger.triggers.Clear();
            // EventTrigger.Entry entry = new EventTrigger.Entry();
            // entry.eventID = EventTriggerType.Deselect;
            // entry.callback.AddListener(AddOnDeselect);
            // m_EventTrigger.triggers.Add(entry);

            

            m_InputField.onSelect.AddListener (AddOnSelect);
            m_InputField.onEndEdit.AddListener(AddOnDeselect);
            m_InputField.onDeselect.AddListener (AddOnDeselect);
            m_IsInitialized = true;
        }
    }

    private void AddOnSelect (string value)
    {
        TaskController.instance.SelectedInputField = m_InputField;
        //GetComponent<Image>().color = Color.green;
        //KeyboardOverseer.Instance.ActivateKeyboard (m_InputField);
    }

    private void AddOnDeselect (string value)
    {
        //TaskController.instance.SelectedInputField = null;
    }

        private void AddOnDeselectClick (BaseEventData eventData)
    {
        //KeyboardOverseer.Instance.DeactivateKeyboard ();
        GetComponent<Image>().color = Color.white;
        //TaskController.instance.SelectedInputField = null;
    }
}