using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreeAncestor {
    class Program {
        static void Main(string[] args) {
            // Building the following tree structure
            /*
             *                  a
             *                 / \
             *                /   \
             *               /     \
             *              b       c
             *             / \     / \
             *            d   e   f   g
             *              / | \ 
             *             h  i  j
             * 
             */
            Node h = new Node("h");
            Node i = new Node("i");
            Node j = new Node("j");

            Node e = new Node("e");
            e.AddChild(h);
            e.AddChild(i);
            e.AddChild(j);
        
            Node d = new Node("d");

            Node b = new Node("b");
            b.AddChild(d);
            b.AddChild(e);

            Node f = new Node("f");
            Node g = new Node("g");

            Node c = new Node("c");
            c.AddChild(f);
            c.AddChild(g);
            
            Node tree = new Node("a");
            tree.AddChild(b);
            tree.AddChild(c);

            Finder ancestorFinder = new Finder();

            // find the lowest common ancestor between nodes "d" and "j"
            String result = ancestorFinder.FindAncestor(tree, "d", "j");
            if(result != "b")
                Console.WriteLine("For d and j, it DID not find b :(");
            else
                Console.WriteLine("For d and j, it found b!");

            Console.ReadLine();
        }
    }



    class Finder {
        public string FindAncestor(Node tree, string x, string y) {
            if(tree == null)
                throw new NullReferenceException("Tree is null");

            if(x == y)
                throw new Exception("Values are the same");

            Node xNode = FindNode(tree, x);
            Node yNode = FindNode(tree, y);

            var xPath = CreateNodePath(xNode);
            var yPath = CreateNodePath(yNode);

            // since we want to find the first occurence, worst case is O(m*n) which can be O(n*n) or O(n^2). 
            // Best case it could be O(1) where we have a match on the first node of each array, e.g. the root node
            foreach(string value in xPath){
                foreach(string yValue in yPath) {
                    if(value == yValue)
                        return value;
                }
            }

            return null;
        }


        /// <summary>
        /// Create the path from the found node up to the root
        /// </summary>
        /// <param name="node">The node that we want to find the path from</param>
        /// <returns>A list of strings (the values of the nodes)</returns>
        private List<String> CreateNodePath(Node node) {
            List<String> pathList = new List<String>();
            Node currentNode = node;
            while(currentNode.Parent != null) {
                currentNode = currentNode.Parent;
                pathList.Add(currentNode.Value);
            }

            return pathList;
        }

        /// <summary>
        /// Finds the specified node equal to the value
        /// </summary>
        /// <param name="node">The node we are currently seeking through</param>
        /// <param name="value">The value we are searching for</param>
        /// <returns>The node that has the same value or null</returns>
        private Node FindNode(Node node, string value) {
            if(node.Value == value)
                return node;

            // search all the children and try to find the node. If it returns null, then we hit a leaf
            // if it DOESNT return null, then we found the node we want, so shoot it up the stack
            // note: this could cause overflow errors for a huge tree since we're using recursion
            foreach(Node child in node.Children) {
                Node returned = FindNode(child, value);
                if(returned != null)
                    return returned;
            }

            return null;
        }
    }


    class Node {
        public string Value { get; set; }
        public Node Parent { get; set; }
        public List<Node> Children { get; set; }
        
        public Node(string value) {
            this.Value = value;
            this.Parent = null;
            this.Children = new List<Node>();
        }

        /// <summary>
        /// Adds a child to the children of this node
        /// </summary>
        /// <param name="node">The node to add to the children of this node</param>
        public void AddChild(Node node) {
            node.Parent = this; // assign the parent since it's now a child of this node
            this.Children.Add(node); // and add it to the list of children
        }
    }
}
