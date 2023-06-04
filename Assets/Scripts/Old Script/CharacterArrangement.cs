using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterArrangement : MonoBehaviour
{
    [HideInInspector]
    public GameObject target;
    private GameObject targetCanvas;
    private GameObject targetButton;
    private Character targetCharacter;
    private Transform floor;

    public bool selected;
    private bool arrangement;
    private float y;
    private int layerMask;

    private void Start()
    {
        selected = false;
        arrangement = false;
        layerMask = ~LayerMask.GetMask("Player");
    }

    private void Update()
    {
        if (selected)
        {
            if (Input.GetMouseButtonUp(0))
            {
                selected = false;

                if (arrangement)
                {
                    target.transform.parent = floor;
                    targetCanvas.SetActive(true);
                    targetButton.SetActive(false);
                    GameManager.instance.panel.SetActive(true);
                }
                else
                {
                    Destroy(target);
                }
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;

                if (Physics.Raycast(ray, out hit, 20f, layerMask))
                {
                    Debug.DrawRay(ray.origin, ray.direction, Color.red);

                    floor = hit.collider.transform;
                    if ((hit.collider.CompareTag("FirstFloor") && targetCharacter.type == Character.Type.Bottom
                            || hit.collider.CompareTag("SecondFloor") && targetCharacter.type == Character.Type.Top) && floor.childCount == 0)
                    {
                        Vector3 pos = hit.collider.transform.position;
                        target.transform.position = new Vector3(pos.x, y + 1, pos.z);
                        arrangement = true;
                    }
                    else
                    {
                        target.transform.localPosition = new Vector3(hit.point.x, y + 1, hit.point.z);
                        arrangement = false;
                    }
                }
            }
        }
    }

    public void SelectCharacter()
    {
        if (selected)
            return;

        selected = true;
        targetButton = EventSystem.current.currentSelectedGameObject;
        targetCharacter = targetButton.GetComponent<CharacterButton>().Prefab.GetComponentInChildren<Character>();
        y = targetCharacter.y;
        target = Instantiate(targetButton.GetComponent<CharacterButton>().Prefab);
        targetCharacter = target.GetComponentInChildren<Character>();
        targetCanvas = targetCharacter.canvas;
        targetCanvas.SetActive(false);
        targetCharacter.button = targetButton;
    }
}