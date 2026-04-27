using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{

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

	// Load Stats

	// Reset Stats

	#endregion

	#region Settings Panel
	
	// Load Settings

	// Turn Music On/Off

	// Turn SFX On/Off

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
