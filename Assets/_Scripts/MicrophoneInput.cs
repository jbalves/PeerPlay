using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MicrophoneInput : MonoBehaviour {

	public float sensitivity = 100;
	public float loudness = 0;

	AudioSource _audioSource;
	//GameObject _player;

	//Microphone input
	public AudioClip _audioClip;
	//public bool _useMicrophone;
	public string _selectedDevice;

	// Use this for initialization
	void Start () {

		_audioSource = GetComponent<AudioSource> ();
		_audioSource.clip = Microphone.Start (Microphone.devices[0], true, 1, AudioSettings.outputSampleRate);
		_audioSource.loop = true;

		while (!(Microphone.GetPosition (null) > 0)) {}
		_audioSource.Play ();

	}
	
	// Update is called once per frame
	void Update () {
		loudness = GetAveragedVolume() * sensitivity;
	}

	float GetAveragedVolume() { 
		float[] data = new float[256];
		float a = 0;
		_audioSource.GetOutputData (data, 0);
		foreach (float s in data)
		{
			a += Mathf.Abs(s);
		}
		return a / 256;
	}
		
}
