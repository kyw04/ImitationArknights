using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Character : MonoBehaviour
{
    public enum Type { Top, Bottom };
    public Type type;
    public GameObject attackRange;
    public GameObject canvas;
    public Image hpImage;
    public float hp;
    private float currentHp;
    public float damage;
    public float attackSpeed;
    public int magicDefense;
    public int defense;

    [HideInInspector]
    public int defenseCount;
    private List<GameObject> defenseObj = new List<GameObject>();

    [HideInInspector]
    public BoxCollider box;
    [HideInInspector]
    public GameObject button;
    [HideInInspector]
    public float y;

    private void Awake()
    {
        if (type == Type.Bottom)
            y = 1.5f;
        else
            y = 1.25f;

        attackRange.SetActive(false);
        defenseCount = 0;
        box = GetComponent<BoxCollider>();
        box.enabled = false;
        currentHp = hp;
    }

    private void Update()
    {
        SetHP();
    }

    public void CancellButtonClick()
    {
        GameManager.instance.panel.SetActive(false);
        button.SetActive(true);
        Destroy(this.transform.parent.gameObject);
    }

    private void Die()
    {
        box.enabled = false;
        for (int i = 0; i < defenseObj.Count; i++)
        {
            if (defenseObj[i] != null && defenseObj[i].GetComponent<Enemy>() != null)
            {
                Enemy defenceEnemy = defenseObj[i].GetComponent<Enemy>();
                defenceEnemy.Running(true, this);
            }
        }

        button.SetActive(true);
        Destroy(this.transform.parent.gameObject);
    }

    public void SetHP()
    {
        hpImage.gameObject.SetActive(true);
        hpImage.fillAmount = currentHp / hp;
    }

    public void OnDamage(float damage)
    {
        currentHp -= damage;

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && defenseCount < defense)
        {
            Enemy enemy = other.GetComponent<Enemy>();
            if (enemy.isRunning == true)
            {
                enemy.Running(false, this);
                defenseCount++;
                defenseObj.Add(enemy.gameObject);
                //Debug.Log(defenseCount);
            }
        }
    }
}
