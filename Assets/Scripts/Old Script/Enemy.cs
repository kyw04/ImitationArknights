using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public NavMeshAgent enemy;
    public NavMeshAgent route;
    public GameObject attackRange;
    public Image hpImage;
    public float hp;
    private float currentHp;
    public float damage;
    public float attackSpeed;
    public float speed;

    private Character defenseCharacter;
    private Transform target;
    private NavMeshAgent agent;
    private Vector3 destination;
    //[HideInInspector]
    public bool isRunning;

    private SpriteRenderer sprite;

    private void Start()
    {
        hpImage.gameObject.SetActive(false);
        isRunning = true;
        enemy.enabled = false;
        target = GameManager.instance.teamBlock;
        agent = route;
        destination = agent.destination;
        sprite = GetComponentInChildren<SpriteRenderer>();
        sprite.enabled = false;
        currentHp = hp;
    }

    private void Update()
    {
        if (isRunning == true)
        {
            if (Vector3.Distance(destination, target.position) > 1.0f)
            {
                destination = target.position;
                agent.destination = destination;
            }

            if (Mathf.Abs(Vector3.Distance(route.transform.position, target.position)) <= 0.5f)
            {
                route.gameObject.SetActive(false);
                sprite.enabled = true;

                agent = enemy;
                enemy.enabled = true;
                destination = enemy.destination;
            }

            if (Mathf.Abs(Vector3.Distance(enemy.transform.position, target.position)) <= 0.5f)
            {
                GameManager.instance.HP--;
                Die();
            }
        }
        else
        {
            enemy.enabled = false;
        }

        SetHP();
    }

    public void Stop()
    {
        isRunning = false;
    }
    public void Run()
    {
        destination = target.position;
        agent.destination = destination;
        isRunning = true;
    }

    public void Running(bool bol, Character defense)
    {
        enemy.enabled = bol;
        isRunning = bol;
        defenseCharacter = defense;

        if (bol)
        {
            defenseCharacter = null;
            destination = target.position;
            agent.destination = destination;
        }
    }

    public void SetHP()
    {
        hpImage.fillAmount = currentHp / hp;
    }

    public void OnDamage(float damage)
    {
        hpImage.gameObject.SetActive(true);
        currentHp -= damage;

        if (currentHp <= 0)
            Die();
    }

    private void Die()
    {
        if (defenseCharacter != null)
        {
            defenseCharacter.defenseCount--;
        }
        GameManager.instance.currentEnemyCount++;
        Destroy(this.gameObject);
    }
}
