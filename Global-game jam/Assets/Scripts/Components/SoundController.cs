﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Helpers;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class SoundController : MonoBehaviour
    {
        public static SoundController Instance;
        private List<AudioSource> _walkClips;
        private List<AudioSource> _battleClips;
        private List<AudioSource> _playClips;

        private List<AudioSource> _currentPlayingList;
        private List<AudioSource> _nextPlaying;
        // Use this for initialization
        void Start ()
        {
            Instance = this;
            _walkClips = new List<AudioSource>();
            _battleClips = new List<AudioSource>();
            _playClips = new List<AudioSource>();
        }
	
        // Update is called once per frame
        void Update () {
	
        }

        public void AddAudioClip(AudioClip a, CharacterAction type)
        {
            AudioSource source = GuiHelper.Instanciate(gameObject).AddComponent<AudioSource>();
            source.clip = a;
            source.playOnAwake = false;
            source.loop = true;
            source.Pause();

            switch (type)
            {
                case CharacterAction.Hit:
                    _nextPlaying = _battleClips;
                    break;
                    case CharacterAction.Walk:
                    case CharacterAction.Jump:
                    _nextPlaying = _walkClips;
                    break;
                    case CharacterAction.Play:
                    _nextPlaying = _playClips;
                    break;
            }

            if (_nextPlaying != _currentPlayingList && _currentPlayingList != null)
            {
                if(_currentPlayingList.Any())
                    foreach (var src in _currentPlayingList)
                    {
                        if(src != null)
                            src.volume = 0f;
                    }
            }

            _currentPlayingList = _nextPlaying;

            if (_currentPlayingList.Any())
                foreach (var src in _currentPlayingList)
                {
                    if(src != null)
                        src.volume = 1f;
                }

            if (_currentPlayingList.Any())
            {
                source.time = _currentPlayingList.FirstOrDefault().time;
            }

            source.Play();

            _currentPlayingList.Add(source);
        }

        public void ClearStack()
        {
            gameObject.ClearChilds();
        }

    }
}