using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class CheckPointControllerComponent : MonoBehaviour
    {    
        public List<CharacterAction> InputsSequences;
        public string StartMessage;
        public string SucessMessage;
        public int BPM = 110;
        public float AcceptanceArea = .3f;
        public OnFailure OnFailure;
        public OnSuccess OnSucess;

        void Start ()
        {
	
        }
	
        void Update ()
        {
	
        }
    }

    public enum OnFailure
    {
        Nothing,
        Backtrack
    }

    public enum OnSuccess
    {
        GotoNextNode,
        MusicMiniGame
    }

    public enum CharacterAction
    {
        Walk,
        Jump,
        Play,
        Hit,
        Idle
    }
}
