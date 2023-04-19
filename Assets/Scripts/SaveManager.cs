using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SaveManager : MonoBehaviour
{
    public bool SoloMode;
    public KeyCode FirstDwarfKeyRight;
    public KeyCode FirstDwarfKeyLeft;
    public KeyCode FirstDwarfKeyUp;
    public KeyCode FirstDwarfKeyDown;
    public KeyCode FirstDwarfKeyBomb;
    public KeyCode FirstDwarfKeyPickaxe;
    public KeyCode SecondDwarfKeyRight;
    public KeyCode SecondDwarfKeyLeft;
    public KeyCode SecondDwarfKeyUp;
    public KeyCode SecondDwarfKeyDown;
    public KeyCode SecondDwarfKeyBomb;
    public KeyCode SecondDwarfKeyPickaxe;

    public void IsItSolo()
    {
        if (GameObject.Find("GameManager"))
        {
            GameObject.Find("GameManager").GetComponent<MainGameManager>().SoloMode = SoloMode;
        }
    }

    public void SetKeys()
    {
        if (GameObject.Find("FirstDwarf"))
        {
            GameObject.Find("FirstDwarf").GetComponent<Pawn>().KeyBomb = FirstDwarfKeyBomb;
            GameObject.Find("FirstDwarf").GetComponent<Pawn>().KeyDown = FirstDwarfKeyDown;
            GameObject.Find("FirstDwarf").GetComponent<Pawn>().KeyUp = FirstDwarfKeyUp;
            GameObject.Find("FirstDwarf").GetComponent<Pawn>().KeyPickaxe = FirstDwarfKeyPickaxe;
            GameObject.Find("FirstDwarf").GetComponent<Pawn>().KeyLeft = FirstDwarfKeyLeft;
            GameObject.Find("FirstDwarf").GetComponent<Pawn>().KeyRight = FirstDwarfKeyRight;
        }
        if (GameObject.Find("SecondDwarf"))
        {
            GameObject.Find("SecondDwarf").GetComponent<Pawn>().KeyBomb = SecondDwarfKeyBomb;
            GameObject.Find("SecondDwarf").GetComponent<Pawn>().KeyDown = SecondDwarfKeyDown;
            GameObject.Find("SecondDwarf").GetComponent<Pawn>().KeyUp = SecondDwarfKeyUp;
            GameObject.Find("SecondDwarf").GetComponent<Pawn>().KeyPickaxe = SecondDwarfKeyPickaxe;
            GameObject.Find("SecondDwarf").GetComponent<Pawn>().KeyLeft = SecondDwarfKeyLeft;
            GameObject.Find("SecondDwarf").GetComponent<Pawn>().KeyRight = SecondDwarfKeyRight;
        }
    }

    public void SaveData()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/MyKeys.dat");
        DataSaved data = new DataSaved();
        data.FirstDwarfKeyRight = FirstDwarfKeyRight;
        data.FirstDwarfKeyLeft = FirstDwarfKeyLeft;
        data.FirstDwarfKeyUp = FirstDwarfKeyUp;
        data.FirstDwarfKeyDown = FirstDwarfKeyDown;
        data.FirstDwarfKeyBomb = FirstDwarfKeyBomb; 
        data.FirstDwarfKeyPickaxe = FirstDwarfKeyPickaxe;
        data.SecondDwarfKeyRight = SecondDwarfKeyRight;
        data.SecondDwarfKeyLeft = SecondDwarfKeyLeft;
        data.SecondDwarfKeyUp = SecondDwarfKeyUp;
        data.SecondDwarfKeyDown = SecondDwarfKeyDown;
        data.SecondDwarfKeyBomb = SecondDwarfKeyBomb;
        data.SecondDwarfKeyPickaxe = SecondDwarfKeyPickaxe;
        data.SoloMode = SoloMode;
        bf.Serialize(file, data);
        file.Close();
    }
    
    public void LoadData()
    {
        if (File.Exists(Application.persistentDataPath + "/MyKeys.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MyKeys.dat", FileMode.Open);
            file.Position = 0;
            DataSaved data = (DataSaved)bf.Deserialize(file);
            file.Close();
            FirstDwarfKeyRight = data.FirstDwarfKeyRight;
            FirstDwarfKeyLeft = data.FirstDwarfKeyLeft;
            FirstDwarfKeyUp = data.FirstDwarfKeyUp;
            FirstDwarfKeyDown = data.FirstDwarfKeyDown;
            FirstDwarfKeyBomb = data.FirstDwarfKeyBomb; 
            FirstDwarfKeyPickaxe = data.FirstDwarfKeyPickaxe;
            SecondDwarfKeyRight = data.SecondDwarfKeyRight;
            SecondDwarfKeyLeft = data.SecondDwarfKeyLeft;
            SecondDwarfKeyUp = data.SecondDwarfKeyUp;
            SecondDwarfKeyDown = data.SecondDwarfKeyDown;
            SecondDwarfKeyBomb = data.SecondDwarfKeyBomb;
            SecondDwarfKeyPickaxe = data.SecondDwarfKeyPickaxe;
            SoloMode = data.SoloMode;
            Debug.Log("Game data loaded!");
            GenerateData();
        }
        else
        {
            GenerateData();
        }
    }

    void GenerateData()
    {
        FirstDwarfKeyRight = KeyCode.D;
        FirstDwarfKeyLeft = KeyCode.Q;
        FirstDwarfKeyUp = KeyCode.Z;
        FirstDwarfKeyDown = KeyCode.S;
        FirstDwarfKeyBomb = KeyCode.E; 
        FirstDwarfKeyPickaxe = KeyCode.Space;
        SecondDwarfKeyRight = KeyCode.RightArrow;
        SecondDwarfKeyLeft = KeyCode.LeftArrow;
        SecondDwarfKeyUp = KeyCode.UpArrow;
        SecondDwarfKeyDown = KeyCode.DownArrow;
        SecondDwarfKeyBomb = KeyCode.RightShift;
        SecondDwarfKeyPickaxe = KeyCode.RightControl;
        SaveData();
    }

    public void LaunchSolo()
    {
        SoloMode = true;
        SaveData();
        SceneManager.LoadScene(1);
    }
    
    public void LaunchDuo()
    {
        SoloMode = false;
        SaveData();
        SceneManager.LoadScene(1);
    }

    public void SetFDKRight()
    {
        /*GameObject.Find("ButtonFDKRight").GetComponent<Button>().image.color = new Color(0,0,140,1);
        FirstDwarfKeyRight = KeyCode.Escape;
        while (FirstDwarfKeyRight == KeyCode.Escape || FirstDwarfKeyRight == null);
        {
            Event e = Event.current;
            if (e.isKey && (e.type == EventType.KeyDown))
            {
                FirstDwarfKeyRight = e.keyCode;
            }
        }
        GameObject.Find("ButtonFDKRight").GetComponent<Button>().image.color = new Color(255,255,255,1);
        Debug.Log(FirstDwarfKeyRight);*/
    }
}



[Serializable]
class DataSaved
{
    public bool SoloMode;
    public KeyCode FirstDwarfKeyRight;
    public KeyCode FirstDwarfKeyLeft;
    public KeyCode FirstDwarfKeyUp;
    public KeyCode FirstDwarfKeyDown;
    public KeyCode FirstDwarfKeyBomb;
    public KeyCode FirstDwarfKeyPickaxe;
    public KeyCode SecondDwarfKeyRight;
    public KeyCode SecondDwarfKeyLeft;
    public KeyCode SecondDwarfKeyUp;
    public KeyCode SecondDwarfKeyDown;
    public KeyCode SecondDwarfKeyBomb;
    public KeyCode SecondDwarfKeyPickaxe;
}