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
            //Debug.Log("attack");
            if (character != null && other.CompareTag("Enemy"))
            {
                GameObject[] GameObj = GameObject.FindGameObjectsWithTag("Enemy");
                float min = 100;
                int index = 0;

                for (int i = 0; i < GameObj.Length; i++)
                {
                    float dis = Vector3.Distance(GameObj[i].transform.position, transform.position);
                    if (dis < min)
                    {
                        min = dis;
                        index = i;
                    }
                }

                Enemy target = GameObj[index].GetComponent<Enemy>();
                target.OnDamage(character.damage);
                attackDelay = Time.time + character.attackSpeed;
            }

            if (enemy != null && enemy.isRunning == false && other.CompareTag("Player"))
            {

                GameObject[] GameObj = GameObject.FindGameObjectsWithTag("Player");
                float min = 100;
                int index = 0;

                for (int i = 0; i < GameObj.Length; i++)
                {
                    float dis = Vector3.Distance(GameObj[i].transform.position, transform.position);
                    if (dis < min)
                    {
                        min = dis;
                        index = i;
                    }
                }

                Character target = GameObj[index].GetComponent<Character>();
                target.OnDamage(enemy.damage);
                attackDelay = Time.time + enemy.attackSpeed;
            }
        }
    }
}
