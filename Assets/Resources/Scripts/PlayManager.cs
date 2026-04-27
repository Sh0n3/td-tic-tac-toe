using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
	#region Properties

	[Header("Grid")]
	public Image[] gridCells;

	[Header("Texts")]
	public TextMeshProUGUI timerText;
	public TextMeshProUGUI moveText;
	public TextMeshProUGUI finisherText;

	[Header("Images")]
	public Image player1Symbol;
	public Image player2Symbol;
	public GameObject player1NextMove;
	public GameObject player2NextMove;
	public GameObject gamePanel;

	[Header("Managers")]
	public SettingsManager settingsManager;

	[Header("Win Lines")]
	public List<GameObject> _winLines = new List<GameObject>();

	List<int> _xMoves = new List<int>();
	List<int> _oMoves = new List<int>();

	List<List<int>> _winMoves = new List<List<int>>()
	{
		{ new List<int>(){ 1, 2, 3 } },
		{ new List<int>(){ 4, 5, 6 } },
		{ new List<int>(){ 7, 8, 9 } },
		{ new List<int>(){ 1, 4, 7 } },
		{ new List<int>(){ 2, 5, 8 } },
		{ new List<int>(){ 3, 6, 9 } },
		{ new List<int>(){ 1, 5, 9 } },
		{ new List<int>(){ 3, 5, 7 } },
	};

	Sprite _xSprite;
	Sprite _oSprite;

	bool _isPlayingX = true;
	bool _isGameOver = false;

	int _movesPlayed = 0;
	bool _isTimerOn = true;
	float _timer = 0;

	#endregion

	#region Game Logic

	private void Start()
	{
		var spriteSheetXO = Resources.LoadAll<Sprite>("Sprites/XO/XO-edit");
		_xSprite = spriteSheetXO.First(x => x.name == "X_" + AppManager.instance.themeNumber.ToString());
		_oSprite = spriteSheetXO.First(x => x.name == "O_" + AppManager.instance.themeNumber.ToString());

		player1Symbol.sprite = AppManager.instance.isPlayer1X ? _xSprite : _oSprite;
		player2Symbol.sprite = AppManager.instance.isPlayer1X ? _oSprite : _xSprite;

		player1NextMove.SetActive(AppManager.instance.isPlayer1X ? true : false);
		player2NextMove.SetActive(AppManager.instance.isPlayer1X ? false : true);
	}

	private void Update()
	{
		if (_isTimerOn == true)
		{
			_timer += Time.deltaTime;
			timerText.text = "Time: " + ((int)_timer).ToString() + "s";
		}
	}	

	public void SetSymbol(int cellPositon)
	{
		_movesPlayed++;
		moveText.text = "Moves: " + _movesPlayed.ToString();

		if (_isPlayingX)
		{
			_xMoves.Add(cellPositon);
			gridCells[cellPositon - 1].sprite = _xSprite;
			CheckForWin(_xMoves);
		}
		else
		{
			_oMoves.Add(cellPositon);
			gridCells[cellPositon - 1].sprite = _oSprite;
			CheckForWin(_oMoves);
		}

		_isPlayingX = !_isPlayingX;
		ToggleNextTurn();
		gridCells[cellPositon - 1].GetComponent<Button>().interactable = false;
	}

	private void CheckForWin(List<int> moves)
	{
		if (_movesPlayed >= 5 && !_isGameOver)
		{
			for (int i = 0; i < _winMoves.Count; i++)
			{
				if (_winMoves[i].All(x => moves.Contains(x)))
				{
					SrikeAnimation(i);
					StartCoroutine("GameEnd", _isPlayingX == AppManager.instance.isPlayer1X ? "Player 1" : "Player 2");
					return;
				}
			}
			if (_movesPlayed >= 9)
			{
				StartCoroutine("GameEnd", "Draw");
				return;
			}
		}
	}

	private IEnumerator GameEnd(string finisher)
	{
		_isGameOver = true;
		yield return new WaitForSeconds(0.75f);

		string fullDescription = "";

		int player1Wins = AppManager.instance.GetGameData(AppManager.PLAYER1_WINS_KEY);
		int player2Wins = AppManager.instance.GetGameData(AppManager.PLAYER2_WINS_KEY);
		int draws = AppManager.instance.GetGameData(AppManager.DRAWS_KEY);
		int gamesPlayed = AppManager.instance.GetGameData(AppManager.GAME_PLAYED_KEY);
		int avgTime = AppManager.instance.GetGameData(AppManager.AVERAGE_TIME_KEY);

		switch (finisher)
		{
			case "Player 1":
				finisher += " Wins";
				++player1Wins;
				break;
			case "Player 2":
				finisher += " Wins";
				++player2Wins;
				break;
			case "Draw":
				++draws;
				break;
		}

		AppManager.instance.SaveGameData(
			player1Wins,
			player2Wins,
			draws,
			++gamesPlayed,
			(avgTime == 0 ? (int)_timer : (avgTime + (int)_timer) / 2)
		);

		gamePanel.SetActive(true);
		_isTimerOn = false;

		fullDescription += finisher;
		fullDescription += Environment.NewLine;
		fullDescription += "Time: " + ((int)_timer).ToString() + "s";

		finisherText.text = fullDescription;
	}

	private void SrikeAnimation(int winMovesIndex)
	{
		_winLines[winMovesIndex].SetActive(true);
	}

	private void ToggleNextTurn()
	{
		player1NextMove.SetActive(!player1NextMove.activeSelf);
		player2NextMove.SetActive(!player2NextMove.activeSelf);
	}

	#endregion

	#region Game Panel

	public void RetryGame()
	{
		SceneManager.LoadScene(AppManager.GAME_SCENE);
	}

	public void MainMenu()
	{
		SceneManager.LoadScene(AppManager.MENU_SCENE);
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
}
