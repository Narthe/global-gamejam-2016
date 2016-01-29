using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Components
{
    [RequireComponent(typeof(Collider2D))]
    public class ActionTriggerComponent : MonoBehaviour
    {

        public UnityEvent OnTrigger;
        private bool _isOverPlayer;

        // Use this for initialization
        void Start () {
	
        }
	
        // Update is called once per frame
        void Update () {

	        if(_isOverPlayer && Input.GetButton("Action"))
            {
                Debug.Log("ACTION!!!");
                if(OnTrigger != null)
                    OnTrigger.Invoke();
            }
        }

        void OnTriggerEnter2D(Collider2D other)
        {
            //checked that other is player
            if (other.gameObject.tag == "Player")
                _isOverPlayer = true;
        }
        void OnTriggerExit2D(Collider2D other)
        {
            //checked that other is player
            if (other.gameObject.tag == "Player")
                _isOverPlayer = false;
        }
    }
}
