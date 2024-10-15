using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static DataNodeThingy;

public class test : MonoBehaviour
{
    public Gen1 gen1;
    public Gen2 gen2;
    class LinkedListNode
    {
        public int data;
        public LinkedListNode next;

        public LinkedListNode(int x)
        {
            data = x;
            next = null;
        }
    }

    class LinkedList
    {
        int count;
        LinkedListNode head = null;

        public LinkedList()
        {
            head = null;
            count = 0;
        }

        public void AddNodeToFront(int data)
        {
            LinkedListNode node = new LinkedListNode(data);
            node.next = head;
            head = node;
            count++;
        }

        public void PrintList()
        {
            LinkedListNode runner = head;
            while(runner != null)
            {
                Debug.Log((runner.data));
                runner = runner.next;
            }
        }

        void update()
        {
            LinkedList linkedList = new LinkedList();
            linkedList.AddNodeToFront(1);
            linkedList.AddNodeToFront(2);
            linkedList.AddNodeToFront(3);
            linkedList.AddNodeToFront(4);
            linkedList.AddNodeToFront(5);
            linkedList.PrintList();
        }
    }
}

