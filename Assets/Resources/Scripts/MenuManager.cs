using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
	public StatsManager statsManager;
	public SettingsManager settingsManager;

	#region Play Panel

	public void SelectTheme(int themeNubmer)
	{
		AppManager.instance.themeNumber = themeNubmer;
	}

	public void SelectPlayerX(bool isPlayer1X)
	{
		AppManager.instance.isPlayer1X = isPlayer1X;
	}

	public void StartGame()
	{
		SceneManager.LoadScene(AppManager.GAME_SCENE);
	}

	#endregion

	#region Stats Panel

	public void ResetStats()
	{
		AppManager.instance.ResetGameData();
		statsManager.UpdateStats();
	}

	#endregion

	#region Settings Panel

	public void ToggleMusic()
	{
		AppManager.instance.isMusicOn = !AppManager.instance.isMusicOn;
		AppManager.instance.ToggleAudio(AppManager.MUSIC_PARAMETER, AppManager.instance.isMusicOn);
		settingsManager.UpdateMusicToggle();
	}

	public void ToggleSfx()
	{
		AppManager.instance.isSfxOn = !AppManager.instance.isSfxOn;
		AppManager.instance.ToggleAudio(AppManager.SFX_PARAMETER, AppManager.instance.isSfxOn);
		settingsManager.UpdateSfxToggle();
	}

	#endregion

	#region Quit Panel

	public void QuitGame()
	{
		Application.Quit();
	}

	#endregion

	#region Popup Panels

	public void OpenPopup(GameObject popupPanel)
	{
		popupPanel.SetActive(true);
	}

	public void ClosePopup(GameObject popupPanel)
	{
		popupPanel.SetActive(false);
	}

	#endregion
}
