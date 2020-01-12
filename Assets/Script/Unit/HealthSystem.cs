using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    [SerializeField] private GameObject prefHealthPoint;
    [SerializeField] private List<GameObject> healthPoints;
    public void CreateHealthBar(int health, int maxHealth)
    {
        float x = prefHealthPoint.GetComponent<SpriteRenderer>().bounds.size.x;
        // wightCellHealth = prefHealthPoint.GetComponent<SpriteRenderer>().bounds.size.x * (float)maxHealth
        float xStartPosition = ((prefHealthPoint.GetComponent<SpriteRenderer>().bounds.size.x/2) * maxHealth)/2 + (0.03f * maxHealth);

        for(int i = 0; i < maxHealth; i++)
        {
            GameObject temp = Instantiate(prefHealthPoint, gameObject.transform);
            temp.transform.localPosition = new Vector3(-xStartPosition + i * (x + 0.03f), 0.600f, 0);
            healthPoints.Add(temp);
        }
        UpdateHealthPointRander(health);
    }
    public void UpdateHealthPointRander(int health)
    {

        for (int i = 0; i < healthPoints.Count; i++)
        {
            if(i+1 > health)
            {
                healthPoints[i].GetComponent<SpriteRenderer>().color = Color.black;
            }else
                healthPoints[i].GetComponent<SpriteRenderer>().color = Color.red;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
