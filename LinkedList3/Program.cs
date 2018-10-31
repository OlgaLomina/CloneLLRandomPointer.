using System;
//3. Clone a linked list with a random pointer.
namespace LinkedList3
{
    public class Node
    {
        public Node next;
        public Node r;
        public Object data;

        public Node()
        {
            next = null;
        }
        public Node(Object d)
        {
            data = d;
            next = null;
        }
    }

 
    public class MyLList
    {
        Node head;

        public MyLList()
        {
            head = null;
        }

        public void PrintAllNodes(Node head)
        {
            Node cur = head;
            Object str;
            while (cur.next != null)
            {
                str = cur.r.data;
                if (str == null) str = "";                    
                Console.WriteLine(cur.data + "    Random = " + str);
                cur = cur.next;
            }
            str = cur.r.data;
            if (str == null) str = "";
            Console.WriteLine(cur.data + "    Random = " + str);
            Console.WriteLine();
        }

        public void AddHead(char d)
        {
            Node node = new Node(d);
            node.next = head;
            head = node;
        }

        public void AddAtTail(Object d)
        {
            Node node = new Node(d);
            if (head == null)
            {
                head = node;
            }
            else
            {
                Node current = head;
                while (current.next != null)
                {
                    current = current.next;
                }
                current.next = node;
            }
        }

        public void CloneLinkedList()
        {
            PrintAllNodes(head);

            /* 1) Create a copy of node1 and insert it between node1 & node2 in original Linked List, 
             *    create a copy 2 and insert it between 2 & 3 .. keep going.... add a copy N after each node
               2) So, copy a random pointer in this way:
                  orig.next.r = orig.t.next; //TRAVERSE TWO NODES
                            orig.next - it is a copy of original, 
                            а orig.r.next - it is a random copy. 
                3) Now separate the original list and copied list 
                orig.next = orig.next.next;
                copy.next = copy.next.next;
                4) the last node orig.next should be equal NULL.

                Time complexity: О(п)
                Additoinal Space: O(1)
             * */
            // make holes after each original node 
            Node orig = head, temp, copy;
            while (orig != null)
            {
                temp = orig.next;
                // Inserting node 
                copy = new Node(orig.data);
                orig.next = copy;
                copy.next = temp;
                orig = temp;
            }

            orig = head;
            // adjust the random pointers of the newly added nodes 
            while (orig != null)
            {
                orig.next.r = orig.r.next;
                // move to the next newly added node by skipping an original node 
                //orig = (orig.next.next != null) ? orig.next.next : null;
                orig = orig.next.next ?? null;
            }

            orig = head;
            copy = head.next;
            // save the start of copied linked list 
            temp = copy;

            // now separate the original list and copied list 
            while (orig != null && copy != null)
            {
                orig.next = orig.next.next ?? null;
                copy.next = copy.next?.next;
                //copy.next = (copy.next != null) ? copy.next.next : null;
                //orig.next = orig.next.next;
                //copy.next = copy.next.next;
                orig = orig.next;
                copy = copy.next;
            }
            PrintAllNodes(temp); // clone
    }

        class Program
        {
            static void Main(string[] args)
            {
                MyLList list = new MyLList();

                list.AddAtTail(1);
                list.AddAtTail(2);
                list.AddAtTail(3);
                list.AddAtTail(4);

                Node node = list.head;
                string par = null;
                while (node != null)
                {
                    par = node.data.ToString();
                    switch (par)
                    {
                        case "1":
                            node.r = node.next.next; //1 -> 3
                            break;
                        case "2":
                            node.r = list.head; //2 -> 1
                            break;
                        case "3":
                            node.r = node.next; //3 -> 4
                            break;
                        case "4":
                            node.r = list.head.next; //4 -> 2
                            break;
                        default:
                            break;
                    }
                    node = node.next;
                }
                //PrintLL(list.head);
                list.CloneLinkedList();
            }

            public static void PrintLL(Node head)
            {
                Node cur = head;
                while (cur.next != null)
                {
                    Console.WriteLine(cur.data);
                    cur = cur.next;
                }
                Console.WriteLine(cur.data);
            }

        }
    }
}
