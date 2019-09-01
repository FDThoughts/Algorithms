using System;
using System.Collections.Generic;

namespace TreeBranchLeafSearch
{
    class RootHeight
    {
        static void Main(string[] args)
        {
            Node leaf1 = new Node(null, null);
            Node leaf2 = new Node(null, null);
            Node branch = new Node(leaf1, null);
            Node root = new Node(branch, leaf2);

            Console.WriteLine(root.Height());
            Console.WriteLine(branch.Height());
            Console.WriteLine(leaf1.Height());

            Console.WriteLine("end!");
            Console.ReadLine();
        }
    }

    public class Node
    {
        public Node LeftChild { get; private set; }
        public Node RightChild { get; private set; }

        public Node(Node leftChild, Node rightChild)
        {
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        private List<Node> GetPaths(List<Node> pathNodes,
            Node node, ref int pathHeight)
        {
            if (node == null)
            {
                return pathNodes;
            }

            pathHeight++;
            pathNodes.Add(node);

            if (node.LeftChild == null &&
                node.RightChild == null)
            {
                pathHeight--;
                pathNodes.Remove(node);
                return pathNodes;
            }

            pathNodes = GetPaths(pathNodes, node.LeftChild, ref pathHeight);
            pathNodes = GetPaths(pathNodes, node.RightChild, ref pathHeight);

            pathNodes.Remove(node);

            return pathNodes;
        }

        public int Height()
        {
            int height = 0;
            GetPaths(new List<Node>(), this, ref height);
            return height;
        }
    }
}
