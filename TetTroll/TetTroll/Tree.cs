using System.Collections.Generic;
using System.Linq;

namespace TetTroll
{
	public class Tree<T>
	{
		public Tree(T value)
		{
			Root = new TreeNode<T>(value) {Root = this };
		}

		public TreeNode<T> Root { get; }
		public List<TreeNode<T>> Leafs { get; } =  new List<TreeNode<T>>();
	}
	public class TreeNode<T>
	{
		public TreeNode(T value)
		{
			Value = value;
		}

		public TreeNode<T> AddChild(T value, bool isLeaf = false)
		{
			var node = new TreeNode<T>(value) { Parent = this, Root = this.Root };
			Children.Add(node);
			if (isLeaf)
				Root.Leafs.Add(node);
			return node;
		}

		public TreeNode<T> Parent { get; private set; }
		public Tree<T> Root { get; set; }
		public T Value { get; }
		public List<TreeNode<T>> Children { get; } = new List<TreeNode<T>>();
	}
}
