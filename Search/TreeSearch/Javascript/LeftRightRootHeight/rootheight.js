// Create a Node object that can only have 1 parent and 2 child: 
// left and right
class Node
{
    // Constructor
    // define Left and Right children 
    constructor(leftChild, rightChild)
    {
        this.LeftChild = leftChild;
        this.RightChild = rightChild;
    }

    // search all paths
    getPaths(pathNodes, node, pathHeight)
    {
        // if we are done in this path
        if (node == undefined)
        {
            return pathNodes;
        }

        // search this node
        pathHeight.value++;
        pathNodes.push(node);

        // if this is a leaf, no further paths
        if (node.LeftChild == undefined &&
            node.RightChild == undefined)
            {
                pathHeight.value--;
                pathNodes = pathNodes.filter(n => n != node);
                return pathNodes;
            }

        // search left and right
        pathNodes = this.getPaths(pathNodes,
            node.LeftChild, pathHeight);
        pathNodes = this.getPaths(pathNodes,
            node.RightChild, pathHeight);

        // we are done searching this particular node
        pathNodes = pathNodes.filter(n => n != node);

        return pathNodes;
    }

    // Find the height of a tree
    height()
    {
        // Initiate the height
        let path_Height = { value: 0 };

        let nodes = [];

        // search all paths
        this.getPaths(nodes, this, path_Height);

        return path_Height.value;
    }
}

// Main - start point
function main () {
    // create inputs
    leaf1 = new Node(null, null);
    branch = new Node(leaf1, null);
    leaf2 = new Node(null, null);
    root = new Node(branch, leaf2);

    // test algorithm
    console.log("Height of a leaf : " + 
        leaf1.height());
    console.log("Height of a branch of 1 leaf : " + 
        branch.height());
    console.log("Height of a tree of 1 branch and 1 leaf : " + 
        root.height());
}

main()