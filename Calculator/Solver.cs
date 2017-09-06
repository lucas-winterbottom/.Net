﻿﻿﻿using System;
using System.Collections.Generic;

namespace Calculator
{
    public class Solver
    {
        private List<Term> lhs, rhs;

        public Solver(List<Term> lhs, List<Term> rhs)
        {
            this.lhs = lhs;
            this.rhs = rhs;
        }

        public void Solve()
        {
            //Brackets();
            MulDivideModulus(lhs);
            MulDivideModulus(rhs);
            MovePronumeralsLeft();
            BalanceLeft();
            PlusMinus();
            switch (DecideMethod())
            {
                case 1:
                    DivideX();
                    break;
                case 2:
                    DivideX();
                    Squareroot();
                    break;
                case 3:
                    //SolveQuadratic()
                    break;
            }
            Console.ReadLine();
        }

        private void Squareroot()
        {
            throw new NotImplementedException();
        }

        private int DecideMethod()
        {
            int i = 0;
            foreach (Term t in lhs)
            {
                if (t.Type == TermType.variable)
                {
                    i++;
                }
                if (t.Type == TermType.sqVariable)
                {
                    i += 2;
                }
            }
            return i;
        }

        private void MovePronumeralsLeft()
        {
            for (int i = 0; i < rhs.Count; i++)
            {
                if (!rhs[i].IsNumberz())
                {
                    rhs[i].InvertValue();
                    lhs.Add(rhs[i]);
                    rhs.RemoveAt(i);
                    i--;
                }
            }
        }

        private void DivideX()
        {
            if(rhs.Count==0) rhs.Add(new Term(0));
            rhs[0].Coeff = rhs[0].Coeff / lhs[0].Coeff;
            lhs[0].Coeff = 1;
            PrintOutput("DivideX");
        }

        private void BalanceLeft()
        {
            for (int i = 0; i < lhs.Count; i++)
            {
                if (lhs[i].Modifier == Modifier.NONE && lhs[i].Type == TermType.number)
                {
                    lhs[i].InvertValue();
                    rhs.Add(lhs[i]);
                    lhs.RemoveAt(i);
                    i--;
                }
            }
            PrintOutput("BalanceLeft");
        }
//ALso consider overloading + and Minus)
        public void PlusMinus()
        {
            lhs.Sort((x, y) => x.Type.CompareTo(y.Type));
            //LHS Plus (Should be only pronumerals)
            for (int i = 1; i < lhs.Count; i++)
            {
                if (lhs[i].Type == lhs[i - 1].Type)
                {
                    lhs[i].Coeff += lhs[i - 1].Coeff;
                    lhs.RemoveAt(i - 1);
                    i--;
                }
            }
            ///RHS Plus
            for (int i = 1; i < rhs.Count; i++)
            {
                if (rhs[i].Modifier == Modifier.NONE && rhs[i].IsNumberz() && rhs[i - 1].IsNumberz())
                {
                    rhs[i].Coeff += rhs[i - 1].Coeff;
                    rhs.RemoveAt(i - 1);
                    i--;
                }
            }
            PrintOutput("PlusMinus");
        }

        private void MulDivideModulus(List<Term> side)
        {
            for (int i = 1; i < side.Count; i++)
            {
                switch (side[i].Modifier)
                {
                    case Modifier.MUL:
                        side[i] = side[i] * side[i - 1];
                        side.RemoveAt(i - 1);
                        i--;
                        break;
                    case Modifier.MOD:
                        side[i] = side[i - 1] % side[i];
                        side.RemoveAt(i - 1);
                        i--;
                        break;
                    case Modifier.DIV:
                        side[i] = side[i - 1] / side[i];
                        side.RemoveAt(i - 1);
                        i--;
                        break;
                    default:
                        break;
                }
            }
            PrintOutput("MulDiv");
        }

        public void PrintOutput(string caller)
        {
            Console.WriteLine(caller);
            foreach (Term i in lhs)
            {
                Console.Write(i.ToString());
            }
            Console.Write("= ");
            foreach (Term i in rhs)
            {
                Console.Write(i.ToString());
            }
            Console.WriteLine();
        }

    }
}
