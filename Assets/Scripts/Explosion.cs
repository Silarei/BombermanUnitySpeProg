using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private float currentTime;
    void Start()
    {
        
    }
    
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= 2f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponentInParent<BreakableWall>())
        {
            Destroy(other.gameObject);
            var _coroutineSpawnPowerUp = SpawnPowerUp();
            StartCoroutine(_coroutineSpawnPowerUp);
        }
        if (other.GetComponentInParent<Pawn>())
        {
            Debug.Log("Outch");
        }
    }

    private IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Something Shiny !");
    }
}
