using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class TrackController : MonoBehaviour
{
    public float speed = 10;
    private RectTransform rectTransform;

    //Image image;
    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        
    }
    // Update is called once per frame
    void Update()
    {
        rectTransform.Translate(Vector3.down * Time.deltaTime * speed);

        //Debug.Log(rectTransform.localPosition);
        if (rectTransform.localPosition.y < -1150)
        {
            rectTransform.anchoredPosition3D = new Vector3(0, 1150, 0);
        }
    }
}
