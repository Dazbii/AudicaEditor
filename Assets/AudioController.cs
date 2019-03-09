using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {

	public AudioSource audioSource;
	public Slider progressBar;
	public Button playButton;
	public Text tickReadout;
	public InputField bpmField;
	public InputField jumpField;
	public InputField speed;

	private bool playing = false;
	private int currentTick = 0;
	private int bpm = 120;

	// Use this for initialization
	IEnumerator Start () {
		Debug.Log("File:///" + Application.dataPath + "/song.ogg");
		using (WWW www = new WWW("File:///" + Application.dataPath + "/song.ogg")) {
			yield return www;
            audioSource.clip = www.GetAudioClip();
		}
		playButton.onClick.AddListener(ToggleSong);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnGUI() {
		if (playing) {
			progressBar.value = audioSource.time / audioSource.clip.length;
            SetTicks();
		}
	}

	private void ToggleSong() {
		if (playing) {
			audioSource.Pause();
		} else {
			audioSource.Play();
		}
		playing = !playing;
	}

	private void SetTicks() {
		currentTick = TickFromTime();
		tickReadout.text = "Current Tick: " + currentTick;
	}

	private int TickFromTime() {
		return (int) Mathf.Round(audioSource.time * 1000f * (60000f / (480f * bpm)));
	}

	private float TimeFromTick(int tick) {
		return tick * ((480f * bpm) / (60000f * 1000f));
	}

	public void SliderDrag() {
		audioSource.time = progressBar.value * audioSource.clip.length;
		SetTicks();
	}

	public void SetSpeed() {
		audioSource.pitch = float.Parse(speed.text);
	}

	public void Jump() {
		audioSource.time = TimeFromTick(int.Parse(jumpField.text));
		progressBar.value = audioSource.time / audioSource.clip.length;
		SetTicks();
	}

	public int GetCurrentTick() {
		return currentTick;
	}
}
