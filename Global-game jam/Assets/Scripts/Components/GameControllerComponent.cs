using UnityEngine;

namespace Assets.Scripts.Components
{
    public class GameControllerComponent : MonoBehaviour
    {
        private float _rateInSec;
        public int BPMRate = 110;
        public float Curr;

        public static GameControllerComponent Instance;

        // Use this for initialization
        void Start ()
        {
            Instance = this;
        }
	
        // Update is called once per frame
        void Update () {
            _rateInSec = (BPMRate / 60);
            Curr = Time.time % _rateInSec / _rateInSec;
        }
    }
}
