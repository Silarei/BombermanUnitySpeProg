using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class SimpleEnnemy : MonoBehaviour
{
    private List<Vector3> _listPotentialNextPosition;
    private Vector3 _nextPosition;

    private void Start()
    {
        _nextPosition = this.transform.position;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Pawn>())
        {
            other.GetComponent<Pawn>().GetHit();
        }
    }
    
    void Update()
    {
        if ((_nextPosition.x / this.transform.position.x < 1.01f &&
             _nextPosition.x / this.transform.position.x > 0.99f) &&
            (_nextPosition.z / this.transform.position.z < 1.01f &&
             _nextPosition.z / this.transform.position.z > 0.99f))
        {
            _listPotentialNextPosition = new List<Vector3>();
            if (!(CheckPotentialNextPosition(_nextPosition + Vector3.right)) &&
                !(CheckPotentialNextPosition(_nextPosition + Vector3.left)) &&
                !(CheckPotentialNextPosition(_nextPosition + Vector3.forward)) &&
                !(CheckPotentialNextPosition(_nextPosition + Vector3.back)))
            {
                _nextPosition = _listPotentialNextPosition[(int)Mathf.Round(Random.Range(0f, (float)_listPotentialNextPosition.Count - 1))];
            }
            
        }

        this.transform.rotation = Quaternion.LookRotation(_nextPosition - this.transform.position);
        this.transform.position = Vector3.MoveTowards(this.transform.position,
            _nextPosition, 0.01f);
    }

    bool CheckPotentialNextPosition(Vector3 _vector3Tempo)
    {
        if (_vector3Tempo is { x: > -14 and < 8, z: < 8 and > -8 })
        {
            if (Physics.OverlapSphere(_vector3Tempo, 0.15f).Length > 0)
            {
                if (Physics.OverlapSphere(_vector3Tempo, 0.15f)[0].gameObject.GetComponent<Pawn>())
                {
                    _nextPosition = _vector3Tempo;
                    return true;
                }
                return false;
            }
            _listPotentialNextPosition.Add(_vector3Tempo);
        }

        return false;
    }
}
