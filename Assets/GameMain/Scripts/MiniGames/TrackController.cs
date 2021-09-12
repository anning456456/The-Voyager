using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class TrackController : MonoBehaviour
{
    public int skipd = -100;
    public float newY = 0;
    public float speed = 10;
    //Image image;
    private void Start()
    {
        //image = GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.Translate(-Vector3.up * Time.deltaTime * speed);
        
        if (transform.position.y < skipd)
        {
            transform.position = new Vector3(0, newY, 90);
        }
    }
}
