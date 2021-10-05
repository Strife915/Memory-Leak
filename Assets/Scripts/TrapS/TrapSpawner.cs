using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapSpawner : MonoBehaviour
{
    public GameObject trapPrefab;
    public float spawnWait;
    public float spawnMostWait;
    public float spawnLeastWait;
    public int startWait;
    public bool stop = true;
    

    private void Start()
    {
        StartCoroutine("waitSpawner");
    }
    private void Update()
    {
        spawnWait = Random.Range(spawnLeastWait, spawnMostWait);
    }
    IEnumerator waitSpawner()
    {
        if (GameManager.instace.State != GameManager.GameState.IsStarted)
        { 
            yield return new WaitForSeconds(startWait); 
        }
        yield return new WaitForSeconds(startWait);
        
        while(GameManager.instace.State==GameManager.GameState.IsStarted)
        {
            Instantiate(trapPrefab, transform.position,transform.rotation);
            yield return new WaitForSeconds(spawnWait);
        }
    }
}
