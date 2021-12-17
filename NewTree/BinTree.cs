using System;

namespace NewTree
{
    public class BinTree<T> where T : IComparable
    {
        public BinTreeNode<T> RootNode { get; set; }
        //объявление события
        public delegate void CollHandler(string message);
        public event CollHandler Notify;

        public BinTreeNode<T> Add(BinTreeNode<T> Node, BinTreeNode<T> CurrentNode = null)
        {
            if (RootNode == null)
            {
                Node.ParentNode = null;
                return RootNode = Node;
            }


            CurrentNode = CurrentNode ?? RootNode;
            Node.ParentNode = CurrentNode;
            int result;
            return (result = Node.Data.CompareTo(CurrentNode.Data)) == 0
                ? null
                : result < 0
                    ? CurrentNode.LeftNode == null
                        ? CurrentNode.LeftNode = Node
                        : Add(Node, CurrentNode.LeftNode)
                    : CurrentNode.RightNode == null
                        ? CurrentNode.RightNode = Node
                        : Add(Node, CurrentNode.RightNode);

        }
        //Перегрузка метода добавления для удобного вызова
        public BinTreeNode<T> Add(T Data)
        {
            BinTreeNode<T> NewNode = Add(new BinTreeNode<T>(Data));
            if (NewNode != null)
            {
                Notify?.Invoke($"{Data} has been Added");
            }
            return NewNode;
        }

        public BinTreeNode<T> FindNode(T Data, BinTreeNode<T> Temp = null)
        {
            Temp = Temp ?? RootNode;
            int resalt;

            return (resalt = Data.CompareTo(Temp.Data)) == 0
                ? Temp
                : resalt < 0
                    ? Temp.LeftNode == null
                        ? null
                        : FindNode(Data, Temp.LeftNode)
                    : Temp.RightNode == null
                        ? null
                        : FindNode(Data, Temp.RightNode);
        }

        public void Remove(BinTreeNode<T> Node)
        {
            if (Node == null)
            {
                return;
            }

            var CurrentNode = Node.NodeSide;

            if (Node.LeftNode == null && Node.RightNode == null)
            {
                if (CurrentNode == Side.Left)
                {
                    Node.ParentNode.LeftNode = null;
                    return;
                }
                else
                {
                    Node.ParentNode.RightNode = null;
                    return;
                }
            }

            else if (Node.LeftNode == null)
            {
                if (CurrentNode == Side.Left)
                {
                    Node.ParentNode.LeftNode = Node.RightNode;
                }
                else
                {
                    Node.ParentNode.RightNode = Node.RightNode;
                }
                Node.RightNode.ParentNode = Node.ParentNode;
            }

            else if (Node.RightNode == null)
            {
                if (CurrentNode == Side.Left)
                {
                    Node.ParentNode.LeftNode = Node.LeftNode;
                }
                else
                {
                    Node.ParentNode.RightNode = Node.LeftNode;
                }
                Node.LeftNode.ParentNode = Node.ParentNode;
            }

            else
            {
                switch (CurrentNode)
                {
                    case Side.Left:
                        {
                            Node.ParentNode.LeftNode = Node.RightNode;
                            Node.RightNode.ParentNode = Node.ParentNode;
                            Add(Node.LeftNode, Node.RightNode);
                            break;
                        }

                    case Side.Right:
                        {
                            Node.ParentNode.RightNode = Node.RightNode;
                            Node.RightNode.ParentNode = Node.RightNode;
                            Add(Node.LeftNode, Node.RightNode);
                            break;
                        }
                    default:
                        {
                            var bufLeft = Node.LeftNode;
                            var bufRightLeft = Node.RightNode.LeftNode;
                            var bufRightRight = Node.RightNode.RightNode;
                            Node.Data = Node.RightNode.Data;
                            Node.RightNode = bufRightRight;
                            Node.LeftNode = bufRightLeft;
                            Add(bufLeft, Node);
                            break;
                        }
                }
            }
        }
        //Перегрузка метода удаления для удобного вызова
        public void Remove(T Data)
        {
            var Node = FindNode(Data);
            if (Node != null)
            {
                Notify?.Invoke($"{Node.Data} has been removeded");
                Remove(Node);
            }
        }

        public void PrintTree()
        {
            if (RootNode != null)
            {
                Console.Write($" {RootNode.Data} ");
                PrintLeftTree(RootNode.LeftNode);
                Console.WriteLine();
                PrintRightTree(RootNode.RightNode);
            }

        }

        //выводит левую ветку в строку
        private void PrintLeftTree(BinTreeNode<T> startNode)
        {
            if (startNode != null)
            {
                Console.Write($" {startNode.Data} ");

                PrintLeftTree(startNode.LeftNode);
                PrintLeftTree(startNode.RightNode);
            }
        }
        //выводит правую ветку в столбик
        private void PrintRightTree(BinTreeNode<T> startNode)
        {
            if (startNode != null)
            {
                Console.WriteLine($" {startNode.Data}");

                PrintRightTree(startNode.LeftNode);
                PrintRightTree(startNode.RightNode);
            }
        }

    }
}
