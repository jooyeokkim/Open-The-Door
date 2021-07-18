using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Goals : MonoBehaviour {
	int count;
	float gamerecord;
	public Text[] dia;
	public Button diareward;
	public Text diarewardtext;
	public Text myrecord;
	int diascore;
	string minstring;
	string secstring;
	string millisecstring;
	public timer time;
	AudioSource clear;
	public GameObject clearpanel;
	bool isgameover = false;
	string stageLevel;

	void Start() {
		clear = GetComponent<AudioSource> ();
		count = 0;
		clearpanel.SetActive (false);
		stageLevel = SceneManager.GetActiveScene ().name;
		myrecord.text = PlayerPrefs.GetString (stageLevel, "--:--:--");
	}

	public void UpCount() {
		if(!isgameover) count++;
		if (!isgameover && count == 3) {
			clear.Play ();
			time.gameover ();
			gamerecord = time.getrecord();
			float bestrecord = PlayerPrefs.GetFloat (stageLevel + "Bestrecord", 1.0E+20f);
			int bestdia = PlayerPrefs.GetInt (stageLevel + "Bestdia", 0);
			for (int i = 0; i < 4; i++) {
				minstring = dia [i].text.ToString ().Substring (0, 2);
				secstring = dia [i].text.ToString ().Substring (3, 2);
				millisecstring = dia [i].text.ToString ().Substring (6, 2);
				int min = (minstring [0] - '0') * 10 + (minstring [1] - '0');
				int sec = (secstring [0] - '0') * 10 + (secstring [1] - '0');
				float millisec = (millisecstring [0] - '0') * 10 + (millisecstring [1] - '0');
				if(gamerecord<=(min*60+sec+millisec/100)){
					diascore = 4-i;
					if (4 - i - bestdia <= 0)
						diarewardtext.text = "+00";
					else diarewardtext.text = "+0" + (4 - i - bestdia);
					dia[i].color = new Color (0.9f, 0.1f, 0.1f);
					break;
				}
				if (i == 3 && gamerecord > (min * 60 + sec + millisec/100)) {
					diarewardtext.text = "+00";
				}
			}
			isgameover = true;
			Debug.Log (gamerecord);
			Debug.Log ((float)Math.Round (gamerecord, 4));
			if (myrecord.text == "--:--:--" || gamerecord < bestrecord)
				saveData ();
			diareward.gameObject.SetActive (true);
			clearpanel.SetActive (true);
		}
	}
	public void DownCount() {
		if (!isgameover) count--;
	}

	void saveData() {		
		PlayerPrefs.SetFloat (stageLevel + "Bestrecord", (float)Math.Round(gamerecord,4));
		PlayerPrefs.SetInt (stageLevel + "Bestdia", diascore);
		PlayerPrefs.SetString (stageLevel, time.getTimertext());
		int currentLevel = PlayerPrefs.GetInt ("CurrentLevel", 1);
		if (currentLevel == Int32.Parse (stageLevel.Substring (5)) && currentLevel<40)
			PlayerPrefs.SetInt ("CurrentLevel", currentLevel + 1);
	}
}
