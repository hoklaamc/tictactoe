using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player {
	public Image panel;
	public Text text;
	public Button button;
}

[System.Serializable]
public class PlayerColour {
	public Color panelColour;
	public Color textColour;
}

public class GameController : MonoBehaviour {
	public Text[] buttonList;
	
	public GameObject gameOverPanel;
	public Text gameOverText;
	
	public GameObject restartButton;
	public GameObject startInfo;

	public Player playerX;
	public Player playerO;
	public PlayerColour activePlayerColour;
	public PlayerColour inactivePlayerColour;

	private string playerSide;
	private int moveCount;

	void SetGameControllerReferneceOnButtons() {
		for (int i = 0; i < buttonList.Length; i++) {
			buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
		}
	}

	public string GetPlayerSide() {
		return playerSide;
	}

	void SetPlayerColours(Player newPlayer, Player oldPlayer) {
		newPlayer.panel.color = activePlayerColour.panelColour;
		newPlayer.text.color = activePlayerColour.textColour;
		
		oldPlayer.panel.color = inactivePlayerColour.panelColour;
		oldPlayer.text.color = inactivePlayerColour.textColour;
	}

	void SetPlayerColoursInactive() {
		playerX.panel.color = inactivePlayerColour.panelColour;
		playerX.text.color = inactivePlayerColour.textColour;
		
		playerO.panel.color = inactivePlayerColour.panelColour;
		playerO.text.color = inactivePlayerColour.textColour;
	}

	void SetPlayerButtons(bool value) {
		playerX.button.interactable = value;
		playerO.button.interactable = value;		
	}

	void GameOver(string winningPlayer) {
		if (winningPlayer == "draw") {
			SetGameOverText("It's a draw!");
			SetPlayerColoursInactive();
		} else {
			SetGameOverText(winningPlayer + " Wins!");
		}
		SetBoardInteractable(false);
		restartButton.SetActive(true);
	}

	public void EndTurn() {
		moveCount++;
		if ((buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide) ||
				(buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide) ||
				(buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide) ||
				(buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide) ||
				(buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide) ||
				(buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide) ||
				(buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide) ||
				(buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)) {
			GameOver(playerSide);
		} else if (moveCount >= 9) {
			GameOver("draw");
		} else {
			ChangeSides();
		}
	}

	public void RestartGame() {
		moveCount = 0;
		gameOverPanel.SetActive(false);
		for (int i = 0; i < buttonList.Length; i++) {
			buttonList[i].text = "";
		}
		restartButton.SetActive(false);
		startInfo.SetActive(true);
		SetPlayerButtons(true);
		SetPlayerColoursInactive();
	}

	public void SetStartingSide(string startingSide) {
		if (startingSide == "X") {
			SetActivePlayer(playerX);
		} else {
			SetActivePlayer(playerO);
		}
		StartGame();
	}

	void StartGame() {
		SetBoardInteractable(true);	
		SetPlayerButtons(false);	
		startInfo.SetActive(false);
	}

	void SetActivePlayer(Player player) {
		if (player == playerX) {
			SetPlayerColours(playerX, playerO);
			playerSide = "X";
		} else {
			SetPlayerColours(playerO, playerX);
			playerSide = "O";
		}
	}

	void SetBoardInteractable(bool value) {
		for (int i = 0; i < buttonList.Length; i++) {
			buttonList[i].GetComponentInParent<Button>().interactable = value;
		}
	}

	void SetGameOverText(string value) {
		gameOverPanel.SetActive(true);
		gameOverText.text = value;
	}

	void ChangeSides() {
		if (playerSide == "X") {
			SetActivePlayer(playerO);
		} else {
			SetActivePlayer(playerX);
		}
	}

	void Awake() {
		SetGameControllerReferneceOnButtons();
		restartButton.SetActive(false);
		gameOverPanel.SetActive(false);
		moveCount = 0;
	}
}
