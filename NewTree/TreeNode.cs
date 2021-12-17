using System;

namespace NewTree
{
    public enum Side
    {
        Left,
        Right
    }
    public class BinTreeNode<T> where T : IComparable
    {
        public BinTreeNode(T Data)
        {
            this.Data = Data;
        }

        public T Data { get; set; }

        public BinTreeNode<T> LeftNode { get; set; }

        public BinTreeNode<T> RightNode { get; set; }

        public BinTreeNode<T> ParentNode { get; set; }

        public Side? NodeSide =>
        ParentNode == null ? (Side?)null : ParentNode.LeftNode == this ? Side.Left : Side.Right;


    }
}
