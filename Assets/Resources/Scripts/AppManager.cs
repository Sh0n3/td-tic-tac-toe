using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{
	public const int MENU_SCENE = 0;
	public const int GAME_SCENE = 1;

	public static AppManager instance;

	public int themeNumber;
	public bool isPlayer1X;

	public bool isMusicOn;
	public bool isSfxOn = true;

	private void Awake()
	{
		if (instance != null && instance != this)
		{
			Destroy(gameObject);
			return;
		}
		instance = this;
		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		themeNumber = 1;
		isPlayer1X = true;
		isMusicOn = true;
		isSfxOn = true;
	}

	
}
