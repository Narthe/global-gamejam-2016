using Assets.Scripts.Components.UI;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class GameControllerComponent : MonoBehaviour
    {
    #region public components reference
        public StoryLineComponent StoryLineComponent;
        public MacroRecognizerComponent MacroRegognizerComponent;
    #endregion

        private float _rateInSec;
        public int BPMRate = 110;
        public float Curr;

        public static GameControllerComponent Instance;

        // Use this for initialization
        void Start ()
        {
            Instance = this;
            MacroRegognizerComponent.inputCodes = new[] {"Submit","Cancel"};
            MacroRegognizerComponent.OnMacroOk.AddListener(() => StoryLineComponent.SetCurrentText("MACRO OK"));
        }
	
        // Update is called once per frame
        void Update ()
        {
            UpdateMetronome();

        }

        private void UpdateMetronome()
        {
            _rateInSec = (BPMRate / 60);
            Curr = Time.time % _rateInSec / _rateInSec;
        }
    }
}
