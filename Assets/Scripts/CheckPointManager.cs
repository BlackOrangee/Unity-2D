using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    private HashSet<Transform> checkPoints = new HashSet<Transform>();

    public void SetCheckPoint(Transform checkPointTransform)
    {
        checkPoints.Add(checkPointTransform);
    }

    public void ResetCheckPoints()
    {
        foreach (Transform oneLittlePoint in checkPoints)
        {
            oneLittlePoint.gameObject.GetComponent<CheckPointActivationTrigger>().ResetTrigger();
        }

        checkPoints.Clear();
    }

    public void LoadLastPoint()
    {
        transform.position = checkPoints.Last().position;
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
