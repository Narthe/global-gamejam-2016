using System.Collections;
using System.Linq;
using Assets.Scripts.Helpers;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets.Scripts.Components.UI
{
    public class MetronomeComponent : MonoBehaviour
    {
        public Image Target;
        private RectTransform _rect;

        #region Macro
        public float timeKey = 0f, timeCode = 0f;

        public UnityEvent OnMacroOk;

        public string[] InputSequence;

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

        #endregion

        private static string[] _possibleInputs = new[] {"Walk", "Hit", "Jump", "Play"};

        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update ()
        {
            CheckMacro();
            UpdateMetro();        
        }

        private void UpdateMetro()
        {
            AcceptanceAreaImage.rectTransform.offsetMin = new Vector2((1920f/2f) - ((AcceptanceArea*1920f)/2f),0);
            AcceptanceAreaImage.rectTransform.offsetMax = new Vector2((1920f / 2f) + ((AcceptanceArea * 1920f) / 2f), 200);
            Target.fillAmount = GameControllerComponent.Instance.Curr;
        }

        void CheckMacro()
        {
            _curTick = GameControllerComponent.Instance.TickIndex;
            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0f && _wasNeutral)
            {
                _wasNeutral = false;
                _inputOk = CheckInputIsRight();

                if (!_inputOk || (index > 0 && _curTick > _lastInputTick+1 || _curTick == _lastInputTick))
                {
                    ClearCurrentInput();
                }

                if (_inputOk && GameControllerComponent.Instance.Curr > .5f - AcceptanceArea/2f && GameControllerComponent.Instance.Curr < .5f + AcceptanceArea)
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
                    ClearCurrentInput();
                }
                _lastInputTick = GameControllerComponent.Instance.TickIndex;
            }
            else if( index > 0 && _curTick > _lastInputTick + 1)
            {
                ClearCurrentInput();
            }
            else if(index == 0 && CurrentInputContainer.transform.childCount > 0 && _curTick >_lastInputTick)
                ClearCurrentInput();

            if (Mathf.Abs(Input.GetAxis("Vertical")) <= 0f && !_wasNeutral)
                _wasNeutral = true;
        }

        private bool CheckInputIsRight()
        {
            if (!Input.GetButton(InputSequence[index]))
                return false;

            foreach(string s in _possibleInputs.Where(s => s != InputSequence[index]))
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

        private void ClearCurrentInput()
        {
            CurrentInputContainer.ClearChilds();
            this.index = 0;
        }

        private void AddCurrentInput()
        {
            InputComponent i = GuiHelper.Instanciate(InputPrefab, CurrentInputContainer).GetComponent<InputComponent>();
            switch (InputSequence[index])
            {
                case "Walk":
                    i.Image.color = Color.red;
                    break;
                case "Hit":
                    i.Image.color = Color.magenta;
                    break;
                case "Play":
                    i.Image.color = Color.green;
                    break;
                case "Jump":
                    i.Image.color = Color.blue;
                    break;

            }
            this.index++;

        }
    }
}
