using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    class MainClass
    {
        public static string symbolcheck = "*-+/%X^1234567890calcu ";
        public static List<Item> rhs = new List<Item>();
        public static List<Item> lhs = new List<Item>();

        public static void Main(string[] args)
        {
            TakeInput();
            foreach (Item i in rhs)
            {
                Console.WriteLine(i.GetName());
            }
        }

        private static void CheckInput()
        {
            if(rhs[0].GetName().Equals("calc")){
                rhs.RemoveAt(0);
				foreach (Item i in rhs)
				{
                    if (!i.GetName().Contains("5"))
					{
						Console.WriteLine("Error in input");
                        TakeInput();
						break;
					}
				}
            }
            else{
                Console.WriteLine("Please enter valid input");
                TakeInput();
            }
        }


        public static void TakeInput()
        {
            List<char> input = Console.ReadLine().ToList();
            string current = "";
            rhs = new List<Item>();
            lhs = new List<Item>();
            bool inBrackets = false;
            foreach (char c in input)
            {
                if(!symbolcheck.Contains(c)){
					TakeInput();
					break;
                }
                if (inBrackets)
                {
                    if (c == ')')
                    {
                        inBrackets = false;
                        current += c;
                    }
                    else
                        current += c;
                }
                else
                {
                    if (Char.IsWhiteSpace(c))
                    {
                        rhs.Add(new Item(current, ItemType.number));
                        current = "";
                    }
                    else if (c == '(')
                    {
                        inBrackets = true;
                        current += c;
                    }
                    else
                    {
                        current += c;
                    }
                }
            }
            rhs.Add(new Item(current, ItemType.number));
			CheckInput();
		}

    }




    class Item
    {
        string name;
        ItemType type;

        public string GetName(){
            return name;
        }

        public Item(string name, ItemType type)
        {
            this.name = name;
            this.type = type;
        }

    }
    enum ItemType
    {
        number, variable, sqVariable, multiply, divide, addition, subtract, modulus
    };
}
