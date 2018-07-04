using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum ButtonStyle
{
    Free,
    Player1,
    Player2
}

public class ChangeButtonStyle : MonoBehaviour 
{
    private ButtonStyle currentStyle;

    public Sprite spriteFree;
    public Sprite spritePlayer1;
    public Sprite spritePlayer2;

	void Start () 
    {
        spriteFree = null;
        currentStyle = ButtonStyle.Free;
	}

    public bool ChangeStyle(ButtonStyle newStyle)
    {
        if (currentStyle != ButtonStyle.Free || newStyle == ButtonStyle.Free)
            return false;

        if (newStyle == ButtonStyle.Player1)
            ChangeColor(false);
        else if (newStyle == ButtonStyle.Player2)
            ChangeColor(true);

        currentStyle = newStyle;
        return true;
    }

    public void SetTextActive()
    {
        gameObject.transform.GetChild(0).gameObject.SetActive(true);
    }

    public void ClearButtonStyle()
    {
        currentStyle = ButtonStyle.Free;
        GetComponent<Image>().overrideSprite = spriteFree;
    }

    private void ChangeColor(bool isPlayerTwo)
    {
        GetComponent<Image>().overrideSprite = isPlayerTwo ? spritePlayer2 : spritePlayer1;
    }
}
