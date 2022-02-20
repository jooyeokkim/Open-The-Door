using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
	public Image mainimage;
	public Button nextbutton;
	public Button previousbutton;
	int imageindex;
	public Sprite[] uiimages;
	void Start () {
		//PlayerPrefs.DeleteAll (); initializing test
		imageindex = 0;
		mainimage.sprite = uiimages [0];
	}
	public void next(){
		imageindex++;
		if (imageindex == uiimages.Length - 1)
			nextbutton.interactable = false;
		else {
			nextbutton.interactable = true;
			previousbutton.interactable = true;
		}
		mainimage.sprite = uiimages [imageindex];
	}
	public void previous(){
		imageindex--;
		if (imageindex == 0)
			previousbutton.interactable = false;
		else {
			nextbutton.interactable = true;
			previousbutton.interactable = true;
		}
		mainimage.sprite = uiimages [imageindex];		
	}
	public void initialize(){
		imageindex = 0;
		mainimage.sprite = uiimages [0];
		previousbutton.interactable = false;
		nextbutton.interactable = true;
	}
}
