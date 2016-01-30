using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Components.UI
{
    [RequireComponent(typeof(Text))]
    public class StoryLineComponent : MonoBehaviour
    {
        private Text _text;
        // Use this for initialization
        void Start ()
        {
            _text = GetComponent<Text>();
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        public void SetCurrentText(string text)
        {
            _text.text = text;
        }
    }
}
