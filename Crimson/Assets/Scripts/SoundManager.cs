using UnityEngine;
using System.Collections;

public class SoundManager : MonoBehaviour {
	
	public AudioSource bgm;
	public AudioSource se;
	public AudioClip[] music;
	public AudioClip[] sounds;
	
	void Awake ()
	{
		DontDestroyOnLoad (this);
	}
	
	// Use this for initialization
	void Start ()
	{
		bgm = this.gameObject.AddComponent<AudioSource> ();
		se = this.gameObject.AddComponent<AudioSource> ();
		
		bgm.loop = true;
		se.loop = false;
		
		bgm.playOnAwake = true;
		se.playOnAwake = true;
		
		setVolume(0.3f);
	}
	
	// Sound Effects
	public void playSound (int index)
	{
		if (!se.isPlaying) {
			se.clip = sounds [index];
			se.Play ();
		}
	}
	
	public void playSound (string name)
	{
		if (!se.isPlaying) {
			for (int i = 0; i < 10; i++) {
				if (sounds [i].name == name) {
					se.clip = sounds [i];	
				}
			}
			se.Play ();
		}
	}
	
	public void stopSound(){
		se.Stop();
		se.clip = null;
	}
	
	public void setSEVolume (float val)
	{
		if (val > 1)
			se.volume = 1.0f;
		else
			se.volume = val;
	}
	
	public void addSEVolume(float val){
		se.volume += val;
		if (se.volume < 0.0f)
			se.volume = 0.0f;
		else if (se.volume > 1.0f)
			se.volume = 1.0f;
	}
	
	public float getSEVolume(){
		return se.volume;	
	}
	
	// Background Music
	public void playMusic (int index)
	{
		if (!bgm.isPlaying) {
			bgm.clip = music [index];
			bgm.Play ();
		}
	}
	
	public void playMusic (string name)
	{
		if (!bgm.isPlaying) {
			for (int i = 0; i < 10; i++) {
				if (music [i].name == name) {
					bgm.clip = music [i];	
				}
			}
			bgm.Play ();
		}
	}
	
	public void stopMusic(){
		bgm.Stop();
		bgm.clip = null;
	}
	
	public void setBGMVolume (float val)
	{
		if (val > 1)
			bgm.volume = 1.0f;
		else
			bgm.volume = val;
	}
	
	public void addBGMVolume(float val){
		bgm.volume += val;
		if (bgm.volume < 0.0f)
			bgm.volume = 0.0f;
		else if (bgm.volume > 1.0f)
			bgm.volume = 1.0f;
	}
	
	public float getBGMVolume(){
		return bgm.volume;	
	}
	
	public void setVolume (float val)
	{
		if (val > 1) {
			bgm.volume = 1.0f;	
			se.volume = 1.0f;
		} else {
			bgm.volume = val;
			se.volume = val;
		}
	}
}
