using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Joystick : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField]
    private GameObject joystick;
    [SerializeField]
    private GameObject direction;
    [SerializeField]
    private RectTransform handle;
    private Vector3 startPos;

    [SerializeField]
    private GameObject cancell;

    [Range(0, 5)]
    public float handleRange;
    public float minTravel = 1f;
    private bool dirSet = false;

    public RectTransform[] pos; // top, left, down, right
    private Character character;
    private Transform attackRange;

    private void Start()
    {
        character = GetComponentInParent<Character>();
        attackRange = character.attackRange.transform;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        startPos = Input.mousePosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 currentPos = Input.mousePosition;
        Vector3 inputDir = (currentPos - startPos) * 0.025f;
        //float x = inputDir.x;
        //float y = inputDir.y;
        //float theta = 90f;
        //inputDir = new Vector3(x * Mathf.Cos(theta) - y * Mathf.Sin(theta), x * Mathf.Sin(theta) + y * Mathf.Cos(theta)) * 0.01f;
        Vector3 clampedDir = inputDir.magnitude < handleRange ? inputDir : inputDir.normalized * handleRange;

        if (Mathf.Abs(clampedDir.x) >= minTravel || Mathf.Abs(clampedDir.y) >= minTravel)
        {
            cancell.SetActive(false);
            attackRange.gameObject.SetActive(true);

            int index = 0;
            float min = 100f;

            for (int i = 0; i < pos.Length; i++)
            {
                Vector3 minusPos = handle.anchoredPosition - pos[i].anchoredPosition;
                float dis = Mathf.Sqrt(minusPos.x * minusPos.x + minusPos.y * minusPos.y);

                if (min > dis)
                {
                    index = i;
                    min = dis;
                }
            }

            direction.transform.rotation = Quaternion.Euler(90f, 0, index * 90f);
            attackRange.transform.rotation = Quaternion.Euler(0, index * -90f, 0);
            dirSet = true;
        }
        else
        {
            cancell.SetActive(true);
            attackRange.gameObject.SetActive(false);

            dirSet = false;
        }

        handle.localPosition = clampedDir;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        handle.anchoredPosition = Vector2.zero;
        cancell.SetActive(true);

        if (dirSet)
        {
            GameManager.instance.panel.SetActive(false);
            joystick.SetActive(false);
            character.box.enabled = true;
            for (int i = 0; i < attackRange.childCount; i++)
            {
                attackRange.GetChild(i).GetComponent<BoxCollider>().enabled = true;
                attackRange.GetChild(i).GetComponent<MeshRenderer>().enabled = false;
            }
        }
    }
}
