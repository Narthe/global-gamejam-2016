using UnityEngine;
using System.Collections;
using Assets.Scripts.Components;

[RequireComponent(typeof(Animator))]
public class PlayerControllerComponent : MonoBehaviour
{
    public static PlayerControllerComponent Instance;
    private Animator _animator;

	void Start ()
	{
	    Instance = this;
	    _animator = GetComponent<Animator>();
	}
	
	void Update () {
	
	}

    public void SetState(CharacterAction action)
    {
        _animator.SetTrigger(action.ToString());
    }
}
