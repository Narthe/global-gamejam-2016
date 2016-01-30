using Assets.Scripts.Components;
using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts
{
    public class MacroRecognizerComponent : MonoBehaviour
    {

        public float timeKey = 0f, timeCode = 0f;

        public UnityEvent OnMacroOk;

        public string[] inputCodes;

        int index = 0;
        private float _timeSinceLastInput;
        private float _lastInputTime;
        private bool _wasNeutral;

        void Start()
        {

        }

        void Update()
        {

            if (Mathf.Abs(Input.GetAxis("Vertical")) > 0f && _wasNeutral)
            {
                _wasNeutral = false;
                if (Input.GetButton(this.inputCodes[index]) == false || (index > 0 && Time.time - _lastInputTime > GameControllerComponent.Instance.BPMRate / 60f))
                {
                    this.index = 0;
                }

                if (Input.GetButton(this.inputCodes[index]) && GameControllerComponent.Instance.Curr > .45f && GameControllerComponent.Instance.Curr < .55f)
                {
                    this.index++;
                    Debug.LogError(index);

                    if (this.index >= this.inputCodes.Length)
                    {
                        if (OnMacroOk != null)
                            OnMacroOk.Invoke();

                        this.index = 0;

                    }

                }
                else
                {
                    this.index = 0;
                }
                _lastInputTime = Time.time;
            }

            if (Mathf.Abs(Input.GetAxis("Vertical")) <= 0f && !_wasNeutral)
                _wasNeutral = true;

        }

    }
}
