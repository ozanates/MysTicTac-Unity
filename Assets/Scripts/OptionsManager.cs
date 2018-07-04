using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*
enum GameMode
{
    OnePlayer,
    TwoPlayers
}

enum BoardSize
{
    FourByFour,
    SixBySix,
    EightByEight
}

enum Multipliers
{
    OneToFour,
    OneToSix,
    OneToEight,
    OneToTen
}
 */

public static class ApplicationModel
{
    static public bool gameMode;
    static public int boardSize;
    static public int minMultiplier;
    static public int maxMultiplier;
}

public class OptionsManager : MonoBehaviour 
{
    public Toggle toggleIs1player;

    public Toggle fourByFour;
    public Toggle sixBySix;
    public Toggle eightByEight;

    public Toggle oneToFour;
    public Toggle oneToSix;
    public Toggle oneToEight;

    public bool is2players { get; set; }

    public int boardSize { get; set; }

    public MinMax multipliers { get; set; }

    public void CheckOptions()
    {
        // Check game mode
        is2players = ! toggleIs1player.isOn;

        // Check board size
        if (fourByFour.isOn)
            boardSize = 4;
        else if (sixBySix.isOn)
            boardSize = 6;
        else if (eightByEight.isOn)
            boardSize = 8;
        else
            boardSize = 10;

        // Check multipliers
        int minMultiplier = 1;
        int maxMultiplier = 10;
        if (oneToFour.isOn)
            maxMultiplier = 4;
        else if (oneToSix.isOn)
            maxMultiplier = 6;
        else if (oneToEight.isOn)
            maxMultiplier = 8;
        multipliers = new MinMax(minMultiplier, maxMultiplier);

        SetPlayerPrefs();
    }

    private void SetPlayerPrefs()
    {
        /*
        PlayerPrefs.SetInt("GameMode", is2players ? 2 : 1);
        PlayerPrefs.SetInt("BoardSize", boardSize);
        PlayerPrefs.SetInt("MinMultiplier", multipliers.min);
        PlayerPrefs.SetInt("MaxMultiplier", multipliers.max);
         * */

        ApplicationModel.gameMode = is2players;
        ApplicationModel.boardSize = boardSize;
        ApplicationModel.minMultiplier = multipliers.min;
        ApplicationModel.maxMultiplier = multipliers.max;
    }

    public void GetPlayerPrefs()
    {
        /*
        is2players = PlayerPrefs.GetInt("GameMode")==2;
        boardSize = PlayerPrefs.GetInt("BoardSize");
        int minMultiplier = PlayerPrefs.GetInt("MinMultiplier");
        if (minMultiplier == 0)
            minMultiplier = 1;
        int maxMultiplier = PlayerPrefs.GetInt("MaxMultiplier");
        if (maxMultiplier == 0)
            maxMultiplier = boardSize;
        multipliers = new MinMax(minMultiplier, maxMultiplier);
         * */

        is2players = ApplicationModel.gameMode;
        boardSize = ApplicationModel.boardSize;
        int minMultiplier = ApplicationModel.minMultiplier;
        int maxMultiplier = ApplicationModel.maxMultiplier;
        multipliers = new MinMax(minMultiplier, maxMultiplier);
    }
}
