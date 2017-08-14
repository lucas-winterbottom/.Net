using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    class MainClass
    {
        public static string symbolcheck = "*-+/%X^1234567890calc() ";
        public static List<Item> rhs = new List<Item>();
        public static List<Item> lhs = new List<Item>();

        public static void Main(string[] args)
        {
            TakeInput();
        }

        private static void ProcessInput()
        {
            foreach(Item i in rhs){
                Console.WriteLine(i.GetName());
            }

            if(rhs[0].GetName().Equals("calc")){
                rhs.RemoveAt(0);
				Console.WriteLine("Valid input");
			}
            else{
                Console.WriteLine("Please enter valid input");
                ClearData();
                TakeInput();
            }
        }

        public static void ClearData(){
			rhs.Clear();
			lhs.Clear();
        }

        public static void TakeInput()
        {
            List<char> input = Console.ReadLine().ToList();
            string current = "";

            bool inBrackets = false;
            foreach (char c in input)
            {
                if(!symbolcheck.Contains(c)){
					Console.WriteLine("Please enter valid input");
                    ClearData();
					TakeInput();
                    return;
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
			ProcessInput();
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
