﻿using System.Collections;
using System.Linq;
using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Components.UI
{
    public class MetronomeComponent : MonoBehaviour
    {
        public Image Background;
        public RectTransform Container;
        public RectTransform Metronome;
        private RectTransform _rect;

        #region Macro
        public float timeKey = 0f, timeCode = 0f;

        public UnityEvent OnMacroOk;
        public UnityEvent OnMacroFailed;

        public CharacterAction[] InputSequence;

        int index = 0;
        private float _timeSinceLastInput;
        private long _lastInputTick;
        private bool _wasNeutral;
        private bool _inputOk;

        #endregion

        #region UI exposed

        public float AcceptanceArea = .2f;
        public Image AcceptanceAreaImage;
        public GameObject CurrentInputContainer;
        public GameObject InputPrefab;
        private long _curTick;

        public Sprite XboxButtonsA;

        public Sprite XboxButtonsY;

        public Sprite XboxButtonsX;

        public Sprite XboxButtonsB;

        #endregion

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            CheckMacro();
            UpdateMetro();
        }

        private void UpdateMetro()
        {
            AcceptanceAreaImage.rectTransform.offsetMin = new Vector2((Container.rect.width / 2f) - ((AcceptanceArea * 1920f) / 2f), 50);
            AcceptanceAreaImage.rectTransform.offsetMax = new Vector2((Container.rect.width / 2f) + ((AcceptanceArea * 1920f) / 2f), 150);
            Metronome.localPosition = new Vector3(Mathf.Lerp(-990-559, 990+559, GameControllerComponent.Instance.Curr), Metronome.localPosition.y);
        }

        void CheckMacro()
        {
            _curTick = GameControllerComponent.Instance.TickIndex;

            if (InputSequence == null || !InputSequence.Any())
                Background.color = Color.white;
            else 
                Background.color = GetColorFromAction(InputSequence[index]);

            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0f && _wasNeutral)
            {
                _wasNeutral = false;
                _inputOk = CheckInputIsRight();

                if (!_inputOk || (index > 0 && _curTick > _lastInputTick + 1 || _curTick == _lastInputTick))
                {
                    ClearCurrentInput();
                    if(OnMacroFailed != null)
                        OnMacroFailed.Invoke();
                }

                if (_inputOk && GameControllerComponent.Instance.Curr > .5f - AcceptanceArea / 2f && GameControllerComponent.Instance.Curr < .5f + AcceptanceArea)
                {
                    AddCurrentInput();
                    Debug.LogError(index);

                    if (this.index >= this.InputSequence.Length)
                    {
                        if (OnMacroOk != null)
                            OnMacroOk.Invoke();

                        ShowSuccessEffect();

                    }

                }
                else
                {
                    ClearCurrentInput(false);
                }
                _lastInputTick = GameControllerComponent.Instance.TickIndex;
            }
            else if (index > 0 && _curTick > _lastInputTick + 1)
            {
                ClearCurrentInput();
                if (OnMacroFailed != null)
                    OnMacroFailed.Invoke();
            }
            else if (index == 0 && CurrentInputContainer.transform.childCount > 0 && _curTick > _lastInputTick)
                ClearCurrentInput();

            if (Mathf.Abs(Input.GetAxis("Vertical")) <= 0f && !_wasNeutral)
                _wasNeutral = true;
        }

        private bool CheckInputIsRight()
        {
            if (!Input.GetButton(InputSequence[index].ToString()))
                return false;

            foreach (string s in GameControllerComponent.PossibleInputs.Where(s => s != InputSequence[index].ToString()))
            {
                if (Input.GetButton(s))
                    return false;
            }
            return true;
        }

        private void ShowSuccessEffect()
        {
            this.index = 0;
        }

        private void ClearCurrentInput(bool refreshState = true)
        {
            CurrentInputContainer.ClearChilds();
            if(refreshState)
                PlayerControllerComponent.Instance.SetState(CharacterAction.Idle);
            this.index = 0;
        }

        private void AddCurrentInput()
        {
            InputComponent i = GuiHelper.Instanciate(InputPrefab, CurrentInputContainer).GetComponent<InputComponent>();
            i.Image.sprite = GetTextureFromAction(InputSequence[index]);
            PlayerControllerComponent.Instance.SetState(InputSequence[index]);
            this.index++;

        }

        private Color GetColorFromAction(CharacterAction action)
        {
            switch (action)
            {
                case CharacterAction.Walk:
                    return Color.red;
                case CharacterAction.Hit:
                    return Color.blue;
                case CharacterAction.Play:
                    return Color.yellow;
                case CharacterAction.Jump:
                    return Color.green;
            }
            return Color.white;
        }

        private Sprite GetTextureFromAction(CharacterAction action)
        {
            switch (action)
            {
                case CharacterAction.Walk:
                    return XboxButtonsB;
                case CharacterAction.Hit:
                    return XboxButtonsX;
                case CharacterAction.Play:
                    return XboxButtonsY;
                case CharacterAction.Jump:
                    return XboxButtonsA;
            }
            return null;
        }
    }
}
