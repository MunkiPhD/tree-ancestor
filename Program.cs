using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TreeAncestor {
    class Program {
        static void Main(string[] args) {
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

            foreach(string value in xPath){
                foreach(string yValue in yPath) {
                    if(value == yValue)
                        return value;
                }
            }

            return null;
        }


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
        /// <param name="tree"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private Node FindNode(Node node, string value) {
            if(node.Value == value)
                return node;

            foreach(Node child in node.Children) {
                Node returned = FindNode(child, value);
                if(returned != null)
                    return returned;
            }
            //for(int i = 0; i < node.Children.Count - 1; i++) {
            //    Node childNode = node.Children.ElementAt(i);
            //    Node returned = FindNode(childNode, value);
            //    if(returned != null)
            //        return returned;
            //}

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
        /// <param name="node"></param>
        public void AddChild(Node node) {
            node.Parent = this;
            this.Children.Add(node);
        }
    }
}
