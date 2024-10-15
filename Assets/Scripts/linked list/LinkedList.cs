using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedList : MonoBehaviour
{
    public GameManager gm;
    int count;

    private void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public Node Head { get; set; }
    //make a new node a put data inside
    public void InsertHead(int data)
    {
        Node newNode = gameObject.AddComponent<Node>();
        newNode.Data = data;
        //make old node next & new node head
        newNode.Next = Head;
        Head = newNode;
    }


    public Node DeleteHead()
    {
        //assign the temp variable
        Node temp = Head;
        //assign next head
        Head = Head.Next;
        return temp;
    }

    public void DisplayList()
    {
        Debug.Log("Iterating thru list... ");
        Node current = Head;
        while (current != null)
        {
            current.DisplayNode();
            current = current.Next;
        }
    }

    public void InsertLast(int data)
    {
        Node current = Head;
        while (current != null)
        {
            current = current.Next;
        }
        Node newNode = new Node();
        newNode.Data = data;
        current.Next = newNode;
    }
    public int getCount()
    {
        Node temp = Head;
        int count = 0;
        while (temp != null)
        {
            count++;
            temp = temp.Next;
        }
        return count;
    }

    public void addLifeGen()
    {
        if (getCount() == 5)
        {
            gm.unlockLifeGen();
        }
    }
}
