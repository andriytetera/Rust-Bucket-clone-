using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChanceDropSystem : MonoBehaviour
{
    [SerializeField] private List<GameObject> poolChanceObgects;
    // Start is called before the first frame update
    public void CreateRandonObject()
    {
        int chance = Random.Range(0, 100);
        if (chance < 70)
            return;

        if (chance > 70)
            if (chance > 90)
            {
                Instantiate(poolChanceObgects[0], gameObject.transform.position, Quaternion.identity);
            }
            else
            {
                Instantiate(poolChanceObgects[1], new Vector3
                    (
                        gameObject.transform.position.x, 
                        gameObject.transform.position.y - 0.11f
                    ), 
                    Quaternion.identity);

            }
    }
}
