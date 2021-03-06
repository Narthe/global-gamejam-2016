﻿using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Components.UI;
using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

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
        public AudioClip MinigameAudioClip;
        private float _ajusted;

        // Use this for initialization
        IEnumerator Start()
        {
            _pathControllerComponent = PathController.GetComponent<PathControllerComponent>();
            Instance = this;
            yield return null;
            yield return null;
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
            PlayerControllerComponent.Instance.SetState(CharacterAction.Walk);
            MacroRegognizerComponent.InputSequence = null;
            _pathControllerComponent.GotoPreviousWaypoint(UpdateSequence);
            StoryLineComponent.SetCurrentText(_pathControllerComponent.GetCurrentCheckPoint().StartMessage);

        }

        // Update is called once per framet
        void Update()
        {
            CheckMiniGameState();
        }

        void FixedUpdate()
        {
            UpdateMetronome();
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
                        GameObject temp =
                            (GameObject)
                                Instantiate(Resources.Load("RitualInstance"), Vector3.zero, Quaternion.identity);
                        _miniGame = temp.GetComponent<RitualInstance>();
                    }
                    else
                    {
                        SceneManager.LoadScene(2);
                    }
                        
                }
            }
        }

        void UpdateSequence()
        {
            CheckPointControllerComponent ch = _pathControllerComponent.GetCurrentCheckPoint();
            PlayerControllerComponent.Instance.SetState(CharacterAction.Idle);
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

                    MacroRegognizerComponent.OnMacroOk = new UnityEvent();
                    MacroRegognizerComponent.OnMacroOk.AddListener(delegate
                    {
                        SoundController.Instance.ClearStacks();
                        SoundController.Instance.AddAudioClip(MinigameAudioClip,CharacterAction.Play);
                        GameObject.Find("UI").SetActive(false);
                        GameObject temp = (GameObject)Instantiate(Resources.Load("RitualInstance"), Vector3.zero, Quaternion.identity);
                        _miniGame = temp.GetComponent<RitualInstance>();
                        PlayerControllerComponent.Instance.SetState(CharacterAction.Play);
                    });
                    
                    break;
            }

            if (ch.MusicToPlay != null)
            {
                SoundController.Instance.AddAudioClip(ch.MusicToPlay, ch.InputsSequences[0]);
            }
        }

        private void UpdateMetronome()
        {
            _rateInSec = (float) (60f/BPMRate);
            _ajusted = (Time.time%_rateInSec);
            Curr = (_ajusted / _rateInSec);
            TickIndex = (long)(Time.time / _rateInSec);
        }
    }
}
