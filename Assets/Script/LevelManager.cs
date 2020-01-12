using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;
using EZCameraShake;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private IntarectableObject player;
    [SerializeField] private CameraController cameraController;
    [SerializeField] private Tilemap map;
    [SerializeField] private List<IntarectableObject> intarectableObjects;
    [SerializeField] private Pathfinding pathfinding;
    [SerializeField] private Touch touch;
    public static LevelManager instance;

    // Start is called before the first frame update

    private void Awake()
    {
        instance = this;
        pathfinding = GetComponent<Pathfinding>();
        pathfinding.SetupMap(map);
        cameraController.SetupCameraFolloe(player.transform);
    }
    private void Start()
    {
        
        
        //Debug.Log("map " + map + "pathfinding " + pathfinding);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        { 
            Step(new Vector2(0, 0.5f));
        }
        else
       if (Input.GetKeyDown(KeyCode.D))
        {
            Step(new Vector2(0.5f, 0));
        }
        else
       if (Input.GetKeyDown(KeyCode.S))
        {
            Step(new Vector2(0, -0.5f));
        }
        else
       if (Input.GetKeyDown(KeyCode.A))
        {
            Step(new Vector2(-0.5f, 0));
        }
        if (Input.touches.Length != 0)
        {
            if(!start)
            {
                start = true;
                //touch = Input.GetTouch(0);
                startPosition = Input.GetTouch(0).position;
            }
            if(start)
            {
                float differenceX = startPosition.x - Input.GetTouch(0).position.x;
                float differenceY = startPosition.y - Input.GetTouch(0).position.y;
                //Debug.Log("startPosition.x  " + startPosition.x);
                //Debug.Log("startPosition.y  " + startPosition.y);
                //Debug.Log("Input.GetTouch(0).position.x  " + Input.GetTouch(0).position.x);
                //Debug.Log("Input.GetTouch(0).position.y  " + Input.GetTouch(0).position.y);
                //Debug.Log("differenceX  " + differenceX);
                //Debug.Log("differenceY  " + differenceY);
                if (differenceX > 70)
                {
                    Step(new Vector2(-0.5f, 0));
                    start = false;
                    Debug.Log(differenceX);
                }
                else if(differenceX < -70)
                {
                    Step(new Vector2(0.5f, 0));
                    start = false;
                    Debug.Log(differenceX);
                }
                if (differenceY > 70)
                {
                    Step(new Vector2(0, -0.5f));
                    start = false;
                    Debug.Log(differenceY);
                }
                else if (differenceY < -70)
                {
                    Step(new Vector2(0, 0.5f));
                    start = false;
                    Debug.Log(differenceY);
                }
            }
        }
        if (Input.touches.Length == 0)
        {
            start = false;
            //  startPosition.normalized
        }

        //if (-60 > -50)
        //    Debug.Log("dsds");
    }
    [SerializeField] Vector2 startPosition;
    [SerializeField] private bool start = false;
    public void Step(Vector2 direction)
    {
        Vector3 newPositionInWorld = new Vector3
            (
                player.Position.x + direction.x, 
                player.Position.y + direction.y, 
                player.Position.z
            );

        Vector3Int newPositionInCell = map.WorldToCell(newPositionInWorld);
        /////////////////////////////////////////////////
        //if PlayerStep return false it == "block" and step not complite 
        //////////////////////////////////////////
        if (!PlayerStep(newPositionInCell, direction))
            return;

        for (int i = 0; i < intarectableObjects.Count; i++)
        {
            Vector2Int tempNewPosition;
            Vector2 directionForEnemy = new Vector2Int();
            if (intarectableObjects[i].nameObject == "enemy")
            {
                directionForEnemy = DirectuinForEnemy(intarectableObjects[i], out tempNewPosition);
            }
            else
                tempNewPosition = (Vector2Int)map.WorldToCell(intarectableObjects[i].Position);
            //if(intarectableObjects[i].nameObject = "trap")
            
            if((Vector2Int)map.WorldToCell(player.NewPosition) == tempNewPosition)
            { 
                if(player.TakeHit(intarectableObjects[i].Attack()))
                {
                    RestarLevel();
                }
                //Debug.Log(intarectableObjects[i].nameObject + Vector2.Distance(player.Position,
                //    intarectableObjects[i].Position));

                //Debug.Log(intarectableObjects[i].name + "  " 
                //    + map.WorldToCell(player.NewPosition) + "  ---  " 
                //    + map.WorldToCell(intarectableObjects[i].NewPosition));

            }
            else
            {
                foreach (IntarectableObject intarectableObject in intarectableObjects)
                {
                    //if (intarectableObject.nameObject == "trap")
                    //{
                    //    //Debug.Log("if (tempNewPosition " + tempNewPosition +
                    //    //    " == (Vector2Int)map.WorldToCell(intarectableObject.NewPosition))   " + (Vector2Int)map.WorldToCell(intarectableObject.NewPosition));
                    //    //Debug.Log(intarectableObject.gameObject.name);
                    //    if (tempNewPosition == (Vector2Int)map.WorldToCell(intarectableObject.Position))
                    //    {
                    //        //Debug.Log("intarectableObject.Attack()  trap   " + intarectableObject.Attack());
                    //        if (intarectableObjects[i].TakeHit(intarectableObject.Attack()))
                    //        {
                    //            DeleteIntarectale(intarectableObjects[i]);
                    //        }
                    //    }
                    //    //break;
                    //}
                    //Debug.Log(tempNewPosition + "(Vector2Int)map.WorldToCell(intarectableObject.Position  " + (Vector2Int)map.WorldToCell(intarectableObject.Position));
                    if (tempNewPosition == (Vector2Int)map.WorldToCell(intarectableObject.NewPosition))
                    {
                        if (intarectableObject.nameObject == "enemy")
                        {
                            directionForEnemy.x = 0;
                            directionForEnemy.y = 0;
                            //Debug.Log("fdsdddddddddddddddddd");
                            break;
                        }
                    }
                }
                intarectableObjects[i].Move(directionForEnemy);
            }
            //}
        }
    }
    public void AddIntarectable(IntarectableObject intarectableObject)
    {
        intarectableObjects.Add(intarectableObject);
    }
    private List<Vector3Int> GetIntarectalePositoin3Int()
    {
        List<Vector3Int> tempVector3Int = new List<Vector3Int>();
        //tempVector3Int.Add(map.WorldToCell(player.NewPosition));
        foreach(IntarectableObject activeObject in intarectableObjects)
        {
            if(activeObject.nameObject == "vasa" || activeObject.nameObject == "block")
                tempVector3Int.Add(map.WorldToCell(activeObject.NewPosition));
        }
        return tempVector3Int;
    }
    private Vector2 DirectuinForEnemy(IntarectableObject enemy)
    {
        pathfinding.SetIntactableObjects(GetIntarectalePositoin3Int());
        List<Vector2Int> path = pathfinding.FindPath(enemy.Position, player.Position);
        

        if(path == null)
        {
            return new Vector2(0, 0);
        }
        Vector2Int vector2 = (Vector2Int)map.WorldToCell(enemy.Position);
        //Debug.Log(path);

        Vector2 directionForEnemy = (Vector2)(path[0] - vector2);

        //Debug.Log("map.WorldToCell(intarectableObjects[i].Position  " +
        //   map.WorldToCell(intarectableObjects[i].Position) +
        //   "path[0]  " + path[0]);

        //Debug.Log(directionForEnemy);

        if (directionForEnemy.x > 0)
        {
            directionForEnemy.x = (directionForEnemy.x - 0.5f);
            //Debug.Log(directionForEnemy.x);
        }
        if (directionForEnemy.x < 0)
        {
            directionForEnemy.x = (directionForEnemy.x + 0.5f);
        }
        if (directionForEnemy.y > 0)
        {
            directionForEnemy.y = (directionForEnemy.y - 0.5f);
        }
        if (directionForEnemy.y < 0)
        {
            directionForEnemy.y = (directionForEnemy.y + 0.5f);
        }
        return directionForEnemy;
    }
    private Vector2 DirectuinForEnemy(IntarectableObject enemy, out Vector2Int tempNewPosition)
    {
        pathfinding.SetIntactableObjects(GetIntarectalePositoin3Int());
        List<Vector2Int> path;
        path = pathfinding.FindPath(enemy.Position, player.Position);
        

        if (path == null)
        {
            tempNewPosition = (Vector2Int)map.WorldToCell(enemy.Position);
            return new Vector2(0, 0);
        }
        else
        {
            tempNewPosition = path[0];
        }
        Vector2Int vector2 = (Vector2Int)map.WorldToCell(enemy.Position);
        //Debug.Log(path);

        Vector2 directionForEnemy = (Vector2)(path[0] - vector2);

        //Debug.Log("map.WorldToCell(intarectableObjects[i].Position  " +
        //   map.WorldToCell(intarectableObjects[i].Position) +
        //   "path[0]  " + path[0]);

        //Debug.Log(directionForEnemy);

        if (directionForEnemy.x > 0)
        {
            directionForEnemy.x = (directionForEnemy.x - 0.5f);
            //Debug.Log(directionForEnemy.x);
        }
        if (directionForEnemy.x < 0)
        {
            directionForEnemy.x = (directionForEnemy.x + 0.5f);
        }
        if (directionForEnemy.y > 0)
        {
            directionForEnemy.y = (directionForEnemy.y - 0.5f);
        }
        if (directionForEnemy.y < 0)
        {
            directionForEnemy.y = (directionForEnemy.y + 0.5f);
        }
        return directionForEnemy;
    }
    private bool PlayerStep(Vector3Int newPositionInCell, Vector2 direction)
    {
        if (map.HasTile(newPositionInCell))
        {
            Vector3Int vector3Int;
            for (int i = 0; i < intarectableObjects.Count; i++)
            {
                

                vector3Int = map.WorldToCell(intarectableObjects[i].Position);
                if (newPositionInCell.Equals(vector3Int))
                {
                    if (intarectableObjects[i].nameObject == "block")
                    {
                        Item item = Invantory.instence.HasKeyItem();
                        if (item != null)
                        {
                            intarectableObjects[i].Attack();
                            RemoveIntarectable(intarectableObjects[i]);
                            Invantory.instence.RemoveItem(item);
                        }
                        return false;
                    }
                    
                    //StartCoroutine(cameraController.Shake(0.15f, 0.2f));
                    if (intarectableObjects[i].nameObject != "trap")
                    { 
                        CameraShaker.Instance.ShakeOnce(4f, 4f, 0.1f, 1f);
                        intarectableObjects[i].TakeHit(player.Attack());
                        return true;
                    }
                }
            }
            player.Move(direction);
            return true;
        }
        return false;
    }
    public void RemoveIntarectable(IntarectableObject intarectableObject)
    {
        intarectableObjects.Remove(intarectableObject);
    }
    public void DeleteIntarectale(IntarectableObject intarectableObject)
    {
        Destroy(intarectableObject.gameObject);
        intarectableObjects.Remove(intarectableObject);
    }
    public void RestarLevel()
    {
        player.SetHealth(player.MaxHeath);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void OnUp()
    {
        Step(new Vector2(0, 0.5f));
    }
    public void OnDown()
    {
        Step(new Vector2(0, -0.5f));
    }
    public void OnLeft()
    {
        Step(new Vector2(-0.5f, 0));
    }
    public void OnRight()
    {
        Step(new Vector2(0.5f, 0));
    }













}
