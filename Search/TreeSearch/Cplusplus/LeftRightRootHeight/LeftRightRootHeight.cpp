// LeftRightRootHeight.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <algorithm>
#include <iostream>
#include <vector>

// Create a Node object that can only have 1 parent and 2 child: 
// left and right
class Node
{
public:
	// Left and Right children
	Node* LeftChild;
	Node* RightChild;

	// Constructor
	Node(Node* leftChild, Node* rightChild);
	// deconstructor
	~Node();

	// Find the height of a tree
	int Height();

private:
	// destroy all children nodes
	void destroy_node(Node* node);
	// Search all paths
	std::vector<Node*> getPaths(
		std::vector<Node*> pathNodes,
		Node* node,
		int& pathHeight
	);
};

// Constructor
Node::Node(Node* leftChild, Node* rightChild)
{
	Node::LeftChild = leftChild;
	Node::RightChild = rightChild;
}

// Deconstructor
Node::~Node()
{
	destroy_node(this);
}

// Destroy all children nodes
void Node::destroy_node(Node* node)
{
	if (node != NULL)
	{
		destroy_node(node->LeftChild);
		destroy_node(node->RightChild);
		delete node;
	}
}

// Search all paths
std::vector<struct Node*> Node::getPaths(
	std::vector<struct Node*> pathNodes,
	struct Node* node,
	int& pathHeight)
{
	// if we are done in this path
	if (node == NULL)
	{
		return pathNodes;
	}

	// search this node
	pathHeight++;
	pathNodes.push_back(node);

	// if this is a leaf, no further paths
	if (node->LeftChild == NULL &&
		node->RightChild == NULL)
	{
		pathHeight--;
		pathNodes.erase(
			std::remove(
				pathNodes.begin(),
				pathNodes.end(),
				node),
			pathNodes.end());
	}

	// search left and right
	pathNodes = getPaths(
		pathNodes,
		node->LeftChild,
		pathHeight
	);
	pathNodes = getPaths(
		pathNodes,
		node->RightChild,
		pathHeight
	);

	//we are done searching this particular node
	pathNodes.erase(
		std::remove(
			pathNodes.begin(),
			pathNodes.end(),
			node),
		pathNodes.end());

	return pathNodes;
}

// Find the Height of a tree
int Node::Height()
{
	// initiate the height
	int height = 0;

	// search all paths
	std::vector<Node*> path = std::vector<Node*>();
	getPaths(path, this, height);

	return height;
}

// main - start point
int main()
{
	// Create inputs
	Node* leaf1 = new Node(NULL, NULL);
	Node* branch = new Node(leaf1, NULL);
	Node* leaf2 = new Node(NULL, NULL);
	Node* root = new Node(branch, leaf2);
	
	// test algorithm
    std::cout << "Height of a leaf: " 
		<< leaf1->Height() << "\n";
	std::cout << "Height of a branch: " 
		<< branch->Height() << "\n";
	std::cout << "Height of a tree of branch and leaf: "
		<< root->Height() << "\n";
}