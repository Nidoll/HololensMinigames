using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallGenerator : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 startPointH;
    public GameObject wallPrefab;
    public GameObject wallPrefabH;
    private GameObject newWall;

    public GameObject verticals;
    public GameObject horizontals;
    public Vector3 nodeStart;

    public Node[,] nodes = new Node[11,11];
    public Vector3 startPointN;

    public class Node
    {
        public Vector2 coord;
        public GameObject top;
        public GameObject bottom;
        public GameObject right;
        public GameObject left;

        public int distance = 0;

        public Node (Vector2 c, GameObject t, GameObject b, GameObject r, GameObject l)
        {
            coord = c;
            top = t;
            bottom = b;
            right = r;
            left = l;
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

    public void GenerateWallsVertical()
    {
        Vector3 newPosition = startPoint;
        for(int y = 0; y < 11; y++){
            for(int x = 0; x < 11; x ++){
                
                newWall = Instantiate(wallPrefab, verticals.gameObject.transform);
                newWall.transform.localPosition = newPosition;
                newWall.layer = 9;

                newPosition = new Vector3(newPosition.x + 0.045f, newPosition.y, newPosition.z);
            }
            newPosition = new Vector3(startPoint.x, startPoint.y, startPoint.z - (0.045f*(y+1)));
        }
    }

    public void GenerateWallsHorizontal()
    {
        Vector3 newPosition = startPointH;
        for(int x = 0; x < 11; x++){
            for(int z = 0; z < 11; z++){

                newWall = Instantiate(wallPrefabH, horizontals.gameObject.transform);
                newWall.transform.localPosition = newPosition;
                newWall.layer = 9;

                newPosition = new Vector3(newPosition.x, newPosition.y, newPosition.z - 0.045f);
            }
            newPosition = new Vector3(startPointH.x - (0.045f*(x+1)), startPointH.y, startPointH.z);
        }
    }

    public void GenerateNodes()
    {
        Vector3 newPosition = startPointN;
        // links -> unten -> rechts -> oben
        GameObject[] walls = new GameObject[4];
        RaycastHit hitInfo;
        Vector3[] directions = {Vector3.left, Vector3.back, Vector3.right, Vector3.forward};

        for(int y = 0; y < 11; y++){
            for(int x = 0; x < 11; x++){
                for(int i = 0; i < 4; i++){
                    if(Physics.Raycast(transform.TransformPoint(newPosition),directions[i],out hitInfo,0.10f,LayerMask.GetMask("Walls"))){
                        walls[i] = hitInfo.transform.gameObject;
                        Debug.Log(hitInfo.transform.gameObject);
                    }else{
                        walls[i] = null;
                    }
                }
                nodes[x,y] = new Node(new Vector3(x,y),walls[3],walls[1],walls[2],walls[0]);
                newPosition += new Vector3(0.045f,0,0);
            }
            newPosition = new Vector3(startPointN.x,startPointN.y,newPosition.z-0.045f);
        }

        Debug.Log("Nodes Generated");
    }

    public void TraverseNodes()
    {
        Stack<Node> stack = new Stack<Node>();
        Node startNode = nodes[0,0];
        List<Node> visited = new List<Node>();

        Node currentNode = startNode;
        List<Node> candidats = new List<Node>();

        int randomCan = 0;

        stack.Push(startNode);
        visited.Add(startNode);

        while(stack.Count != 0){
            currentNode = stack.Peek();
            // checkout right Node
            if(currentNode.coord.x < 10){
                if(!visited.Contains((nodes[(int)currentNode.coord.x + 1,(int)currentNode.coord.y]))){
                    candidats.Add(nodes[(int)currentNode.coord.x + 1,(int)currentNode.coord.y]);
                }
            }

            // checkout left Node
            if(currentNode.coord.x > 0){
                if(!visited.Contains((nodes[(int)currentNode.coord.x - 1,(int)currentNode.coord.y]))){
                    candidats.Add(nodes[(int)currentNode.coord.x - 1,(int)currentNode.coord.y]);
                }
            }

            // checkout top Node
            if(currentNode.coord.y > 0){
                if(!visited.Contains((nodes[(int)currentNode.coord.x,(int)currentNode.coord.y - 1]))){
                    candidats.Add(nodes[(int)currentNode.coord.x,(int)currentNode.coord.y - 1]);
                }
            }

            // checkout bot Node
            if(currentNode.coord.y < 10){
                if(!visited.Contains((nodes[(int)currentNode.coord.x,(int)currentNode.coord.y + 1]))){
                    candidats.Add(nodes[(int)currentNode.coord.x,(int)currentNode.coord.y + 1]);
                }
            }

            if(candidats.Contains(nodes[5,5])){
                candidats.Remove(nodes[5,5]);
            }

            if(candidats.Count > 0){
                randomCan = Random.Range(0,candidats.Count);
                nodes[(int)candidats[randomCan].coord.x,(int)candidats[randomCan].coord.y].distance = currentNode.distance + 1;
                stack.Push(candidats[randomCan]);
                visited.Add(candidats[randomCan]);
                candidats.Clear();
            }else{
                stack.Pop();
                if(stack.Count > 0){
                    visited.Add(stack.Peek());
                }
            }
        }
        Debug.Log("Path generated");
        SetWalls(visited);
    }

    public void SetWalls(List<Node> nodesList)
    {
        nodesList.Reverse();
        Node currentNode;
        Node nextNode;
        Vector2 currentCord;
        Vector2 nextCord;
        Vector2 differenz;

        List<Node> goals = new List<Node>();

        while(nodesList.Count > 1){
            
            currentNode = nodesList[0];
            nextNode = nodesList[1];
            currentCord = currentNode.coord;
            nextCord = nextNode.coord;

            differenz = new Vector2(currentCord.x - nextCord.x, currentCord.y - nextCord.y);

            if(differenz.x != 0){
                if(differenz.x < 0){
                    currentNode.right.transform.gameObject.SetActive(false);
                }else{
                    currentNode.left.transform.gameObject.SetActive(false);
                }
            }else{
                if(differenz.y < 0){
                    currentNode.bottom.transform.gameObject.SetActive(false);
                }else{
                    currentNode.top.transform.gameObject.SetActive(false);
                }
            }

            nodesList.Remove(currentNode);
        }
        Debug.Log("Walls set");
        
        goals.Add(nodes[4,5]);
        goals.Add(nodes[6,5]);
        goals.Add(nodes[5,4]);
        goals.Add(nodes[5,6]);

        while(goals.Count > 1){
            if(goals[0].distance <= goals[1].distance){
                goals.Remove(goals[0]);
            }else{
                goals.Remove(goals[1]);
            }
        }

        if(goals[0] == nodes[4,5]){
            nodes[4,5].right.transform.gameObject.SetActive(false);
        }else if(goals[0] == nodes[6,5]){
            nodes[6,5].left.transform.gameObject.SetActive(false);
        }else if(goals[0] == nodes[5,4]){
            nodes[5,4].bottom.transform.gameObject.SetActive(false);
        }else{
            nodes[5,6].top.transform.gameObject.SetActive(false);
        } 

    }

    public void TestNodes()
    {
        nodes[0,0].right.gameObject.SetActive(false);
        nodes[0,5].bottom.gameObject.SetActive(false);
        nodes[5,5].top.gameObject.SetActive(false);
    }

    public void ResetWalls()
    {
        foreach(Transform child in verticals.transform){
            child.transform.gameObject.SetActive(true);
        }

        foreach(Transform child in horizontals.transform){
            child.transform.gameObject.SetActive(true);
        }
    }
}
