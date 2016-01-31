﻿using Assets.Scripts.Components.UI;
using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.Events;

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
        public GameObject PathController;
        private PathControllerComponent _pathControllerComponent;
        public long TickIndex;

        private RitualInstance _miniGame;

        public static GameControllerComponent Instance;

        // Use this for initialization
        void Start()
        {
            _pathControllerComponent = PathController.GetComponent<PathControllerComponent>();
            Instance = this;
            UpdateSequence();
        }

        public void GotoNextCheckpoint()
        {
            MacroRegognizerComponent.InputSequence = null;
            StoryLineComponent.SetCurrentText(_pathControllerComponent.GetCurrentCheckPoint().SucessMessage);
            _pathControllerComponent.GotoNextWaypoint(UpdateSequence);

        }

        public void GotoPreviousCheckpoint()
        {
            MacroRegognizerComponent.InputSequence = null;
            _pathControllerComponent.GotoPreviousWaypoint(UpdateSequence);
            StoryLineComponent.SetCurrentText(_pathControllerComponent.GetCurrentCheckPoint().StartMessage);

        }

        // Update is called once per framet
        void Update()
        {
            UpdateMetronome();
            CheckMiniGameState();
        }

        private void CheckMiniGameState()
        {
            if (_miniGame != null)
            {
                if (_miniGame.RitualTerminated)
                {
                    if (!_miniGame.RitualSuccess)
                    {
                        Destroy(_miniGame.gameObject);
						GameObject temp = (GameObject)Instantiate(Resources.Load("RitualInstance"), Vector3.zero, Quaternion.identity);
                        _miniGame = temp.GetComponent<RitualInstance>();
                    }
                        
                }
            }
        }

        void UpdateSequence()
        {
            CheckPointControllerComponent ch = _pathControllerComponent.GetCurrentCheckPoint();
            MacroRegognizerComponent.InputSequence = ch.InputsSequences.ToArray();
            StoryLineComponent.SetCurrentText(ch.StartMessage);
            BPMRate = ch.BPM;
            MacroRegognizerComponent.AcceptanceArea = ch.AcceptanceArea;

            switch (ch.OnFailure)
            {
                case OnFailure.Nothing:
                    MacroRegognizerComponent.OnMacroFailed = null;
                    break;
                case OnFailure.Backtrack:
                    MacroRegognizerComponent.OnMacroFailed = new UnityEvent();
                    MacroRegognizerComponent.OnMacroFailed.AddListener(GotoPreviousCheckpoint);
                    break;
            }
            MacroRegognizerComponent.OnMacroOk.AddListener(GotoNextCheckpoint);
            switch (ch.OnSucess)
            {
                case OnSuccess.GotoNextNode:
                    MacroRegognizerComponent.OnMacroOk = new UnityEvent();
                    MacroRegognizerComponent.OnMacroOk.AddListener(GotoNextCheckpoint);
                    break;
                case OnSuccess.MusicMiniGame:
					GameObject.Find("UI").SetActive(false);
					GameObject temp = (GameObject)Instantiate(Resources.Load("RitualInstance"), Vector3.zero, Quaternion.identity);
					_miniGame = temp.GetComponent<RitualInstance>();
                    break;
            }

            if (ch.MusicToPlay != null)
            {
                //SoundController.Instance.AddSoundTrack(ch.MusicToPlay);
            }
        }

        private void UpdateMetronome()
        {
            _rateInSec = (BPMRate / 60);
            Curr = Time.time % _rateInSec / _rateInSec;
            TickIndex = (long)(Time.time / _rateInSec);
        }
    }
}
