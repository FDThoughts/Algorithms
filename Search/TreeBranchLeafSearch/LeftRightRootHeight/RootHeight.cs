using System;
using System.Collections.Generic;

namespace LeftRightRootHeight
{
    /// <summary>
    /// Find the height of a tree
    /// </summary>
    class RootHeight
    {
        /// <summary>
        /// Main - start point
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            // Create inputs
            Node leaf1 = new Node(null, null);
            Node leaf2 = new Node(null, null);
            Node branch = new Node(leaf1, null);
            Node root = new Node(branch, leaf2);

            // Test algorithm
            Console.WriteLine($"Height of a Tree of branch and leaf: {root.Height()}");
            Console.WriteLine($"Height of a branch of 1 leaf: {branch.Height()}");
            Console.WriteLine($"Height of a leaf: {leaf1.Height()}");

            // end
            Console.WriteLine("end!");
            Console.ReadLine();
        }
    }

    /// <summary>
    /// Create a Node object that can only have 1 parent and 2 child: left and right
    /// </summary>
    public class Node
    {
        // Left and right children
        public Node LeftChild { get; private set; }
        public Node RightChild { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="leftChild"></param>
        /// <param name="rightChild"></param>
        public Node(Node leftChild, Node rightChild)
        {
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }

        /// <summary>
        /// Search all paths
        /// </summary>
        /// <param name="pathNodes"></param>
        /// <param name="node"></param>
        /// <param name="pathHeight"></param>
        /// <returns></returns>
        private List<Node> GetPaths(List<Node> pathNodes,
            Node node, ref int pathHeight)
        {
            // if we are done in this path
            if (node == null)
            {
                return pathNodes;
            }

            // search this node
            pathHeight++;
            pathNodes.Add(node);

            // if this is a leaf, no further paths
            if (node.LeftChild == null &&
                node.RightChild == null)
            {
                pathHeight--;
                pathNodes.Remove(node);
                return pathNodes;
            }

            // search left and right
            pathNodes = GetPaths(pathNodes, node.LeftChild, ref pathHeight);
            pathNodes = GetPaths(pathNodes, node.RightChild, ref pathHeight);

            // we are done searching this particular node
            pathNodes.Remove(node);

            return pathNodes;
        }

        /// <summary>
        /// Find the height of a tree
        /// </summary>
        /// <returns></returns>
        public int Height()
        {
            // Initiate the height
            int height = 0;

            // search all paths
            GetPaths(new List<Node>(), this, ref height);

            return height;
        }
    }
}
