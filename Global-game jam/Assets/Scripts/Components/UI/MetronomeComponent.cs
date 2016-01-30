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

        public string[] inputCodes;

        int index = 0;
        private float _timeSinceLastInput;
        private float _lastInputTime;
        private bool _wasNeutral;
        private bool _inputOk;

        #endregion

#region UI exposed

        public float AcceptanceArea = .2f;
        public Image AcceptanceAreaImage;
        public GameObject CurrentInputContainer;
        public GameObject InputPrefab;

#endregion

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
            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0f && _wasNeutral)
            {
                _wasNeutral = false;
                _inputOk = Input.GetButton(this.inputCodes[index]);

                if (!_inputOk || (index > 0 && Time.time - _lastInputTime > GameControllerComponent.Instance.BPMRate / 60f))
                {
                    ClearCurrentInput();
                }

                if (_inputOk && GameControllerComponent.Instance.Curr > .5f - AcceptanceArea/2f && GameControllerComponent.Instance.Curr < .5f + AcceptanceArea)
                {
                    AddCurrentInput();
                    Debug.LogError(index);

                    if (this.index >= this.inputCodes.Length)
                    {
                        if (OnMacroOk != null)
                            OnMacroOk.Invoke();

                        ClearCurrentInput();

                    }

                }
                else
                {
                    
                    ClearCurrentInput();
                }
                _lastInputTime = Time.time;
            }

            if (Mathf.Abs(Input.GetAxis("Vertical")) <= 0f && !_wasNeutral)
                _wasNeutral = true;
        }

        private void ClearCurrentInput()
        {
            this.index = 0;
        }

        private void AddCurrentInput()
        {
            this.index++;

        }
    }
}
