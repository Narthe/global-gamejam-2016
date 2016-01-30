using UnityEngine;
using System.Collections;

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
        if (!moving && i < waypoints.Length - 1 && Input.GetKeyDown(KeyCode.A))
        {
            i++;
            moving = true;
            iTween.MoveTo(gameObject, iTween.Hash("position", waypoints[i], "speed", m_speed,
                                                  "easetype", iTween.EaseType.linear, "oncompletetarget", gameObject, 
                                                  "oncomplete", "Done"));
            _pathControllerComponent.GotoNextWaypoint(_pathControllerComponent.CurrentCheckPoint);
        }
 
    }

    public void Done()
    {
        moving = false;
    }
}

