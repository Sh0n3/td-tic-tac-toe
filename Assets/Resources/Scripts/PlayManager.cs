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
    int _movesPlayed = 0;

    public Image[] gridCells;

    public TextMeshProUGUI timerText;
    public TextMeshProUGUI finisherText;

    public GameObject gamePanel;

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

    bool _isTimerOn = true;
    float _timer = 0;

	private void Start()
	{
        var spriteSheetXO = Resources.LoadAll<Sprite>("Sprites/XO/XO-edit");
        _xSprite = spriteSheetXO.First(x => x.name == "X_" + AppManager.instance.themeNumber.ToString());
        _oSprite = spriteSheetXO.First(x => x.name == "O_" + AppManager.instance.themeNumber.ToString());
	}

	private void Update()
	{
        if (_isTimerOn == true)
        {
            _timer += Time.deltaTime;
			timerText.text = "Time: " + ((int)_timer).ToString();
		}
	}

	#region Game Logic

	public void SetSymbol(int cellPositon)
    {
        _movesPlayed++;
        if(_isPlayingX)
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
		gridCells[cellPositon - 1].GetComponent<Button>().interactable = false;       
	}

    private void CheckForWin(List<int> moves)
    {
		if (_movesPlayed >= 5 && _movesPlayed < 9)
		{
			for (int i = 0; i < _winMoves.Count; i++)
            {
                if(moves.All(x => _winMoves[i].Contains(x)))
                {
                    GameEnd(_isPlayingX == AppManager.instance.isPlayer1X ? "Player 1" : "Player 2");
                    return;
                }
			}

		}
		else if (_movesPlayed >= 9)
		{
			GameEnd("Draw");
			return;
		}
	}

    private void GameEnd(string finisher)
    {
        string fullDescription = "Winner: ";

        gamePanel.SetActive(true);
		_isTimerOn = false;

        switch(finisher)
        {
            case "Player 1":

				break;
            case "Player 2":

				break;
            case "Draw":
				fullDescription = "";
				break;
        }
        fullDescription += finisher;
        fullDescription += Environment.NewLine;
        fullDescription += "Time: " +((int)_timer).ToString();

        finisherText.text = fullDescription;
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
}
