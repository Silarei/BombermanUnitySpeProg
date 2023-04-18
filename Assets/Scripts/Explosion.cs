using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.UIElements;
using UnityEngine;
using Random = UnityEngine.Random;

public class Explosion : MonoBehaviour
{
    [SerializeField] private GameObject _beer;
    [SerializeField] private GameObject _gold;
    [SerializeField] private GameObject _supplyCrate;
    
    private float currentTime;
    void Update()
    {
        currentTime += Time.deltaTime;
        if (currentTime >= 1f)
        {
            Destroy(this.GetComponentInParent<BoxCollider>());
        }
        if (currentTime >= 1.2f)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("BreakableWall"))
        {
            Destroy(other.gameObject);
            var _coroutineSpawnPowerUp = SpawnPowerUp();
            StartCoroutine(_coroutineSpawnPowerUp);
        }
        if (other.GetComponentInParent<Pawn>())
        {
            other.GetComponentInParent<Pawn>().GetHit();
        }
        if (other.GetComponent<PowerUps>())
        {
            Destroy(other.gameObject);
        }
    }

    private IEnumerator SpawnPowerUp()
    {
        yield return new WaitForSeconds(1.05f);
        if (Random.Range(0f, 3f) < 1f)
        {
            var _diceRoll = Random.Range(0f, 3f);
            if (_diceRoll < 1f)
            {
                Instantiate(_beer, new Vector3(this.transform.position.x, 0.3f, this.transform.position.z),
                    _beer.transform.rotation);
            }
            else if (_diceRoll < 2f)
            {
                Instantiate(_supplyCrate, new Vector3(this.transform.position.x, 0.3f, this.transform.position.z),
                                    _supplyCrate.transform.rotation);
            }
            else
            {
                Instantiate(_gold, new Vector3(this.transform.position.x, 0.3f, this.transform.position.z),
                                    _gold.transform.rotation);
            }
        }
        
    }
}
