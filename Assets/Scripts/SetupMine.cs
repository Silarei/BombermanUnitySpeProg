using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupMine : MonoBehaviour
{
    [SerializeField] private List<Vector3> _exceptionGrid;
    [SerializeField] private GameObject _breakableCube;
    void Start()
    {
        for (int _nbLine = -7; _nbLine < 8; _nbLine++)
        {
            for (int _nbColumn = -13; _nbColumn < 8; _nbColumn++)
            {
                if (!_exceptionGrid.Contains(new Vector3(_nbColumn,0.25f,_nbLine)) && !(_nbColumn%2 == 0 && _nbLine%2 == 0))
                {
                    if (Random.Range(0f, 1f) < 0.8f)
                    {
                        Instantiate(_breakableCube, new Vector3(_nbColumn, 0.25f, _nbLine), Quaternion.identity);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
