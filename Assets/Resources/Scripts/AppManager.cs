using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppManager : MonoBehaviour
{
	public const int MENU_SCENE = 0;
	public const int GAME_SCENE = 1;

	public const string PLAYER1_WINS_KEY = "Player1Wins";
	public const string PLAYER2_WINS_KEY = "Player2Wins";
	public const string DRAWS_KEY = "Draws";
	public const string GAME_PLAYED_KEY = "GamesPlayed";
	public const string AVERAGE_TIME_KEY = "AverageTime";

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

	public void SaveGameData(int player1WinsValue, int player2WinsValue,
		int drawsValue, int gamesPlayedValue, int avgTimeValue)
	{
		PlayerPrefs.SetInt(PLAYER1_WINS_KEY, player1WinsValue);
		PlayerPrefs.SetInt(PLAYER2_WINS_KEY, player2WinsValue);
		PlayerPrefs.SetInt(DRAWS_KEY, drawsValue);
		PlayerPrefs.SetInt(GAME_PLAYED_KEY, gamesPlayedValue);
		PlayerPrefs.SetInt(AVERAGE_TIME_KEY, avgTimeValue);
		PlayerPrefs.Save();
	}

	public int GetGameData(string key) 
	{ 
		return PlayerPrefs.GetInt(key, 0); 
	}

	public void ResetGameData()
	{
		PlayerPrefs.DeleteAll();
	}

}
