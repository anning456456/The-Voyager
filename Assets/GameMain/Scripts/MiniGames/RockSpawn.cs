using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawn : MonoBehaviour
{
    public GameObject[] Rock;
    float timer = 0;
    public float tempP = 5.5f;
    public float speed = 10;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 1)
        {
            Instantiate(Rock[Random.Range(0, Rock.Length)], new Vector2(Random.Range(10, -8), tempP), Quaternion.identity);
            timer = 0;
            //this.transform.Translate(-Vector3.up * Time.deltaTime * speed);
        }

      

    }
}
