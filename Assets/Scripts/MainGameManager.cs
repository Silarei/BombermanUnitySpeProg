using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainGameManager : MonoBehaviour
{
    public bool SoloMode;
    public float CurrentTime;
    public GameObject SecondDwarf;
    public GameObject Victoire;

    void Start()
    {
        GameObject.Find("SaveManager").GetComponent<SaveManager>().LoadData();
        GameObject.Find("SaveManager").GetComponent<SaveManager>().IsItSolo();
        if (!SoloMode)
        {
            var inOrderToRename = Instantiate(SecondDwarf);
            inOrderToRename.name = "SecondDwarf";
        }
        GameObject.Find("SaveManager").GetComponent<SaveManager>().SetKeys();
    }
    void Update()
    {
        CurrentTime += Time.deltaTime;
        if (CurrentTime > 1 && ((SoloMode && CurrentTime > 60) ||
            (!SoloMode && (!GameObject.Find("FirstDwarf") || !GameObject.Find("SecondDwarf"))) ||
            (SoloMode && !GameObject.Find("FirstDwarf"))))
        {
            GameEnded();
        }
    }

    void GameEnded()
    {
        if (!GameObject.Find("FirstDwarf"))
        {
            if (SoloMode)
            {
                Debug.Log("Vous êtes mort comme un gros loser");
            }
            else
            {
                Debug.Log("LeRouge est sortie de la mine avec l'équivalent de " + GameObject.Find("SecondDwarf").GetComponent<Pawn>().Score + " pièces d'or en butin");
            }
        }
        else {
            Debug.Log("LeBleu est sortie de la mine avec l'équivalent de " + GameObject.Find("FirstDwarf").GetComponent<Pawn>().Score + " pièces d'or en butin");
        }

        StartCoroutine(CloseGame());
    }

    private IEnumerator CloseGame()
    {
        
        if (!GameObject.Find("FirstDwarf") && !SoloMode)
        {
            Victoire.GetComponent<TMP_Text>().text = "Victoire du Rouge";
            GameObject.Find("SecondDwarf").transform.position = new Vector3(0f,14.5f,-0.5f);
            GameObject.Find("SecondDwarf").transform.rotation = new Quaternion(0f,180f,270f,1f).normalized;

        }
        else if (!SoloMode)
        {
            Victoire.GetComponent<TMP_Text>().text = "Victoire du Bleu";
            GameObject.Find("FirstDwarf").transform.position = new Vector3(0f,14.5f,-0.5f);
            GameObject.Find("FirstDwarf").transform.rotation = new Quaternion(0f,180f,270f,1f).normalized;
        }
        else if (!GameObject.Find("FirstDwarf"))
        {
            Victoire.GetComponent<TMP_Text>().text = "Défaite !";
        }
        else
        {
            
            Victoire.GetComponent<TMP_Text>().text = "Regarde ton score et essaye de t'améliorer";
        }

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
}