using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public enum Type { Top, Bottom };
    public Type type;
    public GameObject attackRange;
    public int damage;
    public int defense;
    public int magicDefense;
    public int block;

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

        joystickRect = joystick.GetComponent<RectTransform>();
        handleRect = handle.GetComponent<RectTransform>();
    }

    public void CancellButtonClick()
    {
        button.SetActive(true);
        Destroy(this.gameObject);
    }

    public void DirectionSetting()
    {
        Vector3 dir = handleRect.anchoredPosition - joystickRect.anchoredPosition;
        Vector3 clampedDir = dir.magnitude < handleRange ? dir : dir.normalized * handleRange;

        handleRect.anchoredPosition = clampedDir;
    }
}
