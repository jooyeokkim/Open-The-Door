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
	public Button messagePanel;
	public Text messagePanelText;
	string LoginUrl;

	void Start(){
		LoginUrl = "http://"+IP.creatorIP+"/otd/create_account.php";
		Debug.Log (LoginUrl);
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
			StartCoroutine (LoginGo ());
			createAccount.interactable = false;
		}
	}	

	IEnumerator LoginGo(){
		Debug.Log (IDInput.text);
		Debug.Log (PassInput.text);
		WWWForm form = new WWWForm ();
		form.AddField ("Input_user", IDInput.text);
		form.AddField ("Input_pass", PassInput.text);

		WWW webRequest = new WWW (LoginUrl, form);
		yield return  webRequest;

		string output = webRequest.text;
		Debug.Log (webRequest.text);
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
}
