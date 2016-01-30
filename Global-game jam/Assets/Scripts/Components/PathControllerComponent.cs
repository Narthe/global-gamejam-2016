using UnityEngine;
using System.Collections;
using Assets.Scripts.Helpers;
using System.Collections.Generic;

public class PathControllerComponent : MonoBehaviour
{
    public GameObject CheckPoint;
    public List<Transform> CheckPointList = new List<Transform>();
    public Transform CurrentCheckPoint;
         
	void Start ()
    {
        //Generate checkpoint game object foreach waypoint in ItweenPath script
	    for(int i = 0; i < transform.GetComponent<iTweenPath>().nodeCount; i++)
        {
            GameObject currentGameObject = GuiHelper.Instanciate(CheckPoint, gameObject);
            currentGameObject.name = "CheckPoint";
            currentGameObject.transform.position = new Vector3(transform.GetComponent<iTweenPath>().nodes[i].x, 
                                                               transform.GetComponent<iTweenPath>().nodes[i].y, 
                                                               transform.GetComponent<iTweenPath>().nodes[i].z);
            CheckPointList.Add(currentGameObject.transform);
        }
        CurrentCheckPoint = CheckPointList[0];
    }
	
    public void GotoNextWaypoint(Transform CurrentPosition)
    {
        CurrentCheckPoint = CheckPointList[CheckPointList.IndexOf(CurrentPosition) + 1];
    }

	void Update ()
    {
        Debug.Log(CurrentCheckPoint.position);
    }
}
