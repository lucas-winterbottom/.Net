﻿﻿using System;
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
            //make choice here
            DivideX();
            //Squareroot
            //quadratic formula
            Console.ReadLine();
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
                //Handle x/x x*x for all these types
                if (side[i].Modifier == Modifier.MUL)
                {
                    if (side[i - 1].Type != TermType.number)
                    {
                        side[i].Type = side[i - 1].Type;
                    }
                    side[i].Modifier = Modifier.NONE;
                    side[i].Coeff *= side[i - 1].Coeff;
                    side.RemoveAt(i - 1);
                    i--;
                }
                else if (side[i].Modifier == Modifier.DIV)
                {
                    if (side[i - 1].Type == TermType.number)
                    {
                        side[i].Type = side[i - 1].Type;
                    }
                    side[i].Coeff = side[i - 1].Coeff / side[i].Coeff;
                    side[i].Modifier = Modifier.NONE;
                    side.RemoveAt(i - 1);
                    i--;
                }
                else if (side[i].Modifier == Modifier.MOD)
                {
                    if (side[i - 1].Type == TermType.number)
                    {
                        side[i].Type = side[i - 1].Type;
                    }
                    side[i].Coeff = side[i - 1].Coeff % side[i].Coeff;
                    side[i].Modifier = Modifier.NONE;
                    side.RemoveAt(i - 1);
                    i--;
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
