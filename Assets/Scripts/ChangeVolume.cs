using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeVolume : MonoBehaviour
{
    AudioSource m_MyAudioSource;

    void Start()
    {
        m_MyAudioSource = GetComponent<AudioSource>();
        m_MyAudioSource.volume = 0.5f;
    }

    public void SliderChanged(float newValue)
    {
        m_MyAudioSource.volume = newValue;
    }
}
