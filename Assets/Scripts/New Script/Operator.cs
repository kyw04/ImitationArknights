using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public struct Stats
{
    public int level;
    public float exp;
    public float nextLevelExp;
    public float maxHealth;
    public float currentHealth;
    public float damage;
    public float defense;
    public float magicDefense;
    public float relocation;
    public int cost;
    public int stopCount;
    public float attackSpeed;
    public float lastAttackTime;
}

public class Operator : MonoBehaviour
{
    public enum Type
    {
        None,
        Top,
        Down
    }

    public enum Job
    {
        None,
        // 근거리
        Vanguard,
        Guard,
        Defender,
        Specialist,
        // 원거리
        Sniper,
        Caster,
        Medic,
        Supporter
    }

    public GameObject attackRange;
    public Transform[] attackRangeTransforms;
    public string operatorName;
    public Type type;
    public Job job;
    public Stats stats;
    
    [HideInInspector]
    public GameObject button;
    [HideInInspector]
    public BoxCollider box;
    private List<Enemy> stopEnemy = new List<Enemy>();

    private void Start()
    {
        box = GetComponent<BoxCollider>();
    }

    protected virtual void Update()
    {
        if (stats.attackSpeed + stats.lastAttackTime <= Time.time)
        {
            Attack(GetCloseEntity());
        }
    }

    // block
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && stats.stopCount > stopEnemy.Count)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy.isRunning == true)
            {
                stopEnemy.Add(enemy);
                enemy.Stop();
            }
        }
    }

    public void OnDamage(float damage)
    {
        stats.currentHealth -= damage;

        if (stats.currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        foreach (Enemy enemy in stopEnemy)
        {
            enemy.Run();
        }
        Destroy(this.gameObject);
    }

    protected List<GameObject> GetWithinRangeEntity()
    {
        List<GameObject> objs = new List<GameObject>();

        foreach (Transform range in attackRangeTransforms)
        {
            Collider[] colliders = Physics.OverlapBox(range.position, range.localScale / 2f, Quaternion.identity, 
                                                      LayerMask.GetMask("Entity") & LayerMask.GetMask("Operator"));
            foreach (Collider collider in colliders)
            {
                objs.Add(collider.gameObject);
            }
        }

        return objs;
    }

    protected GameObject GetCloseEntity()
    {
        GameObject obj = null;
        float min = -1;

        foreach (Transform range in attackRangeTransforms)
        {
            Collider[] colliders = Physics.OverlapBox(range.position, range.localScale / 2f, Quaternion.identity, 
                                                      LayerMask.GetMask("Entity") & LayerMask.GetMask("Operator"));
            foreach (Collider collider in colliders)
            {
                float dis = Mathf.Abs(Vector3.Distance(collider.transform.position, transform.position));
                if (min == -1 || min > dis)
                {
                    min = dis;
                    obj = collider.gameObject;
                }
            }
        }

        return obj;
    }

    protected virtual void Attack(GameObject obj)
    {
        if (obj == null)
            return;

        stats.lastAttackTime = Time.time;

        if (obj.CompareTag("Enemy"))
        {
            obj.GetComponent<Enemy>().OnDamage(stats.damage);
            Debug.Log("attack");
        }
    }

    protected virtual void Attack(List<GameObject> objs)
    {
        if (objs.Count <= 0)
            return;

        foreach (GameObject obj in objs)
        {
            Attack(obj);
        }
    }
}
