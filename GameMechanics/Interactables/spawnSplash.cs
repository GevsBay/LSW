using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnSplash : MonoBehaviour
{
    private float time = 0;
    private float duration = 0.2f;

    public float rangeX = 4f;
    public float rangeY = 5f;
    private Vector3 offset;

    private void Start()
    {
        offset = new Vector3(Random.Range(-rangeX, rangeX), Random.Range(rangeY / 3, rangeY), 0); 
    }

    private void Update()
    { 
        if (time <= duration)
        {
            //position of coin
            transform.position += offset * Time.deltaTime;
            time += Time.deltaTime;
        }
    }
}
