using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
        //since public int nodes hold int #
        public int Data { get; set; }

        public Node Next { get; set; }

        public void DisplayNode()
        {
            Debug.Log(Data);
        }
}
