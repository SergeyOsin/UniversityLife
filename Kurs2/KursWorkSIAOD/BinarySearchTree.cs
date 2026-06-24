using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    internal class BinarySearchTree
    {
        private int data;
        private BinarySearchTree left;
        private BinarySearchTree right;
        public BinarySearchTree (int data)
        {
            this.data = data;
            this.left = null;
            this.right = null;
        }
        public BinarySearchTree Insert(BinarySearchTree node, int value)
        {
            if (node == null)
            {
                return new BinarySearchTree(value);
            }
            if (value < node.data)
            {
                node.left = Insert(node.left, value); 
            }
            else if (value > node.data)
            {
                node.right = Insert(node.right, value); 
            }
            return node;
        }
        
        
        public BinarySearchTree BuildTree(int[] array)
        {
            if (array.Length == 0) return null;
            BinarySearchTree bst = new BinarySearchTree(array[0]);
            for (int i = 1; i < array.Length; i++)            
               Insert(bst, array[i]);
            return bst;
        }
        public void InorderTraversal(BinarySearchTree node, List<int>list)
        {
            if (node != null)
            {
               InorderTraversal(node.left, list);
                list.Add(node.data);
               InorderTraversal(node.right, list);
            }
        }
        public List<int> SortArrayUsingBST(int[]array)
        {
            if (array.Length == 0)
                return new List<int>();
            BinarySearchTree bst = new BinarySearchTree(array[0]);
            bst=bst.BuildTree(array);
            List<int> list0 = new List<int>();
            bst.InorderTraversal(bst, list0);
            return list0;
        }
    }
}
