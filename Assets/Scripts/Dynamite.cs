using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour
{
    public float Range;
    
    private List<Vector3> _areaOfEffect;
    
    void LaunchCountdown()
    {
        _areaOfEffect = new List<Vector3>();
        _areaOfEffect.Add(this.transform.position);
        for (int _currentRange = 1; _currentRange <= Range; _currentRange++)
        {
            
        }
    }
}
