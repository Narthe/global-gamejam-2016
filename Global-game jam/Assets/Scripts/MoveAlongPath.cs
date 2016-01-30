using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;

public class MoveAlongPath : MonoBehaviour
{
    public Vector3[] waypoints;
    public float m_speed = 1.0f;
    public GameObject PathController;

    private iTweenPath _iTweenPath;
    private PathControllerComponent _pathControllerComponent;
    private int i = 0;

    private bool moving = false;

    public void Start()
    {
        _iTweenPath = PathController.GetComponent<iTweenPath>();
        _pathControllerComponent = PathController.GetComponent<PathControllerComponent>();

        waypoints = _iTweenPath.nodes.ToArray();
        transform.position = waypoints[0];
    }

    public void Update()
    {
       
 
    }


    public void Done()
    {
        moving = false;
    }
}

