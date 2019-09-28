// MultiChildRootSearch.cpp : This file contains the 'main' function. Program execution begins and ends there.
//

#include <algorithm>
#include <iostream>
#include <vector>

// Create a Node object that can only have 1 parent 
// and multiple children
class Node
{
public:
	// children
	std::vector<Node*> Children;
	
	// Constructor
	Node(std::vector<Node*> children);
	// Deconstructor
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
Node::Node(std::vector<Node*> children)
{
	Node::Children = children;
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
		for (auto n : node->Children)
		{
			destroy_node(n);
		}
		delete node;
	}
}

// Search all paths
std::vector<Node*> Node::getPaths(
	std::vector<Node*> pathNodes,
	Node* node,
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
	if (node->Children.empty())
	{
		pathHeight--;
		pathNodes.erase(
			std::remove(
				pathNodes.begin(),
				pathNodes.end(),
				node),
			pathNodes.end());
	}

	// search every child
	for (auto n : node->Children)
	{
		pathNodes = getPaths(
			pathNodes,
			n,
			pathHeight
		);
	}

	// we are done searching this particular node
	pathNodes.erase(
		std::remove(
			pathNodes.begin(),
			pathNodes.end(),
			node),
		pathNodes.end()
	);

	return pathNodes;
}

// find the height of a tree
int Node::Height()
{
	// Initiate the height
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
	Node* leaf1 = new Node(std::vector<Node*>());
	std::vector<Node*> children1 = std::vector<Node*>();
	children1.push_back(leaf1);
	Node* branch1 = new Node(children1);
	Node* leaf2 = new Node(std::vector<Node*>());
	std::vector<Node*> children2 = std::vector<Node*>();
	children2.push_back(branch1);
	children2.push_back(leaf2);
	Node* root1 = new Node(children2);
	std::vector<Node*> children3 = std::vector<Node*>();
	children3.push_back(leaf2);
	Node* branch2 = new Node(children3);
	std::vector<Node*> children4 = std::vector<Node*>();
	children4.push_back(leaf1);
	children4.push_back(branch2);	
	Node* root2 = new Node(children4);

	// Test the algorithm 
	std::cout << "Height of a leaf: "
		<< leaf1->Height() << "\n";
	std::cout << "Height of a branch: "
		<< branch1->Height() << "\n";
	std::cout << "Height of a tree of branch and leaf: "
		<< root1->Height() << "\n";

	std::cout << "Height of a leaf: "
		<< leaf2->Height() << "\n";
	std::cout << "Height of a branch: "
		<< branch2->Height() << "\n";
	std::cout << "Height of a tree of branch and leaf: "
		<< root2->Height() << "\n";
}