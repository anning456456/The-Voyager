using FrameworkExtension;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockLifeSpan : MonoBehaviour
{
    public float RockSum, speed;
    float lifeTime;
    // Start is called before the first frame update
    void Start()
    {
        lifeTime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (lifeTime > RockSum)
        {
            Die();
        }
        lifeTime += Time.deltaTime;
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }

    void Die()
    {
        RockSpawn.instance.Rocks.Remove(gameObject);
        Destroy(gameObject, 0.5f);
    }
}

