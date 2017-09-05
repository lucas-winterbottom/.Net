using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    public class InputReader
    {
        private static List<Term> rhs;
        private static List<Term> lhs;
        private static List<string> input;
        private bool isLhs = false;
        private static string symbolcheck = "*-+/%X^1234567890calc()= ";

        public List<Term> Lhs
        {
            get
            {
                return lhs;
            }
        }
        public List<Term> Rhs
        {
            get
            {
                return rhs;
            }
        }

        public InputReader()
        {
            rhs = new List<Term>();
            lhs = new List<Term>();
            input = new List<string>();
            isLhs = true;
            InputToStrings();
            PrintInput();
            ParseInput();
            PrintParsedInput();
            Console.ReadLine();
        }

        private void PrintParsedInput()
        {
            Console.WriteLine("LHS");
            foreach (Term i in lhs)
            {
                Console.WriteLine(i.ToString());
            }
            Console.WriteLine("RHS");
            foreach (Term i in rhs)
            {
                Console.WriteLine(i.ToString());
            }
        }

        //Fix issue with multiple -- ++ etc etc
        //Fix issue with -ve pronumerals
        private void ParseInput()
        {
            double numericalValue;
            Term temp = new Term();
            foreach (string s in input)
            {
                if (s.Contains("()"))
                {
                    temp.Type = TermType.brackets;
                    temp.Pronumeral = s;
                    AddItem(temp);
                    temp = new Term();
                }
                else if (s.Contains("X^2"))
                {
                    temp.Type = TermType.sqVariable;
                    temp.Pronumeral = "X^2";
                    ProcessPronumeral(s, temp);
                    temp = new Term();
                }
                else if (s.Contains("X"))
                {
                    temp.Type = TermType.variable;
                    temp.Pronumeral = "X";
                    ProcessPronumeral(s, temp);
                    temp = new Term();
                }
                else if (s.Equals("+"))
                {
                    temp.Sign = Sign.POS;
                }
                else if (s.Equals("/"))
                {
                    temp.Modifier = Modifier.DIV;
                }
                else if (s.Equals("-"))
                {
                    temp.Sign = Sign.NEG;
                }
                else if (s.Equals("*"))
                {
                    temp.Modifier = Modifier.MUL;
                }
                else if (s.Equals("%"))
                {
                    temp.Modifier = Modifier.MOD;
                }
                else if (s.Equals("="))
                {
                    isLhs = false;
                    temp = new Term();
                }
                else if (Double.TryParse(s, out numericalValue))
                {
                    if (temp.Sign == Sign.POS) temp.Coeff = numericalValue;
                    else temp.Coeff = -numericalValue;
                    AddItem(temp);
                    temp = new Term();
                }
                else
                {
                    ThrowError("error in input -- ParseInput()");
                }
            }
        }

        public void PrintInput()
        {
            foreach (string s in input)
            {
                Console.WriteLine(s);
            }
        }

        private void InputToStrings()
        {
            List<char> consoleInput = Console.ReadLine().ToList();
            string current = "";
            bool inBrackets = false;
            foreach (char c in consoleInput)
            {
                if (!symbolcheck.Contains(c))
                {
                    ThrowError("Invalid character found in input");
                }
                switch (c)
                {
                    case ' ':
                        if (inBrackets) current += c;
                        else if (!current.Equals(""))
                        {
                            input.Add(current);
                            current = "";
                        }
                        break;
                    case '(':
                        inBrackets = true;
                        current += c;
                        break;
                    case ')':
                        input.Add(current + c);
                        inBrackets = false;
                        current = "";
                        break;
                    default:
                        current += c;
                        break;
                }
            }
            if (!current.Equals("")) input.Add(current);
            if (input[0].Equals("calc")) RemoveCalc();
            else ThrowError("Command does not contain calc");
        }

        private void AddItem(Term test)
        {
            if (isLhs)
            {
                lhs.Add(test);
            }
            else
            {
                rhs.Add(test);
            }
        }


        private void ProcessPronumeral(string s, Term temp)
        {
            string tempno = "";
            foreach (char c in s)
            {
                if (Char.IsDigit(c))
                {
                    tempno += c;
                }
                else
                {
                    if (!tempno.Equals(""))
                    {
                        if (temp.Sign == Sign.NEG) temp.Coeff = -Double.Parse(tempno);
                        else temp.Coeff = Double.Parse(tempno);
                    }
                    AddItem(temp);
                }
            }
        }

        private void ThrowError(string msg)
        {
            Console.WriteLine("Please enter valid input");
            Console.WriteLine(msg);
            ClearData();
            //make this method that calls all input functions
        }
        //Clears data from all the lists
        private void ClearData()
        {
            rhs.Clear();
            lhs.Clear();
            input.Clear();
            InputToStrings();
        }

        public void RemoveCalc()
        {
            input.RemoveAt(0);
        }
    }

}

