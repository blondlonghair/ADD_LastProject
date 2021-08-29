using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngameManager : MonoBehaviour
{
    [SerializeField] Text scoreText;
    [SerializeField] Text hpText;
    [HideInInspector] public float score = 0;
    [HideInInspector] public float hp = 5;
    float skillintervalTime = 30;
    float skillTimer = 30;

    private List<Enemy> enemies = new List<Enemy>();

    public static IngameManager Instance;
    private void Awake() => Instance = this;

    private void Update()
    {
        scoreText.text = score.ToString();
        hpText.text = hp.ToString();

        skillTimer += Time.deltaTime;

        if (Input.GetMouseButtonDown(1) && skillTimer > skillintervalTime)
        {
            skillTimer = 0;
            NotifyObservers();
        }

        if (hp <= 0)
        {
            Time.timeScale = 0;
        }
    }

    public void AddEnemy(Enemy o)
    {
        enemies.Add(o);
    }

    public void DeletEneme(Enemy o)
    {
        enemies.Remove(o);
    }

    public void NotifyObservers()
    {
        foreach (var item in enemies)
        {
            item.OnSkill();
        }
    }
}
