using Entities.Enemies;
using Entities.Player;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesAdder : MonoBehaviour
{
    [SerializeField] private List<EnemyBase> enemies;
    [SerializeField] private PlayerController target;

    // Start is called before the first frame update
    void Start()
    {
        foreach (EnemyBase enemy in enemies)
        {
            enemy.Target = target.transform;
        }
    }
}
