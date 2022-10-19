using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class RayCastLaser : MonoBehaviour
{

    LineRenderer LR;
    [SerializeField]
    int LineCount = 10;
    [SerializeField]
    [Range(60f, 180f)]
    float MaxAngle = 120f;
    [SerializeField]
    float BreakLineAngleThreshhold = .1f;
    [SerializeField]
    float AngleOffset;
    [SerializeField]
    float startAngle = 60f;
    float currentAngle;
    [SerializeField]
    Material LineProjectMaterial;
    [SerializeField]
    Vector3[] RayPointHits;
    [SerializeField]
    public List<ListClass> ListObjects;

    // Start is called before the first frame update
    void Start()
    {
        //LR = GetComponent<LineRenderer>();
        //LR.material = LineProjectMaterial;
        //LR.positionCount = LineCount;
        AngleOffset = MaxAngle / LineCount;
        RayPointHits = new Vector3[LineCount];

    }

    // Update is called once per frame
    void Update()
    {
        RaycastLaserLine();
    }

    void RaycastLaserLine()
    {
        //SomeWhat Working code
        for (int i = 0; i < LineCount; i++)
        {
            if (i != 0)
            {
                currentAngle += AngleOffset;
            }
            else
            {
                currentAngle = startAngle;
            }
            Quaternion spreadAngle = Quaternion.AngleAxis(currentAngle - 120f, new Vector3(0, 0, 1));
            Debug.DrawRay(transform.position, transform.TransformDirection(spreadAngle * -Vector3.up) * 6, Color.red);

            if (Physics.Raycast(transform.position, transform.TransformDirection(spreadAngle * -Vector3.up), out RaycastHit hit, 10f))
            {

                if (RayPointHits[i] != hit.point)
                {
                    RayPointHits[i] = hit.point;
                }

                //LR.SetPosition(i, hit.point);
            }

        }
        ListObjects.Clear();

        int index = 0;
        Vector3 TempVect = new Vector3();
        for (int i = 0; i < RayPointHits.Length - 1; i++)
        {
            if(i == 0)
            {
                TempVect = RayPointHits[i];
                List<Vector3> TempList = new List<Vector3>();
                ListObjects.Add(new ListClass(TempList));
                ListObjects[index].PointList.Add(RayPointHits[i]);
            }
            if (i >=1)
            {
                Debug.Log(TempVect);
                if (RayPointHits[i].y < TempVect.y + .05f || RayPointHits[i].y > TempVect.y - .05f)
                {
                    ListObjects[index].PointList.Add(RayPointHits[i]);
                }
            }
            if (RayPointHits[i].y > RayPointHits[i + 1].y + BreakLineAngleThreshhold || RayPointHits[i].y < RayPointHits[i + 1].y - BreakLineAngleThreshhold)
            {
                index++;
                TempVect = RayPointHits[i];
                List<Vector3> TempList = new List<Vector3>();
                ListObjects.Add(new ListClass(TempList));
                ListObjects[index-1].PointList.Add(RayPointHits[i]);
            }


        }
        foreach (ListClass ListObject in ListObjects)
        {
            if (this.gameObject.transform.childCount < ListObjects.Count)
            {
                GameObject TempGo = new GameObject("Line");
                TempGo.transform.SetParent(this.transform);

            }
            if(this.gameObject.transform.childCount> ListObjects.Count)
            {
                Destroy(transform.GetChild(ListObjects.Count + 1).gameObject);
            }
        }
        
        for(int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<LineRenderer>() == null)
            {
                LineRenderer LR = transform.GetChild(i).gameObject.AddComponent<LineRenderer>();
                LR.material = LineProjectMaterial;
                LR.startWidth = .04f;
                LR.endWidth = .04f;
                LR.widthMultiplier = 1f;
                LR.positionCount = ListObjects[i].PointList.Count;
                LR.SetPositions(ListObjects[i].PointList.ToArray());
            }
            else
            {
                LineRenderer ExistLR = transform.GetChild(i).GetComponent<LineRenderer>();
                ExistLR.positionCount = ListObjects[i].PointList.Count;
                ExistLR.SetPositions(ListObjects[i].PointList.ToArray());
            }
        }

        //foreach (ListClass ListObj in ListObjects)
        //{
        //    if(this.gameObject.transform.childCount < ListObjects.Count)
        //    {
        //        GameObject TempGo = new GameObject("Line");
        //        TempGo.transform.SetParent(this.transform);
        //        LineRenderer LR = TempGo.AddComponent<LineRenderer>();
        //        LR.material = LineProjectMaterial;
        //        LR.startWidth = .04f;
        //        LR.endWidth = .04f;
        //        LR.widthMultiplier = 1f;
        //        LR.positionCount = ListObj.PointList.Count;
        //        LR.SetPositions(ListObj.PointList.ToArray());
        //    }
        //}

    }
}
[System.Serializable]
public class ListClass
{
    [SerializeField]
    public List<Vector3> PointList;

    public ListClass(List<Vector3> Points)
    {
        this.PointList = Points;
    }
}

