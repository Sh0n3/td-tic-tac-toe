using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PlayManager : MonoBehaviour
{
    static int _movesPlayed  = 0;

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

    public Image[] gridCells;

    public Sprite xSprite;
    public Sprite oSprite;

    bool _isPlayingX = true;

    public void SetSymbol(int cellPositon)
    {
        _movesPlayed++;
        if(_isPlayingX)
        {
            _xMoves.Add(cellPositon);
            gridCells[cellPositon - 1].sprite = xSprite;
			CheckForWin(_xMoves);
		}
        else
        {
			_oMoves.Add(cellPositon);
			gridCells[cellPositon - 1].sprite = oSprite;
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
                    Debug.Log((_isPlayingX ? "X" : "O") + "Wins");
                    return;
                }
			}

		}
		else if (_movesPlayed >= 9)
		{
			Debug.Log("Draw");
			return;
		}
	}
}
