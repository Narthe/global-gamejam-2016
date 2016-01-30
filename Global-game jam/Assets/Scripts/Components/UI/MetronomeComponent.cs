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
            Target.fillAmount = GameControllerComponent.Instance.Curr;
        }
    }
}
