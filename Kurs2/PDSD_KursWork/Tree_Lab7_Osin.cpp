#include "Tree_Lab7_Osin.h"

BinarySearchTree::BinarySearchTree() {
    bst = nullptr;
}

bool BinarySearchTree::IsEmptyTree() {
    return bst == nullptr;
}

bool BinarySearchTree::InsertNewData(int new_elem) {
    if (bst == nullptr) {
        bst = new BST(new_elem);
        return true;
    }
    BST* current = bst;
    while (current) {
        if (new_elem == current->data) {
            return false;
        }
        if (new_elem < current->data) {
            if (current->left == nullptr) {
                current->left = new BST(new_elem);
                return true;
            }
            current = current->left;
        }
        else {
            if (current->right == nullptr) {
                current->right = new BST(new_elem);
                return true;
            }
            current = current->right;
        }
    }
    return false;
}

void BinarySearchTree::createTree(int min_elem, int max_elem, int count_elements) {
    int size = 0;
	while (size < count_elements) {
		int random_element = rand() % (max_elem - min_elem + 1) + min_elem;
        if (InsertNewData(random_element))
            size++;
	}
}

string BinarySearchTree::AvoidfromToptoButtom() {
    string result;
    if (bst == nullptr) return result;
    BST* current = bst;
    while (current) {
        BST* left = current->left;
        if (left) {
            while (left->right && left->right != current) {
                left = left->right;
            }
            if (left->right == nullptr) {
                result += to_string(current->data) + " ";
                left->right = current;
                current = current->left;
            }
            else {
                left->right = nullptr;
                current = current->right;
            }
        }
        else {
            result += to_string(current->data) + " ";
            current = current->right;
        }
    }
    return result;
}

string BinarySearchTree::AvoidLeftToRight() {
    string result;
    BST* current = bst;
    while (current) {
        if (current->left == nullptr) {
            result += to_string(current->data) + " ";
            current = current->right;
        }
        else {
            BST* predecessor = current->left;
            while (predecessor->right && predecessor->right != current) {
                predecessor = predecessor->right;
            }

            if (predecessor->right == nullptr) {
                predecessor->right = current;
                current = current->left;
            }
            else {
                predecessor->right = nullptr;
                result += to_string(current->data) + " ";
                current = current->right;
            }
        }
    }
    return result;
}

string BinarySearchTree::AvoidButtomtoTop() {
    string result;
    BST* dummy = new BST(0);
    dummy->left = bst;
    BST* current = dummy;
    while (current) {
        if (current->left == nullptr) {
            current = current->right;
        }
        else {
            BST* predecessor = current->left;
            while (predecessor->right && predecessor->right != current) {
                predecessor = predecessor->right;
            }

            if (predecessor->right == nullptr) {
                predecessor->right = current;
                current = current->left;
            }
            else {
                BST* node = current->left;
                string temp;
                while (node != predecessor) {
                    temp = to_string(node->data) + " " + temp;
                    node = node->right;
                }
                temp = to_string(predecessor->data) + " " + temp;
                result += temp;

                predecessor->right = nullptr;
                current = current->right;
            }
        }
    }
    return result;
}

void BinarySearchTree::DeleteTree() {
    BST* current = bst;
    while (current != nullptr) {
        if (current->left != nullptr) {
            BST* predecessor = current->left;
            while (predecessor->right != nullptr) {
                predecessor = predecessor->right;
            }
            predecessor->right = current->right;
            BST* temp = current;
            current = current->left;
            delete temp;
        }
        else {
            BST* temp = current;
            current = current->right;
            delete temp;
        }
    }
    bst = nullptr;
}
