using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Xml.Linq;
using UnityEngine;



public class DataNodeThingy : MonoBehaviour
{
    public GameManager gm;
    public class Node
    {
        //since public int nodes hold int #
        public int Data { get; set; }

        public Node Next { get; set; }

        public void DisplayNode()
        {
            Debug.Log(Data);
        }
    }

    public class LinkedList
    {
        public Gen1 gen1;
        public Gen2 gen2;

        public Node Head { get; set; }
        //make a new node a put data inside
        public void InsertHead(int data)
        {
            Node newNode = new Node();
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

        
    }

    
}

