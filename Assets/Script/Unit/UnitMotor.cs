using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class UnitMotor : MonoBehaviour{

	[SerializeField] private Vector3 newPosition;
    public Vector3 NewPosition
    {
        get { return newPosition; }
    }
	[SerializeField] private Vector3 lastPosition;
	public Vector3 LastPosition
	{
		get{return lastPosition;}
	} 	

	[SerializeField] private bool canMove;
	[SerializeField] private Tilemap map;
	public bool CanMove
	{
		get{return canMove;}
		set{canMove = value;}
	}
	[SerializeField] private float speed;
	//public Vector2Int PositionInt
	//{
	//	get{return (Vector2Int)map.WorldToCell(Position);}
	//}
	public Vector3 Position
	{
		get{
			//transform.position = new Vector2(transform.position.x-0.5f, transform.position.y-0.5f);
			return transform.position;
		}
		set{
			transform.position = value;
			//transform.position = new Vector2(transform.position.x, transform.position.y);
		}
	}
	
	// Use this for initialization
	void Start () {
        //anim = GetComponent<Animator>();
        //Position = LevelManager.instance.StartPoint;
        //Position = map.WorldToCell(Position);
        newPosition = Position;
	}
	// Update is called once per frame
	void Update () {
		if(!canMove)
		{
			
		}
		Move();
	}
    public void LeftScale()
    {
        gameObject.transform.localScale = new Vector3
            (
                gameObject.transform.localScale.x * -1, 
                gameObject.transform.localScale.y,
                gameObject.transform.localScale.z
            );
    }
    public void RightScale()
    {
        gameObject.transform.localScale = new Vector3
            (
                gameObject.transform.localScale.x,
                gameObject.transform.localScale.y,
                gameObject.transform.localScale.z
            );
    }
	public bool CheckCanMove(Vector2 direction)
	{
        if (canMove == false)
        {
            newPosition = new Vector3(Position.x + direction.x, Position.y + direction.y, Position.z);
            lastPosition = Position;

            //if(map.HasTile(map.WorldToCell(newPos)))
            //{
            // if (direction.y == 1)
            // {
            // 	anim.Play("up");
            // }
            // if(direction.y == -1)
            // {
            // 	anim.Play("down");
            // }
            // if(direction.x == +1)
            // {
            // 	transform.localScale = new Vector3(direction.x, 1,1);
            // 	anim.Play("walk");
            // }
            // if(direction.x == -1)
            // {
            // 	anim.Play("walk");
            // }
            //if (direction.x != 0)
            //{
            if (direction.x > 0)
                transform.localScale = new Vector3(1, 1, 1);
            if (direction.x < 0)
                transform.localScale = new Vector3(-1, 1, 1);
            //}
            //anim.Play("Walk");
            canMove = true;
            return true;
            //StartCoroutine(PlayFootStap());
            // SoundManager.PlaySound();
            //}
        }
        return false;
	}
	public void Move()
	{
		if(canMove)
		{
			if(Vector3.Distance(Position, newPosition) >= 0.1)
			{
				Position = Vector3.MoveTowards(Position, newPosition, speed * Time.deltaTime);
				//canMove = false;
			}
			else
			{
				Position = newPosition;
				
				canMove = false;
				//GetComponent<Hero>().RemoveStep(1);
				//LevelManager.instance.StartRound(LevelManager.GameState.HeroEnd);
				//SoundManager.i.PlaySound(SoundManager.Sound.PlayerStep);
				//anim.Play("idl");
			}
		}
	}
	IEnumerator PlayFootStap()
	{
		yield return new WaitForSeconds(0.2f);
        //SoundManager.instance.PlaySoundSingle()
			//SoundManager.i.PlaySound(SoundManager.Sound.PlayerStep);
	}
}
