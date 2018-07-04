using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
    public float marginBetweenButtons;

    public GameObject object_OptionsManager;
    OptionsManager optionsManager;

    public GameObject boardLayout;
    public GameObject buttonPrefab;

    public Button scorePlayer1;
    public Button scorePlayer2;
    public Text finalMessage;

    private int[] rowMult;
    private int[] columnMult;

    private bool[,] selected;

    private int[] scores;

    private bool nextPlayer;

	void Start () 
    {
        optionsManager = object_OptionsManager.GetComponent<OptionsManager>();
        optionsManager.GetPlayerPrefs();

        scores = new int[2] { 0, 0 };
        nextPlayer = false;

        CreateBoard();
	}
	
	void Update () 
    {
	
	}

    private void CreateBoard()
    {
        SetMultipliers();
        CreateButtons();
    }

    private void SetMultipliers()
    {
        int boardSize = optionsManager.boardSize;
        MinMax mult = optionsManager.multipliers;

        rowMult = new int[boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            rowMult[i] = Random.Range(mult.min, mult.max+1);
        }

        columnMult = new int[boardSize];
        for (int i = 0; i < boardSize; i++)
        {
            columnMult[i] = Random.Range(mult.min, mult.max + 1);
        }
    }
    private void CreateButtons()
    {
        int boardSize = optionsManager.boardSize;
        selected = new bool[boardSize, boardSize];
        float layoutSize = boardLayout.GetComponent<RectTransform>().sizeDelta.x;
        float squareSize = ( layoutSize - marginBetweenButtons * (boardSize - 1) ) / boardSize;

        for (int i=0; i<boardSize; i++)
        {
            for (int j=0; j<boardSize; j++)
            {
                float x = squareSize / 2 + (marginBetweenButtons + squareSize) * j;
                float y = squareSize / 2 + (marginBetweenButtons + squareSize) * i;
                Vector3 buttonPosition = Vector3.zero;
                GameObject buttonObject = Instantiate(buttonPrefab, buttonPosition, Quaternion.identity, boardLayout.transform) as GameObject;
                Button button = buttonObject.GetComponent<Button>();
                RectTransform rect = button.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector3(x, y, 0);
                rect.sizeDelta = new Vector2(squareSize, squareSize);
                button.name = "Button" + j + "_" + i;
                int value = columnMult[j] *  rowMult[i];
                Text textObject = button.GetComponentInChildren<Text>();
                textObject.text = value.ToString();
                textObject.fontSize = (int)(squareSize / 2);
                textObject.gameObject.SetActive(false);
                button.onClick.AddListener(delegate { ButtonPressed(button); });
            }
        }
    }

    public void ButtonPressed(Button button)
    {
        ChangeButtonStyle changeButtonStyle = button.GetComponent<ChangeButtonStyle>();
        changeButtonStyle.ChangeStyle(nextPlayer ? ButtonStyle.Player2 : ButtonStyle.Player1);
        button.GetComponent<ChangeButtonStyle>().SetTextActive();

        int nIndex = button.name.IndexOf("n");
        int underscoreIndex = button.name.IndexOf("_");
        int j = -1;
        string jText = button.name.Substring(nIndex+1, underscoreIndex - nIndex-1);
        int.TryParse(jText, out j);
        int i = -1;
        string iText = button.name.Substring(underscoreIndex+1);
        int.TryParse(iText, out i);

        if (selected[j, i])
            return;

        selected[j, i] = true;
        int value = columnMult[j] * rowMult[i];
        scores[nextPlayer ? 1 : 0] += value;
        UpdateGUIScores();

        nextPlayer = !nextPlayer;
    }

    private void UpdateGUIScores()
    {
        Text textObject = scorePlayer1.GetComponentInChildren<Text>();
        textObject.text = scores[0].ToString();

        Text textObject2 = scorePlayer2.GetComponentInChildren<Text>();
        textObject2.text = scores[1].ToString();

        CheckWin();
    }

    private void CheckWin()
    {
        int boardSize = optionsManager.boardSize;
        for(int i=0; i<boardSize; i++)
        {
            for (int j=0; j<boardSize; j++)
            {
                if (!selected[j, i])
                    return;
            }
        }

        string message = string.Empty;
        if (scores[1] > scores[0])
            message = "PLAYER 2 WON!!!";
        else if (scores[1] < scores[0])
            message = "PLAYER 1 WON!!!";
        else
            message = "IT IS A TIE!!!";

        finalMessage.text = message;
        finalMessage.gameObject.SetActive(true);
    }
}
