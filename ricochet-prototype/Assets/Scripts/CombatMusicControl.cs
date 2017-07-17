using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CombatMusicControl : MonoBehaviour {

    public bool fighting;
    public AudioMixerSnapshot outOfCombat;
    public AudioMixerSnapshot inCombat;

    // time in ms to fade the music to transition tracks
    private float m_TransitionIn;
    private float m_TransitionOut;

	// Use this for initialization
	void Start () {
        m_TransitionIn = 60.0f / 128.0f;
        m_TransitionOut = (60.0f / 128.0f) * 32.0f;
	}
	
    public void TransitionAudio(bool inFight)
    {
        if (inFight)
        {
            inCombat.TransitionTo(m_TransitionIn);
        }
        else if (!inFight)
        {
            outOfCombat.TransitionTo(m_TransitionOut);
        }
        else { }
    }
}
