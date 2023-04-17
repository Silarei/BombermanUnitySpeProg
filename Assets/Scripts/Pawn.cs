using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Pawn : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject Explosion;
    void Start()
    {
        
    }
    
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
    }

    void GoUp()
    {
        this.transform.eulerAngles = new Vector3(0f,0f,0f);
        GetComponent<Rigidbody>().position += this.transform.forward * _speed;
    }
    
    void GoDown()
    {
        this.transform.eulerAngles = new Vector3(0f,180f,0f);
        GetComponent<Rigidbody>().position += this.transform.forward * _speed;
    }
    
    void GoRight()
    {
        this.transform.eulerAngles = new Vector3(0f,90f,0f);
        GetComponent<Rigidbody>().position += this.transform.forward * _speed;
    }
    
    void GoLeft()
    {
        this.transform.eulerAngles = new Vector3(0f,270f,0f);
        GetComponent<Rigidbody>().position += this.transform.forward * _speed;
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
        Instantiate(Explosion, _pickaxeExplosionLocation, Explosion.transform.rotation);
    }

    void UseBomb()
    {
        
    }
}
