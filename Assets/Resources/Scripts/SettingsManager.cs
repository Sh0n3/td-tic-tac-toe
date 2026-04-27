using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
	public TextMeshProUGUI musicToggleText;
	public TextMeshProUGUI sfxToggleText;

	private void OnEnable()
	{
		UpdateMusicToggle();
		UpdateSfxToggle();
	}

	public void UpdateMusicToggle()
	{
		if(AppManager.instance.isMusicOn)
		{
			musicToggleText.text = "On";
		}
		else
		{
			musicToggleText.text = "Off";
		}
	}

	public void UpdateSfxToggle()
	{
		if (AppManager.instance.isSfxOn)
		{
			sfxToggleText.text = "On";
		}
		else
		{
			sfxToggleText.text = "Off";
		}
	}
}
