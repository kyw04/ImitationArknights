using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CharacterArrangement : MonoBehaviour
{
    [HideInInspector]
    public GameObject target;
    private GameObject button;
    private Transform floor;
    private Character character;
    private bool selected;
    private bool arrangement;
    private float y;

    private void Start()
    {
        selected = false;
        arrangement = false;
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
                    button.SetActive(false);
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

                if (Physics.Raycast(ray, out hit))
                {
                    Debug.DrawRay(ray.origin, ray.direction, Color.red);

                    floor = hit.collider.transform;
                    if ((hit.collider.CompareTag("FirstFloor") && character.type == Character.Type.Bottom
                            || hit.collider.CompareTag("SecondFloor") && character.type == Character.Type.Top) && floor.childCount == 0)
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
        button = EventSystem.current.currentSelectedGameObject;
        character = button.GetComponent<CharacterButton>().Prefab.GetComponent<Character>();
        y = character.y;
        target = Instantiate(character.gameObject);
    }

    public void CancellButtonClick()
    {

    }
}