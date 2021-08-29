using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    float spawnIntervalTime = 3;
    float spawnTimer = 3;
    float itemSpawnTimer = 0;

    [SerializeField] GameObject Prefab;
    [SerializeField] GameObject[] Item;


    void Update()
    {
        spawnTimer += Time.deltaTime;
        itemSpawnTimer += Time.deltaTime;

        if (spawnTimer > spawnIntervalTime)
        {
            spawnIntervalTime = Random.Range(1, 3);
            float randPos = Random.Range(-4.5f, 4.5f);
            Instantiate(Prefab, new Vector3(7.5f, randPos, 0), Quaternion.identity);
            spawnTimer = 0;
        }

        if (itemSpawnTimer > 10)
        {
            itemSpawnTimer = 0;
            int rand = Random.Range(0, 4);
            float randPos = Random.Range(-4.5f, 4.5f);
            Instantiate(Item[rand], new Vector3(7.5f, randPos, 0), Quaternion.identity);
        }
    }
}
