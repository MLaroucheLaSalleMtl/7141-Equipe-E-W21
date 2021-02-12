using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourOpponent : MonoBehaviour
{
    //[SerializeField] private GameObject panelColour = null;
    private string[] arrayColour = new string[8];
    private string colourTemp = " ";
    private int colourNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        arrayColour[0] = "Red";
        arrayColour[1] = "Blue";
        arrayColour[2] = "Yellow";
        arrayColour[3] = "Green";
        arrayColour[4] = "Orange";
        arrayColour[5] = "Purple";
        arrayColour[6] = "LightBlue";
        arrayColour[7] = "LightGreen";
    }

    public void ColourRestart()
    {
        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[colourNumber];
        arrayColour[colourNumber] = colourTemp;
    }
    public void ColourCheck()
    {
        if (arrayColour[0] != "Red")
        {
            ColourRestart();
        }
    }
    /*public string[] ColourExit()
    {
        panelColour.SetActive(false);

        return arrayColour;
    }*/
    public void ColourRed()
    {
        ColourCheck();

        colourNumber = 0;
    }
    public void ColourBlue()
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[1];
        arrayColour[1] = colourTemp;
        colourNumber = 1;
    }
    public void ColourYellow()
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[2];
        arrayColour[2] = colourTemp;
        colourNumber = 2;
    }
    public void ColourGreen()
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[3];
        arrayColour[3] = colourTemp;
        colourNumber = 3;
    }
    public void ColourOrange()
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[4];
        arrayColour[4] = colourTemp;
        colourNumber = 4;
    }
    public void ColourPurple()
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[5];
        arrayColour[5] = colourTemp;
        colourNumber = 5;
    }
    public void ColourLightBlue()
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[6];
        arrayColour[6] = colourTemp;
        colourNumber = 6;
    }
    public void ColourLightGreen()
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[7];
        arrayColour[7] = colourTemp;
        colourNumber = 7;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
