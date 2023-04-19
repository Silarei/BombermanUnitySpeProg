using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    [SerializeField] private int _effect;
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Pawn>())
        {
            switch (_effect)
            {
                case 0 :
                    other.GetComponent<Pawn>().Range++;
                    other.GetComponent<Pawn>().Score += 100;
                    break;
                case 1 :
                    other.GetComponent<Pawn>().NumberOfBombs += 5;
                    other.GetComponent<Pawn>().Score += 50;
                    break;
                case 2 :
                    other.GetComponent<Pawn>().Speed += 0.01f;
                    other.GetComponent<Pawn>().Score += 50;
                    break;
            }

            Destroy(this.gameObject);
        }
    }
}
