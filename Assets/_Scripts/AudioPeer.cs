using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof (AudioSource))]
public class AudioPeer : MonoBehaviour {

	AudioSource _audioSource;
	public static float[] _samples = new float[512];
	public static float[] _freqBand = new float[8];
	public static float[] _bandBuffer = new float[8];
	float[] _bufferDecrease = new float[8];
	public static float _axis = 0;



	// Use this for initialization
	void Start () {
		_audioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		GetSpectrumAudioSource ();
		MakeFrequencyBands ();
		BandBuffer ();
		
	}

	void GetSpectrumAudioSource(){
		_audioSource.GetSpectrumData (_samples, 0, FFTWindow.Blackman);
	}

	void MakeFrequencyBands(){
		/*
	 	* 22050 Hz [Hertz]  /  512 [bands]  = 43 hertz per sample
	 	* 
	 	* [1] 20   - 60    hertz
	 	* [2] 60   - 250   hertz
	 	* [3] 250  - 500   hertz
	 	* [4] 500  - 2000  hertz
	 	* [5] 2000 - 4000  hertz 
	 	* [6] 4000 - 6000  hertz
	 	* [7] 6000 - 20000 hertz
	 	* 
	 	*/

		int count = 0;
		_axis = 0;
		for (int i = 0; i < 8; i++) {
			
			float average = 0;
			int sampleCount = (int)Mathf.Pow (2, i) * 2;

			if(i == 7){
				sampleCount += 2;
			}

			for(int j = 0; j < sampleCount; j++){
				average += _samples [count] * (count + 1);
				count++;
			}

			average /= count;
			_freqBand [i] = average * 10;


			if (_freqBand [2] > 1) {
				Debug.Log ("HuHu");
				_axis = -1;
			}

			if ((_freqBand [3] > 1) && (_freqBand [4] > 1)) {
				Debug.Log ("HaHa");
				_axis = 1;
			}
			//Debug.Log ("_freqBand["+i+"]: " + _freqBand[i].ToString());
		}
	}

	void BandBuffer(){
		for (int g = 0; g < 8; g++) {
			if (_freqBand [g] > _bandBuffer [g]) {
				_bandBuffer [g] = _freqBand [g];
				_bufferDecrease [g] = 0.005f;
			}

			if (_freqBand [g] < _bandBuffer [g]) {
				_bandBuffer[g] -= _bufferDecrease [g];
				_bufferDecrease [g] *= 1.2f; 
			}
		}
	}
}
