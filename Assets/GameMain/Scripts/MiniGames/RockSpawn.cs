using FrameworkExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawn : MonoBehaviour
{
    public static RockSpawn instance;
    public GameObject[] Rock_Ui;
    [HideInInspector]
    public List<GameObject> Rocks;
    float timer = 0;
    public float spawnCD;
    private GameObject parent;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }
    private void Start()
    {
        Rocks = new List<GameObject>();
        parent = GameObject.Find("Rocks");
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnCD)
        {
            GameObject go = Instantiate(Rock_Ui[Random.Range(0, Rock_Ui.Length)]);
            go.transform.parent = parent.transform;
            go.transform.localPosition = new Vector3(Random.Range(-270f, 270f), 575, 0);
            go.transform.localScale = Vector3.one;
            Rocks.Add(go);
            timer = 0;
        }
    }
}
