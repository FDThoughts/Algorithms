// Create a Node object that can only have 1 parent 
// and multiple children
class Node
{
    // Constructor
    // define children nodes
    constructor(children)
    {
        this.Chidren = children;
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
        if (node.Chidren == undefined ||
            node.Chidren.length == 0)
            {
                pathHeight.value--;
                pathNodes = pathNodes.filter(n => n != node);
                return pathNodes;
            }

        // search every child
        Array.prototype.forEach.call(node.Chidren, 
            child => {
            pathNodes = this.getPaths(
                pathNodes,
                child,
                pathHeight
            );
        });

        // we are done searching this particular node
        pathNodes = pathNodes.filter(n => n != node);

        return pathNodes;
    }

    // Find the height od a tree
    height ()
    {
        // Initiate the height
        let path_Height = { value: 0 };

        // search all paths
        let nodes = [];
        this.getPaths(nodes, this, path_Height);

        return path_Height.value;
    }
}

// main - start point
function main () {
    // Create inputes
    leaf1 = new Node(null);
    branch1 = new Node([leaf1]);
    leaf2 = new Node(null);
    root1 = new Node([branch1, leaf2]);
    branch2 = new Node([leaf2]);
    root2 = new Node([leaf1, branch2]);

    // Test algorithm
    console.log("Height of a leaf: " +
        leaf1.height());
    console.log("Height of a branch of 1 leaf : " + 
        branch1.height());
    console.log("Height of a tree of 1 branch and 1 leaf : " + 
        root1.height());

    console.log("Height of a leaf: " +
        leaf2.height());
    console.log("Height of a branch of 1 leaf : " + 
        branch2.height());
    console.log("Height of a tree of 1 branch and 1 leaf : " + 
        root2.height());
}

main()