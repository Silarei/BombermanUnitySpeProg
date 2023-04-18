using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public float Range;
    
    [SerializeField] private GameObject Explosion;
    
    private List<Vector3> _areaOfEffect;

    private void Start()
    {
        _areaOfEffect = new List<Vector3>();
        _areaOfEffect.Add(this.transform.position);
        var _coroutineLaunchCountdown = LaunchCountdown();
        StartCoroutine(_coroutineLaunchCountdown);
    }

    void Detonate()
    {
        CalculateAreaOfEffect(-1, true);
        CalculateAreaOfEffect(1, true);
        CalculateAreaOfEffect(-1, false);
        CalculateAreaOfEffect(1, false);

        foreach (var _explosionPosition in _areaOfEffect)
        {
            Instantiate(Explosion, _explosionPosition, Explosion.transform.rotation);
        }
    }

    private void CalculateAreaOfEffect(int side, bool x)
    {
        var _vector3Tempo = new Vector3();
        Collider[] _colliderTempo;
        for (int _currentRange = 1; _currentRange <= Range; _currentRange++)
        {
            if (x)
            {
                _vector3Tempo = new Vector3(this.transform.position.x + (_currentRange * side), 0.6f,
                    this.transform.position.z);
            }
            else
            {
                _vector3Tempo = new Vector3(this.transform.position.x, 0.6f,
                    this.transform.position.z + (_currentRange * side));
            }

            _colliderTempo = Physics.OverlapSphere(_vector3Tempo, 0.15f);
            if (_vector3Tempo is { x: < -13 or > 7 } or { z: > 7 or < -7 }) 
            {
                break;
            }
            if (_vector3Tempo is { x: > -14 and < 8, z: < 8 and > -8 } && _colliderTempo.Length < 1)
            {
                CheckDuplicate(_vector3Tempo);
            }

            if (_colliderTempo.Length > 0)
            {
                if (_colliderTempo[0].gameObject.CompareTag("BreakableWall"))
                {
                    CheckDuplicate(_vector3Tempo);
                    return;
                }
                if (_colliderTempo[0].gameObject.CompareTag("UnbreakableWall"))
                {
                    return;
                }
                if (!_colliderTempo[0].gameObject.CompareTag("UnbreakableWall"))
                {
                    CheckDuplicate(_vector3Tempo);
                }
            }
        }
    }

    public void CheckDuplicate(Vector3 vectorToBeChecked)
    {
        var _duplicate = false;
        foreach (var _explosionPosition in _areaOfEffect)
        {
            if (_explosionPosition == vectorToBeChecked)
            {
                _duplicate = true;
            }
        }

        if (!_duplicate)
        {
            _areaOfEffect.Add(vectorToBeChecked);
        }
    }

    private IEnumerator LaunchCountdown()
    {
        yield return new WaitForSeconds(3f);
        Detonate();
        Destroy(this.gameObject);
    }
}
