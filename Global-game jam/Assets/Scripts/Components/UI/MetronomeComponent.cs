using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Components.UI
{
    public class MetronomeComponent : MonoBehaviour
    {
        public Image Target;
        private RectTransform _rect;

        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update ()
        {
            _rect = Target.rectTransform;
            _rect.offsetMin = new Vector2(Mathf.Lerp(0, 1920-4,GameControllerComponent.Instance.Curr),0);
            _rect.offsetMax = new Vector2(Mathf.Lerp(4, 1920, GameControllerComponent.Instance.Curr), 200);
        }
    }
}
