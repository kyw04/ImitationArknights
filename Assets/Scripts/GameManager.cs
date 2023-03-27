using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject panel;
    public TextMeshProUGUI HPText;
    public TextMeshProUGUI EnemyCountText;
    public TextMeshProUGUI MaxEnemyCountText;
    public Transform teamBlock;
    public Transform enemyBlock;
    public int HP;

    [HideInInspector]
    public int enemyCount;
    [HideInInspector]
    public int currentEnemyCount;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        currentEnemyCount = 0;
    }

    private void Update()
    {
        HPText.text = HP.ToString();
        EnemyCountText.text = currentEnemyCount.ToString();
        MaxEnemyCountText.text = enemyCount.ToString();
    }
}
