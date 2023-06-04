using System;
using UnityEngine;


[Serializable] public struct Stats
{
    public int level;
    public float exp;
    public float nextLevelExp;
    public float helth;
    public float damage;
    public float defense;
    public float magicDefense;
    public float relocation;
    public float cost;
    public float stopCount;
    public float attackSpeed;
}

public enum Type
{
    None,
    Top,
    Down
}

public class Operator : MonoBehaviour
{
    public Type type;
    public Stats stats;
}
