﻿using System;
namespace Calculator
{
    public class Term
    {
        private TermType type;
        private double coeff;
        private Modifier modifier;

        public Modifier Modifier { get => modifier; set => modifier = value; }
        public double Coeff { get => coeff; set => this.coeff = value; }
        public TermType Type { get => type; set => type = value; }

        public Term()
        {
            coeff = 1;
            type = TermType.number;
        }

        public override string ToString()
        {
            string s = "";
            // s += sign;
            // s += " ";
            // s += modifier;
            // s += " ";
            if (modifier == Modifier.DIV) s += "/";
            else if (modifier == Modifier.MUL) s += "*";
            else if (modifier == Modifier.MOD) s += "%";
            else if (modifier == Modifier.NONE) s += "";
            if(coeff>0)s+="+";
            s += coeff;
            if (type == TermType.variable) s += "X";
            if (type == TermType.sqVariable) s += "X^2";

            return s;

        }

        internal void InvertValue()
        {
            coeff = -coeff;
        }

        internal bool IsVariable()
        {
            return type == TermType.variable;
        }
        internal bool IsSqVariable()
        {
            return type == TermType.sqVariable;
        }
        internal bool IsBrackets()
        {
            return type == TermType.brackets;
        }
        internal bool IsNumberz()
        {
            return type == TermType.number;
        }

    }


    public enum Modifier
    {
        NONE, MOD, MUL, DIV
    }
    public enum TermType
    {
        number, variable, sqVariable, brackets
    }
}
