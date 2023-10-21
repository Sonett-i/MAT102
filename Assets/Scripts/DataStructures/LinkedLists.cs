public class LinkedLists
{
	public class Node<T>
	{
		public T Value;
		public Node<T> Next;

		public Node(T value)
		{
			this.Value = value;
			this.Next = null;
		}
	}

	public class LinkedList<T>
	{
		public Node<T> Head;

		public void AddFirst(T value)
		{
			Node<T> newNode = new Node<T>(value);
			newNode.Next = Head;
			Head = newNode;
		}

		public void AddLast(T value)
		{
			Node<T> newNode = new Node<T>(value);
			if (Head == null)
			{
				Head = newNode;
			}
			else
			{
				Node<T> current = Head;
				while (current.Next != null)
				{
					current = current.Next;
				}
				current.Next = newNode;
			}
		}

		public string Print()
		{
			string output = string.Empty;

			Node<T> current = Head;

			while(current != null)
			{
				output += current.Value + "\n";
				current = current.Next;
			}

			return output;
		}
	}

	public class Stack
	{

	}
}
