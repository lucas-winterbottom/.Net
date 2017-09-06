﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    public class InputReader
    {
        private static List<Term> rhs;
        private static List<Term> lhs;
        private static List<string> input;
        private bool isNegative = false;
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
            Console.WriteLine("PrintParsedInput");
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
        private void ParseInput()
        {
            double numericalValue;
            Term temp = new Term();
            isNegative = false;
            foreach (string s in input)
            {
                if (s.Contains("()"))
                {
                    temp.Type = TermType.brackets;
                    AddTerm(temp);
                    temp = new Term();
                }
                else if (s.Contains("X^2"))
                {
                    temp.Type = TermType.sqVariable;
                    ProcessPronumeral(s, temp);
                    temp = new Term();
                }
                else if (s.Contains("X"))
                {
                    temp.Type = TermType.variable;
                    ProcessPronumeral(s, temp);
                    temp = new Term();
                }
                else if (s.Equals("+"))
                {
                }
                else if (s.Equals("/"))
                {
                    temp.Modifier = Modifier.DIV;
                }
                else if (s.Equals("-"))
                {
                    if (isNegative) isNegative = false;
                    else isNegative = true;
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
                    if (isNegative) temp.Coeff = -numericalValue;
                    else temp.Coeff = numericalValue;
                    AddTerm(temp);
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
            bool hasEquals = false;
            bool hasX = false;
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
                    case '=':
                        hasEquals = true;
                        current += c;
                        break;
                    case 'X':
                        hasX = true;
                        current += c;
                        break;
                    default:
                        current += c;
                        break;
                }
            }
            if (!current.Equals("")) input.Add(current);
            if (input[0].Equals("calc")) RemoveCalc();
            else ThrowError("Command does not contain calc");
            if (!hasEquals) ThrowError("Invalid input formula missing =");
            if (!hasX) ThrowError("Invalid input formula missing an X");
        }

        private void AddTerm(Term test)
        {
            if (isLhs)
            {
                lhs.Add(test);
            }
            else
            {
                rhs.Add(test);
            }
            isNegative = false;
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
                        if (isNegative) temp.Coeff = -Double.Parse(tempno);
                        else temp.Coeff = Double.Parse(tempno);
                    }
                }
            }
            AddTerm(temp);

        }

        private void ThrowError(string msg)
        {
            Console.WriteLine(msg);
            Environment.Exit(0);
        }

        public void RemoveCalc()
        {
            input.RemoveAt(0);
        }
    }

}

