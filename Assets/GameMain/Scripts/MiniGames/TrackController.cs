using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackController : MonoBehaviour
{
    public int skipd = -100;
    public float newY = 0;
    public float speed = 10;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.up * Time.deltaTime * speed);
       // print(transform.position.y);
        //print(transform.position.z);

        if (transform.position.y < skipd)
        {
            transform.position = new Vector3(0, newY, 90);
        }
    }
}
