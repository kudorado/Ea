using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EaAudio : EaMonoSingleton<EaAudio>  {
	private AudioSource _audioSrc;
	public  AudioSource audioSrc{ get {return _audioSrc ?? (_audioSrc = gameObject.AddComponent<AudioSource> ());}}
	public  List<AudioClip> audioList = new List<AudioClip>();
	public bool mute { get; set; }

	public  void Load(params string [] audios){
		AudioConfig ();
		int length = audios.Length;
		for (int i = 0; i < length; i++) {
			var audio =	Resources.Load <AudioClip>(audios[i]);
			audioList.Add (audio);
		}
	}
	void AudioConfig(){
		audioSrc.playOnAwake = false;
		audioSrc.loop = false;
	}

	public  void Play(string audioName){
		if (mute || audioName == string.Empty)
			return;
		
		int count = audioList.Count;
		for (int i = 0; i < count; i++) {
			if (audioList [i].name == audioName) {
				audioSrc.PlayOneShot(audioList[i]);
				return;
			}
		}	
//		Debug.LogError ("No clip found, please checking your Resources folder.");
	}

}
