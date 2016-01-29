using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace Assets.Scripts.Components
{

    public class ClockComponent : MonoBehaviour
    {

        public Text Text;
        // Use this for initialization
        void Start()
        {
            if (Text == null)
                Text = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            Text.text = DateTime.Now.ToString("HH:mm:ss tt");
        }
    }
}
