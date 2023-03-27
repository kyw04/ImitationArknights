using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public Character character;
    public Enemy enemy;
    private float attackDelay;

    private void Start()
    {
        attackDelay = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        if (attackDelay <= Time.time)
        {
            if (character != null && other.CompareTag("Enemy"))
            {
                Enemy target = other.GetComponent<Enemy>();
                target.OnDamage(character.damage);
                attackDelay = Time.time + character.attackSpeed;
            }

            if (enemy != null && enemy.isRunning == false && other.CompareTag("Player"))
            {
                Character target = other.GetComponent<Character>();
                target.OnDamage(enemy.damage);
                attackDelay = Time.time + enemy.attackSpeed;
            }
        }
    }
}
