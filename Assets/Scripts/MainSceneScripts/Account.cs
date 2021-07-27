using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Net;
using System.Net.Sockets;

public class Account : MonoBehaviour {
	public InputField IDInput;
	public InputField PassInput;
	public InputField ConfirmPassInput;
	public Button createAccount;
	public Button saveData;
	public Button loadData;
	public Button messagePanel;
	public Text messagePanelText;
	string LoginUrl;
	string SaveUrl;
	string LoadUrl;
	int currentLevel;

	void Start(){
		LoginUrl = "http://"+IP.creatorIP+"/otd/create_account.php";
		SaveUrl = "http://"+IP.creatorIP+"/otd/save.php";
		LoadUrl = "http://"+IP.creatorIP+"/otd/load.php";
		currentLevel = PlayerPrefs.GetInt ("CurrentLevel", 1);
	}

	public void LoginBtn(){
		if (IDInput.text.Length == 0 || PassInput.text.Length == 0) {
			messagePanelText.text = "Enter ID and Password!";
			messagePanelText.fontSize = 13;
			messagePanel.gameObject.SetActive (true);
		}
		else if (PassInput.text != ConfirmPassInput.text) {
			messagePanelText.text = "Password doesn't match confirmation!";
			messagePanelText.fontSize = 12;
			messagePanel.gameObject.SetActive (true);
		} else {
			StartCoroutine (CreateGo ());
			createAccount.interactable = false;
		}
	}

	public void SaveBtn(){
		if (IDInput.text.Length == 0 || PassInput.text.Length == 0) {
			messagePanelText.text = "Enter ID and Password!";
			messagePanelText.fontSize = 13;
			messagePanel.gameObject.SetActive (true);
		}
		else if (currentLevel == 1) {
			messagePanelText.text = "Please clear level1 first!";
			messagePanelText.fontSize = 13;
			messagePanel.gameObject.SetActive (true);
		}
		else {
			StartCoroutine (SaveGo ());
			saveData.interactable = false;
		}
	}

	public void LoadBtn(){
		if (IDInput.text.Length == 0 || PassInput.text.Length == 0) {
			messagePanelText.text = "Enter ID and Password!";
			messagePanelText.fontSize = 13;
			messagePanel.gameObject.SetActive (true);
		}
		else {
			StartCoroutine (LoadGo ());
			loadData.interactable = false;
		}
	}

	IEnumerator CreateGo(){
		WWWForm form = new WWWForm ();
		form.AddField ("Input_user", IDInput.text);
		form.AddField ("Input_pass", PassInput.text);

		WWW webRequest = new WWW (LoginUrl, form);
		yield return  webRequest;

		string output = webRequest.text;
		if (output == "duplication") {
			messagePanelText.text = "This ID already exists!";
			messagePanelText.fontSize = 13;
		} else if (output == "success") {
			messagePanelText.text = "Success!";
			messagePanelText.fontSize = 14;
		} else {
			messagePanelText.text = "Game server is not running now!";
			messagePanelText.fontSize = 12;
		}
		messagePanel.gameObject.SetActive (true);
		yield return null;
	}

	IEnumerator SaveGo(){
		WWWForm form = new WWWForm ();
		form.AddField ("Input_user", IDInput.text);
		form.AddField ("Input_pass", PassInput.text);

		for (int i = 1; i < currentLevel; i++) {
			form.AddField ("Bestrecords[]", PlayerPrefs.GetFloat ("Level" + i + "Bestrecord").ToString("E"));
			form.AddField ("Bestdias[]", PlayerPrefs.GetInt ("Level" + i + "Bestdia").ToString());
			form.AddField ("Bestrecords_str[]", PlayerPrefs.GetString ("Level" + i));
		}

		form.AddField ("CurrentLevel", currentLevel.ToString());
		form.AddField ("Totaldia", PlayerPrefs.GetInt ("Totaldia").ToString());
		WWW webRequest = new WWW (SaveUrl, form);
		yield return  webRequest;

		string output = webRequest.text;
		Debug.Log (webRequest.text);
		if (output == "noid") {
			messagePanelText.text = " ID does not exist!";
			messagePanelText.fontSize = 14;
		} else if (output == "inpa") {
			messagePanelText.text = "Invalid password!";
			messagePanelText.fontSize = 14;
		} else if (output == "success") {
			messagePanelText.text = "Success!";
			messagePanelText.fontSize = 14;
		} else {
			messagePanelText.text = "Game server is not running now!";
			messagePanelText.fontSize = 12;
		}
		messagePanel.gameObject.SetActive (true);
		yield return null;
	}

	IEnumerator LoadGo(){
		WWWForm form = new WWWForm ();
		form.AddField ("Input_user", IDInput.text);
		form.AddField ("Input_pass", PassInput.text);

		WWW webRequest = new WWW (LoadUrl, form);
		yield return  webRequest;
		string output = webRequest.text;

		if (output == "noid") {
			messagePanelText.text = " ID does not exist!";
			messagePanelText.fontSize = 14;
		} else if (output == "inpa") {
			messagePanelText.text = "Invalid password!";
			messagePanelText.fontSize = 14;
		} else if (output.Length>300) {
			string[] tables = output.Split ('/');
			string[] bestdias = tables [0].Split (',');
			string[] bestrecords_str = tables [1].Split(',');
			string[] bestrecords = tables [2].Split(',');
			string[] usertotal = tables [3].Split (',');

			for (int i = 1; i < 41; i++) {
				PlayerPrefs.SetInt ("Level" + i + "Bestdia", int.Parse (bestdias [i - 1]));
				PlayerPrefs.SetString ("Level" + i, bestrecords_str [i - 1]);
				PlayerPrefs.SetFloat ("Level" + i + "Bestrecord", float.Parse (bestrecords [i - 1]));
			}

			PlayerPrefs.SetInt("CurrentLevel", int.Parse(usertotal[0]));
			PlayerPrefs.SetInt ("Totaldia", int.Parse (usertotal[1]));
			messagePanelText.text = "Success!";
			messagePanelText.fontSize = 14;
		} else {
			messagePanelText.text = "Game server is not running now!";
			messagePanelText.fontSize = 12;
		}
		messagePanel.gameObject.SetActive (true);
		yield return null;
	}
}