using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SfxManager : MonoBehaviour
{
	public AudioClip clickMenuSound;
	public AudioClip clickGridSound;

	public AudioSource audioSource;

	private void Awake()
	{
		var allButtons = FindObjectsOfType<Button>(true);
		foreach (Button btn in allButtons)
		{
			if (btn.tag == "MenuButton")
			{
				btn.onClick.AddListener(() => PlayClick(clickMenuSound));
			}
			if(btn.tag == "GridButton")
			{
				btn.onClick.AddListener(() => PlayClick(clickGridSound));
			}
		}
	}

	private void PlayClick(AudioClip audioClip)
	{
		audioSource.PlayOneShot(audioClip);
	}
}
