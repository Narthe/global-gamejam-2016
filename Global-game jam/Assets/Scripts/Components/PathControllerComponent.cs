﻿using System;
using System.Collections.Generic;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class PathControllerComponent : MonoBehaviour
    {
        public GameObject CheckPoint;
        public List<GameObject> CheckPointList = new List<GameObject>();
        public GameObject CheckPointsContainer;
        public float m_speed = 1.0f;
        public iTweenPath ITweenPath;
        private int _checkpointIndex;

        private Action _gotoNextFallBack;

        public GameObject Target;

        private bool _moving = false;

        void Start ()
        {
            _checkpointIndex = 0;
            RefreshPath();
            Target.transform.position = ITweenPath.nodes[_checkpointIndex];
            
        }
	
        public void GotoNextWaypoint(Action onDone)
        {
            if (!_moving && _checkpointIndex < ITweenPath.nodes.Count - 1)
            {
                _checkpointIndex++;
                _moving = true;
                iTween.MoveTo(Target, iTween.Hash("position", ITweenPath.nodes[_checkpointIndex], "speed", m_speed,
                                                      "easetype", iTween.EaseType.linear, "oncompletetarget", gameObject,
                                                      "oncomplete", "Done"));

                _gotoNextFallBack = onDone;
            }
        }

        void Update ()
        {
            //Debug.Log(GetCurrentCheckPoint().transform.position);
        }

        public void Done()
        {
            if(_gotoNextFallBack != null)
                _gotoNextFallBack.Invoke();

            _moving = false;
        }

        public void RefreshPath()
        {
            ITweenPath.nodes = new List<Vector3>();
            foreach (Transform t in CheckPointsContainer.transform)
            {
                ITweenPath.nodes.Add(t.position);
                CheckPointList.Add(t.gameObject);
            }
            ITweenPath.nodeCount = ITweenPath.nodes.Count;
            ITweenPath.pathVisible = false;
            ITweenPath.pathVisible = true;
        }

        public CheckPointControllerComponent GetCurrentCheckPoint()
        {
            return CheckPointList[_checkpointIndex].GetComponent<CheckPointControllerComponent>();
        }
    }
}