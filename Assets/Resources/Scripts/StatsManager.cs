using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
	public TextMeshProUGUI player1WinsText;
	public TextMeshProUGUI player2WinsText;
	public TextMeshProUGUI drawsText;
	public TextMeshProUGUI gamesPlatedText;
	public TextMeshProUGUI averageTimeText;

	private void OnEnable()
	{
		UpdateStats();
	}

	public void UpdateStats()
	{
		player1WinsText.text = "Player 1 Wins: " + AppManager.instance.GetGameData(AppManager.PLAYER1_WINS_KEY);
		player2WinsText.text = "Player 2 Wins: " + AppManager.instance.GetGameData(AppManager.PLAYER2_WINS_KEY);
		drawsText.text = "Draws: " + AppManager.instance.GetGameData(AppManager.DRAWS_KEY);
		gamesPlatedText.text = "Games Played: " + AppManager.instance.GetGameData(AppManager.GAME_PLAYED_KEY);
		averageTimeText.text = "Average Time: " + AppManager.instance.GetGameData(AppManager.AVERAGE_TIME_KEY);
	}
}
