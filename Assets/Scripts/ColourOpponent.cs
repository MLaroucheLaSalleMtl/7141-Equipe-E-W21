using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColourOpponent : MonoBehaviour
{
    [SerializeField] private GameObject panelArena = null;
    [SerializeField] private GameObject panelColour = null;
    private int[] arrayColour = new int[8];
    private int colourTemp = 0;
    private int colourNumber = 0;

    // Start is called before the first frame update
    void Start()
    {
        arrayColour[0] = 0;
        arrayColour[1] = 1;
        arrayColour[2] = 2;
        arrayColour[3] = 3;
        arrayColour[4] = 4;
        arrayColour[5] = 5;
        arrayColour[6] = 6;
        arrayColour[7] = 7;

        PlayerPrefs.DeleteKey("Player");
        PlayerPrefs.DeleteKey("Enemy");

    }

    public void ColourRestart()
    {
        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[colourNumber];
        arrayColour[colourNumber] = colourTemp;
    }
    public void ColourCheck()
    {
        if (arrayColour[0] != 1)
        {
            ColourRestart();
        }
    }
    public void ColourSave()
    {
        PlayerPrefs.SetInt("Player", arrayColour[0]);
        for(int i = 1; i < arrayColour.Length; i++)
        {
            PlayerPrefs.SetInt("Enemy" + i, arrayColour[i]);
        }

        PlayerPrefs.Save();
    }
    public void ChangePanel()
    {
        panelColour.SetActive(false);
        panelArena.SetActive(true);
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

        ColourSave();

        ChangePanel();
    }
    public void ColourBlue()
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[1];
        arrayColour[1] = colourTemp;
        colourNumber = 1;

        ColourSave();

        ChangePanel();
    }
    public void ColourYellow()
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[2];
        arrayColour[2] = colourTemp;
        colourNumber = 2;

        ColourSave();

        ChangePanel();
    }
    public void ColourGreen()
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[3];
        arrayColour[3] = colourTemp;
        colourNumber = 3;

        ColourSave();

        ChangePanel();
    }
    public void ColourOrange()
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[4];
        arrayColour[4] = colourTemp;
        colourNumber = 4;

        ColourSave();

        ChangePanel();
    }
    public void ColourPurple()
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[5];
        arrayColour[5] = colourTemp;
        colourNumber = 5;

        ColourSave();

        ChangePanel();
    }
    public void ColourLightBlue()
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[6];
        arrayColour[6] = colourTemp;
        colourNumber = 6;

        ColourSave();

        ChangePanel();
    }
    public void ColourLightGreen()
    {
        ColourCheck();

        colourTemp = arrayColour[0];
        arrayColour[0] = arrayColour[7];
        arrayColour[7] = colourTemp;
        colourNumber = 7;

        ColourSave();

        ChangePanel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
