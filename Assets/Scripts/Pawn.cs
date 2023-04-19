using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UIElements;

public class Pawn : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private GameObject _dynamite;
    [SerializeField] private Vector3 _spawnLocation;
    
    public float Range;
    public float Speed;
    public int NumberOfBombs;
    public int NumberOfLife;
    public int Score;

    public KeyCode KeyRight;
    public KeyCode KeyLeft;
    public KeyCode KeyUp;
    public KeyCode KeyDown;
    public KeyCode KeyBomb;
    public KeyCode KeyPickaxe;

    void Update()
    {
        if (GameObject.Find("GameManager").GetComponent<MainGameManager>().CurrentTime <= 60) {
            if (Input.GetKey(KeyRight))
            {
                GoRight();
            }

            if (Input.GetKey(KeyLeft))
            {
                GoLeft();
            }

            if (Input.GetKey(KeyUp))
            {
                GoUp();
            }

            if (Input.GetKey(KeyDown))
            {
                GoDown();
            }

            if (Input.GetKeyDown(KeyBomb))
            {
                UsePickaxe();
            }

            if (Input.GetKeyDown(KeyPickaxe))
            {
                UseBomb();
            }
        }
    }

    public void GoUp()
    {
        this.transform.eulerAngles = new Vector3(0f,0f,0f);
        GetComponent<Rigidbody>().position += this.transform.forward * Speed;
    }
    
    public void GoDown()
    {
        this.transform.eulerAngles = new Vector3(0f,180f,0f);
        GetComponent<Rigidbody>().position += this.transform.forward * Speed;
    }
    
    public void GoRight()
    {
        this.transform.eulerAngles = new Vector3(0f,90f,0f);
        GetComponent<Rigidbody>().position += this.transform.forward * Speed;
    }
    
    public void GoLeft()
    {
        this.transform.eulerAngles = new Vector3(0f,270f,0f);
        GetComponent<Rigidbody>().position += this.transform.forward * Speed;
    }

    public void UsePickaxe()
    {
        var _pickaxeExplosionLocation = new Vector3();
        if (this.transform.eulerAngles == new Vector3(0f, 0f, 0f))
        {
            _pickaxeExplosionLocation = new Vector3(Mathf.Round(this.transform.position.x),
                0.6f, Mathf.Round(this.transform.position.z+1));
        }
        else if (this.transform.eulerAngles == new Vector3(0f, 90f, 0f))
        {
            _pickaxeExplosionLocation = new Vector3(Mathf.Round(this.transform.position.x+1),
                0.6f, Mathf.Round(this.transform.position.z));
        }
        else if (this.transform.eulerAngles == new Vector3(0f, 180f, 0f))
        {
            _pickaxeExplosionLocation = new Vector3(Mathf.Round(this.transform.position.x),
                0.6f, Mathf.Round(this.transform.position.z-1));
        }
        else
        {
            _pickaxeExplosionLocation = new Vector3(Mathf.Round(this.transform.position.x-1), 
                0.6f, Mathf.Round(this.transform.position.z));
        }
        Instantiate(this._explosion, _pickaxeExplosionLocation, this._explosion.transform.rotation);
    }

    public void UseBomb()
    {
        if (this.NumberOfBombs < 1)
        {
            return;
        }
        var _dynamiteLocation = new Vector3();
        if (this.transform.eulerAngles == new Vector3(0f, 0f, 0f))
        {
            _dynamiteLocation = new Vector3(Mathf.Round(this.transform.position.x),
                0.6f, Mathf.Round(this.transform.position.z+1));
        }
        else if (this.transform.eulerAngles == new Vector3(0f, 90f, 0f))
        {
            _dynamiteLocation = new Vector3(Mathf.Round(this.transform.position.x+1),
                0.6f, Mathf.Round(this.transform.position.z));
        }
        else if (this.transform.eulerAngles == new Vector3(0f, 180f, 0f))
        {
            _dynamiteLocation = new Vector3(Mathf.Round(this.transform.position.x),
                0.6f, Mathf.Round(this.transform.position.z-1));
        }
        else
        {
            _dynamiteLocation = new Vector3(Mathf.Round(this.transform.position.x-1), 
                0.6f, Mathf.Round(this.transform.position.z));
        }

        if (_dynamiteLocation is { x: > -14 and < 8, z: < 8 and > -8 })
        {
            var _thisDynamite = new GameObject();
            if (Physics.OverlapSphere(_dynamiteLocation, 0.15f).Length > 0)
            {
                if (!Physics.OverlapSphere(_dynamiteLocation, 0.15f)[0].gameObject.CompareTag("BreakableWall") &&
                    !Physics.OverlapSphere(_dynamiteLocation, 0.15f)[0].gameObject.CompareTag("UnbreakableWall"))
                {
                    _thisDynamite = Instantiate(_dynamite, _dynamiteLocation, _dynamite.transform.rotation);
                }
                else
                {
                    return;
                }
            }
            else
            {
                _thisDynamite = Instantiate(_dynamite, _dynamiteLocation, _dynamite.transform.rotation);
            }
            _thisDynamite.GetComponent<Dynamite>().Range = Range;
            NumberOfBombs--;
        }
    }

    public void GetHit()
    {
        NumberOfLife--;
        Score -= 200;
        if (NumberOfLife != 0)
        {
            this.transform.position = this._spawnLocation;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
}
