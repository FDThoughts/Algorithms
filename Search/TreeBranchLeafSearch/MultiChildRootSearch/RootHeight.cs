using System;
using System.Collections.Generic;

namespace MultiChildRootSearch
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
            Node leaf1 = new Node(null);
            Node leaf2 = new Node(null);
            Node branch = new Node(new List<Node> { leaf1 });
            Node root = new Node(new List<Node> { branch, leaf2 });
            Node branch2 = new Node(new List<Node> { leaf2 });
            Node root2 = new Node(new List<Node> { leaf1, branch2 });

            // Test the algorithm
            Console.WriteLine($"Height of a Tree1 of branch and leaf: {root.Height()}");
            Console.WriteLine($"Height of a branch1 of 1 leaf: {branch.Height()}");
            Console.WriteLine($"Height of a leaf1: {leaf1.Height()}");

            Console.WriteLine($"Height of a Tree2 of a leaf and branch: {root2.Height()}");
            Console.WriteLine($"Height of a branch2 of 1 leaf: {branch2.Height()}");
            Console.WriteLine($"Height of a leaf2: {leaf2.Height()}");

            // end
            Console.WriteLine("end!");
            Console.ReadLine();
        }
    }

    /// <summary>
    /// Create a Node object that can only have 1 parent and multiple children
    /// </summary>
    public class Node
    {
        // children
        public List<Node> Childern { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="children"></param>
        public Node(List<Node> children)
        {
            this.Childern = children;
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
            if (node.Childern == null ||
                node.Childern.Count == 0)
            {
                pathHeight--;
                pathNodes.Remove(node);
                return pathNodes;
            }

            // search every child
            foreach (var child in node.Childern)
            {
                pathNodes = GetPaths(pathNodes, child, ref pathHeight);
            }

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
