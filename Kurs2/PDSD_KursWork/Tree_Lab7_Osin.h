#pragma once
#include <string>
using std::string;
using std::to_string;
struct BST {
	int data;
	BST* left;
	BST* right;
	BST(int value) : data(value), left(nullptr), right(nullptr) {}
};

class BinarySearchTree {
private:
	BST* bst;
public:
	BinarySearchTree();
	bool IsEmptyTree();
	bool InsertNewData(int new_elem);
	void createTree(int min_elem, int max_elem, int count_elements);
	string AvoidfromToptoButtom();
	string AvoidLeftToRight();
	string AvoidButtomtoTop();
	void DeleteTree();
};