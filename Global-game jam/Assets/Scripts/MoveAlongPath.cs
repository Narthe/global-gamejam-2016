using UnityEngine;
using System.Collections;

public class MoveAlongPath : MonoBehaviour
{
    public Vector3[] waypoints;
    public float m_speed = 1.0f;
    private int i = 0;
    public iTweenPath Path;

    private bool moving = false;

    public void Start()
    {
        waypoints = Path.nodes.ToArray();
        transform.position = waypoints[0];
    }

    public void Update()
    {
        if (!moving && i < waypoints.Length - 1 && Input.GetKeyDown(KeyCode.A))
        {
            i++;
            moving = true;
            iTween.MoveTo(gameObject, iTween.Hash("position", waypoints[i], "speed", m_speed, "easetype", iTween.EaseType.linear, "oncompletetarget", gameObject, "oncomplete", "Done"));
        }
 
    }

    public void Done()
    {
        moving = false;
    }
}

