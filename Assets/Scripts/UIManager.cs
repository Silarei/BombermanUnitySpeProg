using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Pawn _firstDwarf;
    [SerializeField] private Pawn _secondDwarf;
    [SerializeField] private TMP_Text _firstDwarfUI;
    [SerializeField] private TMP_Text _secondDwarfUI;
    void Start()
    {
        if (GameObject.Find("SecondDwarf"))
        {
            _secondDwarf = GameObject.Find("SecondDwarf").GetComponent<Pawn>();
        }
        else
        {
            _secondDwarfUI.text = "";
            _secondDwarfUI.transform.Find("SecondPlayerBomb").GetComponent<TMP_Text>().text = "";
            _secondDwarfUI.transform.Find("SecondPlayerRange").GetComponent<TMP_Text>().text = "";
            _secondDwarfUI.transform.Find("SecondPlayerSpeed").GetComponent<TMP_Text>().text = "";
            _secondDwarfUI.transform.Find("SecondPlayerLife").GetComponent<TMP_Text>().text = "";
            _firstDwarfUI.transform.parent.transform.Find("ScoreSolo").GetComponent<TMP_Text>().text = "Score : " + _firstDwarf.Score;
        }
    }

    void Update()
    {
        if (GameObject.Find("SecondDwarf"))
        {
            _secondDwarf = GameObject.Find("SecondDwarf").GetComponent<Pawn>();
        }
        
        _firstDwarfUI.transform.Find("FirstPlayerBomb").GetComponent<TMP_Text>().text = "Bombe : " + _firstDwarf.NumberOfBombs;
        _firstDwarfUI.transform.Find("FirstPlayerRange").GetComponent<TMP_Text>().text = "Portée : " + _firstDwarf.Range;
        _firstDwarfUI.transform.Find("FirstPlayerSpeed").GetComponent<TMP_Text>().text = "Vitesse : " + _firstDwarf.Speed * 10;
        _firstDwarfUI.transform.Find("FirstPlayerLife").GetComponent<TMP_Text>().text = "" + _firstDwarf.NumberOfLife;
        if (!GameObject.Find("GameManager").GetComponent<MainGameManager>().SoloMode)
        {
            _secondDwarfUI.text = "LeRouge";
            _secondDwarfUI.transform.Find("SecondPlayerBomb").GetComponent<TMP_Text>().text = "Bombe : " + _secondDwarf.NumberOfBombs;
            _secondDwarfUI.transform.Find("SecondPlayerRange").GetComponent<TMP_Text>().text = "Portée : " + _secondDwarf.Range;
            _secondDwarfUI.transform.Find("SecondPlayerSpeed").GetComponent<TMP_Text>().text = "Vitesse : " + _secondDwarf.Speed * 10;
            _secondDwarfUI.transform.Find("SecondPlayerLife").GetComponent<TMP_Text>().text = "" + _secondDwarf.NumberOfLife; 
            _firstDwarfUI.transform.parent.transform.Find("ScoreSolo").GetComponent<TMP_Text>().text = "";
            GameObject.Find("SoloTimeUI").GetComponent<TMP_Text>().text = "";
        }
        else
        {
            _firstDwarfUI.transform.parent.transform.Find("ScoreSolo").GetComponent<TMP_Text>().text = "Score : " + _firstDwarf.Score;
            GameObject.Find("SoloTimeUI").GetComponent<TMP_Text>().text =
                "" + Mathf.Round(60 - GameObject.Find("GameManager").GetComponent<MainGameManager>().CurrentTime);
        }

        
    }
}
