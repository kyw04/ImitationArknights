using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public struct Spawn
    {
        public int index;
        public float time;

        public Spawn(int _index, float _time)
        {
            this.index = _index;
            this.time = _time;
        }
    }

    public List<GameObject> enemy = new List<GameObject>();
    public List<Spawn> spawn = new List<Spawn>();

    private int currentIndex;
    private float spawnTime;

    private void Start()
    {
        spawn.Add(new Spawn(0, 1.5f));
        spawn.Add(new Spawn(0, 0.5f));
        spawn.Add(new Spawn(0, 5.5f));
        spawn.Add(new Spawn(0, 0.5f));
        spawn.Add(new Spawn(0, 0.5f));
        spawn.Add(new Spawn(0, 5.5f));
        spawn.Add(new Spawn(0, 0.5f));
        spawn.Add(new Spawn(0, 0.5f));
        spawn.Add(new Spawn(0, 5f));
        spawn.Add(new Spawn(0, 0.5f));

        GameManager.instance.enemyCount = spawn.Count;

        spawnTime = Time.time + spawn[0].time;
        currentIndex = 0;
    }

    private void Update()
    {
        if (currentIndex >= spawn.Count)
        {
            return;
        }

        if (spawnTime <= Time.time)
        {
            if (enemy.Count > spawn[currentIndex].index)
                Instantiate(enemy[spawn[currentIndex].index], transform.position, Quaternion.Euler(0f, -90f, 0f));
            currentIndex++;

            if (currentIndex < spawn.Count)
                spawnTime = Time.time + spawn[currentIndex].time;
        }
    }
}
