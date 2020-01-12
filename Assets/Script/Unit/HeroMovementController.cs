using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroMovementController : MonoBehaviour {

    [SerializeField] private UnitMotor motor;
    private void Awake()
    {
        motor = GetComponent<UnitMotor>();
    }
    // Use this for initialization
    public void moveUp()
	{
		motor.CheckCanMove(new Vector2(0, 0.5f));
	}
    public void moveDown()
    {
        if (motor.CheckCanMove(new Vector2(0, -0.5f)));
	}
	public void moveRight()
	{
		motor.CheckCanMove(new Vector2(0.5f, 0));
	}
	public void moveLeft()
	{
		motor.CheckCanMove(new Vector2(-0.5f, 0));
	}
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            moveUp();
        }else
        if (Input.GetKeyDown(KeyCode.D))
        {
            moveRight();
        }else
        if (Input.GetKeyDown(KeyCode.S))
        {
            moveDown();
        }else
        if (Input.GetKeyDown(KeyCode.A))
        {
            moveLeft();
        }
    }
}
