using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;
public class Pathfinding : MonoBehaviour
{
    [SerializeField]
    private Tilemap map;
    public GameObject pre;
    public GameObject pref;
    private List<Vector3Int> activeObjects;
    //[SerializeField]private List<Enemy> enemies = new List<Enemy>();
    //public List<Node> way = new List<Node>();
    
    private void Start() 
    {
        //map = GetComponent<Tilemap>();
        //enemies = le
        //Debug.Log("map.WorldToCell(enemy.transform.position)");
        //foreach(Enemy enemy in LevelManager.instance.Units)
        //{
        //    Debug.Log("map.WorldToCell(enemy.transform.position)" + map.WorldToCell(enemy.transform.position));
        //}
        ////open = GetNeighbours(new Node(new Vector2Int(11, 11),true));
      
    }
    public void SetIntactableObjects(List<Vector3Int> activeObjects)
    {
        //this.activeObjects = new List<Vector3Int>();
        this.activeObjects = activeObjects;
    }
    public void SetupMap(Tilemap map)
    {
        this.map = map;
    }
    public List<Vector2Int> FindPath(Vector3 startPoint, Vector3 targetPoint)
    {
        //Debug.Log("startPoint " + startPoint + "targetPoint " + targetPoint);
        Vector2Int start = (Vector2Int)map.WorldToCell(startPoint);
        Vector2Int target = (Vector2Int)map.WorldToCell(targetPoint);

        List<Node> path = new List<Node>();
        Node startNode = new Node(start, true);
        Node endNode = new Node(target, true);
        //Debug.Log("start " + start + "target " + target);
        List<Node> open = new List<Node>();
        List<Node> close = new List<Node>();

        open.Add(startNode);
        //Debug.Log("ope  " + open);
        while (open.Count > 0)
        {
            
            Node current = open[0];

            open.Remove(current);
            close.Add(current);
            
            if (current.Equals(endNode))
            {
                return RetracePath(startNode, current);
            }
            foreach(Node neighbour in  GetNeighboursFourNode(current))
            {
                //Debug.Log("current " + current.Position + "endNode " + endNode.Position);
                if (!neighbour.CanWake || CheckSimiralNodeInList(close, neighbour))
                    continue;
                
                int  newMovementCostToNeighbour = current.gCost + GetManhattenDistance(current, neighbour);
                if(newMovementCostToNeighbour < neighbour.gCost || !open.Contains(neighbour))
                {
                    neighbour.gCost = newMovementCostToNeighbour;
                    neighbour.hCost = GetManhattenDistance(neighbour, endNode);
                    neighbour.Parent = current;

                    if (!CheckSimiralNodeInList(open, neighbour))
                    {
                        open.Add(neighbour);
                        //Instantiate(pre ,new Vector2(neighbour.Position.x+0.5f, neighbour.Position.y+0.5f), Quaternion.identity);
                        open = open.OrderBy(a => a.fCost).ToList();
                    }
                }
            }
            open = open.OrderBy(a => a.fCost).ToList();
        }
        return null;
        
    }
    [SerializeField]
    
    private bool CheckSimiralNodeInList(List<Node> nodes, Node checkNode)
    {
        foreach(Node node in nodes)
        {
            if(node.Equals(checkNode))
                return true;
        }
        return false;
    }
    public void RemoveIntactableObjects()
    {
        activeObjects = null;
    }
    private List<Vector2Int> RetracePath(Node start, Node end)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Node currentNode = end;
  
        while(!currentNode.Equals(start))
        {
            path.Add(currentNode.Position);
            currentNode = currentNode.Parent;
            //Debug.Log(currentNode.Parent);
        }
        path.Reverse();
        
        return path;
    }

    private int GetDistance(Node a, Node b)
    {
        int disX = Mathf.Abs(a.Position.x - b.Position.x);
        int disY = Mathf.Abs(a.Position.y - b.Position.y);

        if(disX > disY)
            return 14 * disY + 10 * (disX - disY);

        return 14 * disX + 10 * (disY - disX);
    }
    private int GetManhattenDistance(Node a, Node b)
    {
        int disX = Mathf.Abs(a.Position.x - b.Position.x);
        int disY = Mathf.Abs(a.Position.y - b.Position.y);

        return disX + disY;
    }
    // Update is called once per frame
   private List<Node> GetNeighbours(Node node)
   {
       List<Node> neighbours = new List<Node>();
        Debug.Log("current ");
       for (int x = -1; x <= 1; x++)
       {
           for(int y = -1; y <= 1; y++)
           {
                if(x == 0 && y == 0)
                    continue;
                
                Vector2Int posn = new Vector2Int(node.Position.x + x, node.Position.y + y);
                if(map.HasTile(new Vector3Int(posn.x, posn.y, 0)))
                {
                    neighbours.Add(new Node(posn,map.HasTile(new Vector3Int(posn.x, posn.y, 0))));
                }
           }
       }
       return neighbours;
   }
    private List<Node> GetNeighboursFourNode(Node node)
    {
        //Debug.Log("current ");
        List<Node> neighbours = new List<Node>();
        int x = node.Position.x;
        int y = node.Position.y;

        if(map.HasTile(new Vector3Int(x + 1, y, 0)) && !HasEnemy(new Vector3Int(x + 1, y, 0)))
        {
            neighbours.Add(new Node(new Vector2Int(x + 1, y),true));
        }
        if(map.HasTile(new Vector3Int(x - 1, y, 0)) && !HasEnemy(new Vector3Int(x - 1, y, 0)))
        {
            neighbours.Add(new Node(new Vector2Int(x - 1, y),true));
        }
        if(map.HasTile(new Vector3Int(x, y + 1, 0)) && !HasEnemy(new Vector3Int(x, y + 1, 0)))
        {
            neighbours.Add(new Node(new Vector2Int(x, y + 1),true));
        }
        if(map.HasTile(new Vector3Int(x, y - 1, 0)) && !HasEnemy(new Vector3Int(x, y - 1, 0)))
        {
            neighbours.Add(new Node(new Vector2Int(x, y - 1),true));
        }
        return neighbours;
    }
    private bool HasEnemy(Vector3Int position)
    {
        if (activeObjects != null)
        { 
            foreach (Vector3Int objectPosition in activeObjects)
            {
                if (objectPosition == position)
                    return true;
            }

            return false;
        }
        else
        { 
            return false;
        }
    }
}

[System.Serializable]
public class Node
{
    [SerializeField]
    private int g; 
    public int gCost
    {
        get{return g;}
        set{g=value;}
    }
    [SerializeField]
    private int h;
    public int hCost
    {
        get{return h;}
        set{h = value;}
    }
    [SerializeField]private int f;
    public int fCost
    {
        get{
            f = gCost + hCost;
            return f;
        }
    }
    [SerializeField]
    private Node parent;
    public Node Parent{get{return parent;}set{parent = value;}}
    [SerializeField]
    private Vector2Int position;
    public Vector2Int Position{ get{return position;} set{position = value;}}
    public bool CanWake{get;set;}
    public Node(Vector2Int position, bool canWake)
    {
        this.Position = position;
        this.CanWake = canWake;
    }
    public bool Equals(Node obj)
    {
        if(obj == null)
            return false;

        return (Position.x == obj.Position.x && Position.y == obj.Position.y);
    }
   
}
