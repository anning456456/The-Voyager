using FrameworkExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockSpawn : MonoBehaviour
{
    public static RockSpawn instance;
    public GameObject[] Rock;
    [HideInInspector]
    public List<GameObject> Rocks;
    float timer = 0;
    public float tempP = 5.365f;
    public float speed = 10;
    public float spawnCD;
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
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= spawnCD)
        {
            GameObject go = Instantiate(Rock[Random.Range(0, Rock.Length)], new Vector2(Random.Range(-1.73f, 1.83f), tempP), Quaternion.identity);
            Rocks.Add(go);
            timer = 0;
        }
    }
}
