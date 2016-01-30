using Assets.Scripts.Components.UI;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class GameControllerComponent : MonoBehaviour
    {
        public static string[] PossibleInputs = new[] { "Walk", "Hit", "Jump", "Play" };
    #region public components reference
        public StoryLineComponent StoryLineComponent;
        public MetronomeComponent MacroRegognizerComponent;
    #endregion

        private float _rateInSec;
        public int BPMRate = 110;
        public float Curr;
        public long TickIndex;
        public GameObject PathController;
        private PathControllerComponent _pathControllerComponent;

        public static GameControllerComponent Instance;

        // Use this for initialization
        void Start ()
        {
            _pathControllerComponent = PathController.GetComponent<PathControllerComponent>();

            Instance = this;
            MacroRegognizerComponent.InputSequence = _pathControllerComponent.GetCurrentCheckPoint().InputsSequences.ToArray();
            MacroRegognizerComponent.OnMacroOk.AddListener(() => _pathControllerComponent.GotoNextWaypoint());
        }
	
        // Update is called once per framet
        void Update ()
        {
            UpdateMetronome();

        }

        private void UpdateMetronome()
        {
            _rateInSec = (BPMRate / 60);
            Curr = Time.time % _rateInSec / _rateInSec;
            TickIndex = (long) (Time.time/_rateInSec);
        }
    }
}
