using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Sounds
{
    public class AudioPool : PoolBase<AudioSource, AudioPool>
    {
        public void Play(AudioClip clip)
        {
            if(clip != null)
            {
                var item = GetPoolItem();
                item.clip = clip;
                item.Play();
            }
        }

        protected override bool CheckItem(AudioSource item)
        {
            return !item.isPlaying;
        }
    }
}