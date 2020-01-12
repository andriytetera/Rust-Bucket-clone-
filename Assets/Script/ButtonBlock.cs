using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonBlock : MonoBehaviour
{
    [SerializeField] private GameObject switchObject;
    [SerializeField] private GameObject prefSpawnEnemy;
    [SerializeField] private List<Vector3> positions;
    private bool isActrive = true;

    [SerializeField] private Sprite switchOnButton;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isActrive)
        {
            if (collision.tag == "player")
            {
                if (switchObject != null)
                {
                    GetComponent<SpriteRenderer>().sprite = switchOnButton;
                    LevelManager.instance.RemoveIntarectable(switchObject.GetComponent<IntarectableObject>());
                    Destroy(switchObject);
                }
                if (prefSpawnEnemy)
                    foreach (Vector3 vector3 in positions)
                        Instantiate(prefSpawnEnemy, vector3, Quaternion.identity);
                isActrive = false;
            }
        }
    }
}
