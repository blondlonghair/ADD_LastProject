using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

interface IEnemy
{
    public void OnSkill();
}

public class Enemy : MonoBehaviour, IEnemy
{
    public float MaxHp;
    public float CurHp;

    private void Start()
    {
        IngameManager.Instance.AddEnemy(this);
    }

    void Update()
    {
        transform.Translate(-1 * Time.deltaTime, 0, 0);

        if (CurHp <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void OnSkill()
    {
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        IngameManager.Instance.score += 10;
        IngameManager.Instance.DeletEneme(this);
    }
}
