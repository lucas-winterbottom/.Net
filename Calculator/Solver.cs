﻿using System;
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
                    SolveQuadratic();
                    break;
            }
            Console.ReadLine();
        }

        private void SolveQuadratic()
        {
            rhs[0].InvertValue();
            lhs.Add(rhs[0]);
            rhs.RemoveAt(0);
            PrintOutput("Prep Quadratic");
            int a = Convert.ToInt32(lhs[0].Coeff);
            int b = Convert.ToInt32(lhs[1].Coeff);
            int c = Convert.ToInt32(lhs[2].Coeff);

            double sqrtpart = b * b - 4 * a * c;
            double x1 = (b + Math.Sqrt(sqrtpart)) / 2 * a;
            double x2 = (b - Math.Sqrt(sqrtpart)) / 2 * a;
            PrintXs(x1, x2);

        }

        private void PrintXs(double x1, double x2)
        {
            if (x1.Equals(Double.NaN) || x2.Equals(Double.NaN)) ErrorHandler.ExitWithMessage(Error.NaN);
            Console.WriteLine("X = " + Convert.ToInt32(x1) + "," + Convert.ToInt32(x2));
        }

        private void Squareroot()
        {
            double x1 = Math.Sqrt(rhs[0].Coeff);
            double x2 = -Math.Sqrt(rhs[0].Coeff);
            PrintXs(x1, x2);
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
            if (rhs.Count == 0) rhs.Add(new Term(0));
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
            lhs.Sort((x, y) => y.Type.CompareTo(x.Type));
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
                if (rhs[i].IsNumberz() && rhs[i - 1].IsNumberz())
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
            Console.WriteLine("----------------");
        }

    }
}
