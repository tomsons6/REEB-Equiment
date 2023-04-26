using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Task",menuName ="ScriptableObjects/CreateTask",order = 1)]
public class Task : ScriptableObject
{
    public float TreatmentX;
    public float TreatmentY;
    public float TreatmentZ;
    public float TreatmentRotation;
    public float PatientX;
    public float PatientZ;
    public float PatientRotation;
    enum Organ { RightShoulder, LeftShoulder, Heart, Lungs, Head, Stomach, RightArm,LeftArm };
}
