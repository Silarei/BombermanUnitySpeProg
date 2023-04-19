using System.Collections;
using System.Collections.Generic;
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
        if (CurrentTime > 5 && ((SoloMode && CurrentTime > 60) ||
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
        if (!GameObject.Find("FirstDwarf"))
        {
            
        }
        else
        {
            GameObject.Find("FirstDwarf").transform.position = new Vector3();
        }

        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(1);
    }
}