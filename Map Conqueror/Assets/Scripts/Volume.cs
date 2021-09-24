using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Volume : MonoBehaviour
{
	public AudioMixer audioMixer;

	public void SetVolume(float volume) //Links the volume scroll bar to the audio mixer.
	{
		audioMixer.SetFloat("volume", volume);
	}
}
