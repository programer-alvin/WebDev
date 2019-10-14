using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NodeOperations
{
    
    static public class LinkedListHelpers
    {
        public class Node
        {
            static int same_node_count = 0;
            static List<char> chars = new List<char>();
            public char value;//holds characters
            public Node next;//holds the next node to be attached
            public Node(char value)
            {
                if (!isMatching(value))//ensures no unwanted character is accepted
                {
                    throw new ArgumentException("Make sure only Range of A to Z is entered");
                }
                this.value = value;//set char value
                next = null;
            }
            public void print()
            {
                Console.Write("|" + value + "||->");
                if (next != null)
                {
                    next.print();//print value of next node
                }
                else
                {
                    Console.WriteLine();
                }
            }

            public void deleteTwoSameConcecutive(char root)//delete 
            {
                if (!isMatching(root))//ensures no unwanted character is accepted
                {
                    throw new ArgumentException("Make sure only Range of A to Z is entered");
                }
                if (next != null)//not empty list or single node or end of list
                {
                    if (value == next.value)//check if the value of the current node is same as value of the next node
                    {
                        next = next.next;//if same delete the next node
                                         //The inaccessable node will be collected by garbage collector
                        next.deleteTwoSameConcecutive(root);//keep going forward to the next nodes
                    }
                    else
                    {
                        next.deleteTwoSameConcecutive(root);//keep going forward to the next nodes
                    }
                }
            }

            public void deleteMoreThanTwoSameNode(char root)//delete 
            {
                if (!isMatching(root))//ensures no unwanted character is accepted
                {
                    throw new ArgumentException("Make sure only Range of A to Z is entered");
                }
                if (next != null && next.next != null)//not empty list or single node or end of list
                {
                    if (this.value == root)//check if the current node value is same as passed value
                    {
                        same_node_count++;//if same increment count value


                    }
                    if (next.next.value == root)//check if the next is value is the same to increment again
                    {
                        same_node_count++;//if the next same increment count value
                        if (same_node_count > 2)//if count is greater than two
                        {
                            next.next = next.next.next;//delete current node
                                                       //The inaccessable node will be collected by garbage collector
                            //Console.WriteLine("delete->" + root);//prints element to be deleted
                        }

                        next.deleteMoreThanTwoSameNode(root);//keep going forward to the next nodes
                    }
                    else
                    {
                        next.deleteMoreThanTwoSameNode(root);//keep going forward to the next nodes
                    }
                }
            }

            public void deleteAllMMoreThanTwoSame(char root)
            {
                if (!isMatching(root))//ensures no unwanted character is accepted
                {
                    throw new ArgumentException("Make sure only Range of A to Z is entered");
                }
                    if (root == '\0' || root == default(Char))
                {
                    throw new ArgumentException("null or empty char is not accepted");
                }
                if (value != root)
                {
                    throw new ArgumentException(root + " is not the root.");//element entered does not match the root
                }
                List<char> chars = getNodesValues();
                for (int i = 0; i < chars.Count; i++)
                {
                    same_node_count = 0;//reset the counter before starting deletion
                    deleteMoreThanTwoSameNode(chars[i]); // analyze one by one
                }
            }

            public List<char> getNodesValues()
            {
                
                if (next != null)
                {
                    bool char_exists = false;
                    for (int i = 0; i < chars.Count; i++)
                    {
                        if (chars[i] == value)
                        {
                            char_exists = true;
                            break;//break the for loop to save CPU cycles for better performance
                        }
                    }
                    if (!char_exists)
                    {
                        chars.Add(value);
                    }
                    next.getNodesValues();
                }
                return chars;
            }

            bool isMatching(char char_to_check) //check if char ranges frm A to Z
            {
                List<string> matches = new List<string>();//list for storing the matches
                MatchCollection mathcollection = Regex.Matches(char_to_check.ToString(), "[A-Z]{1}");//otain matches
                if(mathcollection.Count ==1)//count must be equal to one
                {
                    return true;
                }
                return false;
            }


            public void addNode(char value)
            {
                if (next == null)// check it it is the end of list
                {
                    next = new Node(value);//add the new node since it is the end of list

                }
                else
                {
                    next.addNode(value);//pass value and move to the next node since it is Not the last
                }
            }
        }

    }
}
