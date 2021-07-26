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
			createAccount.interactable = false;
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
}