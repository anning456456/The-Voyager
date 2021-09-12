using FrameworkExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawn : MonoBehaviour
{
    public GameObject[] Rock;
    float timer = 0;
    public float tempP = 5.5f;
    public float speed = 10;
    public float spawnCD;
    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnCD)
        {
            Instantiate(Rock[Random.Range(0, Rock.Length)], new Vector2(Random.Range(-1.73f, 1.83f), tempP), Quaternion.identity);
            timer = 0;
        }
        
    }
}
