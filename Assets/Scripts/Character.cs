using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum Type { Top, Bottom };
    public Type type;
    public GameObject Range;
    public int damage;
    public int defense;
    public int magicDefense;
    public int block;

    [HideInInspector]
    public float y;

    private void Awake()
    {
        if (type == Type.Bottom)
            y = 1.5f;
        else
            y = 1.25f;
    }
}
