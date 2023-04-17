using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Pawn : MonoBehaviour
{
    [SerializeField] private float _speed;
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
        
    }

    void UseBomb()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.name);
    }
}
