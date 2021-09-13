using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieService.Models;

namespace MovieService
{
    class Node
    {
        internal int key;
        internal Movie value;
        internal Node next;
        internal Node prev;
        public Node(int k=-1,Movie v=null)
        {
            key = k;
            value = v;
        }
    }

    class LinkedList
    {
        Node head;
        Node tail;
        int count;
        public LinkedList()
        {
            head = new Node();
            tail = new Node();

            head.next = tail;
            tail.prev = head;
            count = 0;
        }
        public Node front()
        {
            return head.next;
        }
        Node last()
        {
            return tail.prev;
        }

        void insert_between(Node prev,Node new_node,Node next)
        {
            prev.next = new_node;
            next.prev = new_node;

            new_node.next = next;
            new_node.prev = prev;
            ++count;
        }

        public void push_front(Node new_node)
        {
            insert_between(this.head, new_node, this.head.next);
        }

        public Node remove(Node node)
        {
            node.prev.next = node.next;
            node.next.prev = node.prev;
            --count;
            return node;
        }

        public Node remove_last()
        {
            return remove(last());
        }
    }

    public class LRUcache
    {
        private int size;
        LinkedList linkedList;
        IDictionary<int, Node> dicc;

        public LRUcache(int n)
        {
            size = n;
            linkedList = new LinkedList();
            dicc = new Dictionary<int, Node>();
        }

        public void push(int key, Movie value)
        {
            var new_node = new Node(key, value);
            if (dicc.Count < size)
            {
                linkedList.push_front(new_node);
                dicc[key] = new_node;
            }
            else
            {
                linkedList.push_front(new_node);
                var last = linkedList.remove_last();
                dicc[key] = new_node;
                dicc.Remove(last.key);
            }
        }

        public int get_most_recent_key()
        {
            if (linkedList.front() != null)
                return linkedList.front().key;
            return -1;
        }

        public Movie get_value_from_key(int key)
        {
            if (!dicc.ContainsKey(key))
                return null;
            var old_node = dicc[key];
            linkedList.remove(old_node);
            var new_node = new Node(old_node.key, old_node.value);
            linkedList.push_front(new_node);
            return new_node.value;
        }
    }
}