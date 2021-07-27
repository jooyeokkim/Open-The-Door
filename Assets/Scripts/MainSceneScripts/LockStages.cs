using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LockStages : MonoBehaviour {
	public Text diaSumText;
	private GameObject level;
	private Image image;
	private Button frameButton;
	private Button imageButton;
	private GameObject button;
	private GameObject dia;
	public Sprite lockimage;
	int diaSum;
	// Use this for initialization
	private int userLevel;
	void Start () {
		// PlayerPrefs.DeleteAll ();
		diaSum = 0;
		userLevel = PlayerPrefs.GetInt ("CurrentLevel", 1);
		Debug.Log (userLevel);
		loadLevel ();
	}

	void loadLevel() {
		for (int i = 1; i <= 40; i++) {
			string levelnum = "LEVEL" + i;
			level = this.gameObject.transform.Find (levelnum).gameObject;
			button = level.transform.Find ("Button").gameObject;
			dia = level.transform.Find ("userdia").gameObject;
			frameButton = level.GetComponent<Button> ();
			imageButton = button.GetComponent<Button> ();
			image = button.GetComponent<Image> ();

			if (i <= userLevel) {
				int userdia = PlayerPrefs.GetInt ("Level" + i + "Bestdia", 0);
				diaSum += userdia;
				for (int j = 1; j <= userdia; j++) {
					Button diaImage = dia.transform.GetChild (j - 1).GetComponent<Button> ();
					diaImage.image.color = new Color (114 / 255f, 233 / 255f, 249 / 255f, 255 / 255f);
				}
			}
			else {
				frameButton.interactable = false;
				imageButton.interactable = false;
				image.sprite = lockimage;
			}
		}
		PlayerPrefs.SetInt ("Totaldia", diaSum);
		diaSumText.text = diaSum.ToString ();
	}
}
