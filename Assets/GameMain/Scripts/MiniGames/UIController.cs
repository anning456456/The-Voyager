using NPOI.HPSF;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Transform sideTracks;
    public GameObject tracks;
    Vector3 trackPos;
    private void Start()
    {
        trackPos = new Vector3(0,220,0);
        InvokeRepeating("trackRespawn", 4.0f, 4.0f);
    }
    private void Update()
    {
        
    }
    void trackRespawn()
    {
        //trackPos = tracks.transform.position;
        GameObject nextTrack = Instantiate(tracks, tracks.transform.position, Quaternion.identity, sideTracks);
        //trackPos = nextTrack.transform.position;
    }
}
