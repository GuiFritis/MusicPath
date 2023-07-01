using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumeChangeHelper : MonoBehaviour
{
    public AudioMixer group;
    public string floatParam = "MyExposedParam";

    private float _currentVolume = 0f;
    private Coroutine _changeVolumeCoroutine;

    public void ChangeMixerVolume(float value, float transitionDuration = .2f, float duration = 0)
    {
        if(_changeVolumeCoroutine != null)
        {
            StopCoroutine(_changeVolumeCoroutine);
            group.SetFloat(floatParam, _currentVolume);
        }
        _changeVolumeCoroutine = StartCoroutine(ChangeMixerVolumeCoroutine(value, transitionDuration, duration));
    }

    private IEnumerator ChangeMixerVolumeCoroutine(float value, float transitionDuration, float duration)
    {
        float timer = 0f;
        _currentVolume = value;
        group.GetFloat(floatParam, out _currentVolume);
        value = _currentVolume + value;
        while(timer < transitionDuration)
        {
            group.SetFloat(floatParam, Mathf.Lerp(_currentVolume, value, timer/transitionDuration));
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
        if(transitionDuration <= duration/2)
        {
            yield return new WaitForSeconds(duration - transitionDuration * 2);
        }
        timer = 0;
        while(timer < transitionDuration)
        {
            group.SetFloat(floatParam, Mathf.Lerp(value, _currentVolume, timer/transitionDuration));
            yield return new WaitForEndOfFrame();
            timer += Time.deltaTime;
        }
    }
}
