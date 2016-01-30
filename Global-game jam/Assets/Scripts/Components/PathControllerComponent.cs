using UnityEngine;
using System.Collections;

public class PathControllerComponent : MonoBehaviour
{    
         
	void Start ()
    {
	    for(int i = 0; i < transform.GetComponent<iTweenPath>().nodeCount; i++)
        {
            GameObject wayPointGameobject = new GameObject();
            
        }
	}
	
	void Update ()
    {
	
	}
}
