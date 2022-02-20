using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class MainSceneButtons : MonoBehaviour {
	public GameObject copyrightPanel;
	public GameObject gameinfoPanel;
	public GameObject accountPanel;
	public GameObject createAccountPanel;
	public GameObject SaveDataPanel;
	public GameObject LoadDataPanel;
	public GameObject ErrorPanel;
	bool isactiveC;
	bool isactiveG;
	bool isactiveA;
	bool isactiveAC;
	bool isactiveAS;
	bool isactiveAL;
	public GameObject loading;
	string webURL = "http://ec2-54-180-152-253.ap-northeast-2.compute.amazonaws.com:8080/home";

	public void OpenCopyright(){
		if (isactiveC == false) {
			copyrightPanel.SetActive (true);
			isactiveC = true;
		} else {
			copyrightPanel.SetActive (false);
			isactiveC = false;
		}
	}

	public void OpenGameinfo(){
		if (isactiveG == false) {
			gameinfoPanel.SetActive (true);
			isactiveG = true;
		} else {
			gameinfoPanel.SetActive (false);
			isactiveG = false;
		}
	}

	public void OpenMemo(){
		Application.OpenURL (webURL);
	}

	public void OpenAccount(){
		if (isactiveA == false) {
			accountPanel.SetActive (true);
			isactiveA = true;
		} else {
			accountPanel.SetActive (false);
			isactiveA = false;
		}
	}

	public void OpenCreateAccount(){
		accountPanel.SetActive (false);
		isactiveA = false;
		if (isactiveAC == false) {
			createAccountPanel.SetActive (true);
			isactiveAC = true;
		} else {
			createAccountPanel.SetActive (false);
			isactiveAC = false;
		}
	}

	public void OpenSaveDataAccount(){
		accountPanel.SetActive (false);
		isactiveA = false;
		if (isactiveAS == false) {
			SaveDataPanel.SetActive (true);
			isactiveAS = true;
		} else {
			SaveDataPanel.SetActive (false);
			isactiveAS = false;
		}
	}

	public void OpenLoadDataAccount(){
		accountPanel.SetActive (false);
		isactiveA = false;
		if (isactiveAL == false) {
			LoadDataPanel.SetActive (true);
			isactiveAL = true;
		} else {
			LoadDataPanel.SetActive (false);
			isactiveAL = false;
		}
	}

	public void OpenError(){
		ErrorPanel.SetActive (false);
	}

	public void Gotostage(){
		loading.SetActive (true);
		SceneManager.LoadScene (1);
	}

	public void Guide(){
		loading.SetActive (true);
		SceneManager.LoadScene (2);
	}	

	public void Quit(){
		Application.Quit ();
	}	
}
