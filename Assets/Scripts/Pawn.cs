using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
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
    public GameObject UIRange;
    public GameObject UISpeed;
    public GameObject UIBomb;
    public GameObject UILife;

    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            GoRight();
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            GoLeft();
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            GoUp();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            GoDown();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UsePickaxe();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            UseBomb();
        }
    }

    void UpdateUI()
    {
        
    }

    void GoUp()
    {
        this.transform.eulerAngles = new Vector3(0f,0f,0f);
        GetComponent<Rigidbody>().position += this.transform.forward * Speed;
    }
    
    void GoDown()
    {
        this.transform.eulerAngles = new Vector3(0f,180f,0f);
        GetComponent<Rigidbody>().position += this.transform.forward * Speed;
    }
    
    void GoRight()
    {
        this.transform.eulerAngles = new Vector3(0f,90f,0f);
        GetComponent<Rigidbody>().position += this.transform.forward * Speed;
    }
    
    void GoLeft()
    {
        this.transform.eulerAngles = new Vector3(0f,270f,0f);
        GetComponent<Rigidbody>().position += this.transform.forward * Speed;
    }

    void UsePickaxe()
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
        Instantiate(_explosion, _pickaxeExplosionLocation, _explosion.transform.rotation);
    }

    void UseBomb()
    {
        if (NumberOfBombs < 1)
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
        this.transform.position = _spawnLocation;
    }
}
