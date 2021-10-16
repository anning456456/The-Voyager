using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lv2sides : MonoBehaviour
{

    public GameObject rot1;
    public GameObject rot2;
    public GameObject rot3;
    public float rotateSpeed1 = 0;
    public float rotateSpeed2 = 0;
    public float rotateSpeed3 = 0;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        rot1.transform.Rotate(new Vector3(0, 0, 1), rotateSpeed1);
        rot2.transform.Rotate(new Vector3(0, 0, 1), rotateSpeed2);
        rot3.transform.Rotate(new Vector3(0, 0, 1), rotateSpeed3);
    }
}
