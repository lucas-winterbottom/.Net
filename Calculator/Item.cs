using System;
namespace Calculator
{
    public class Term
    {
        private string pronumeral;
        private TermType type;
        private double coeff;
        private Sign sign;
        private Modifier modifier;

        public Modifier Modifier { get => modifier; set => modifier = value; }
        public Sign Sign { get => sign; set => sign = value; }
        public double Coeff { get => coeff; set => this.coeff = value; }
        public string Pronumeral { get => pronumeral; set => pronumeral = value; }
        public TermType Type { get => type; set => type = value; }

        public Term()
        {
            sign = Sign.POS;
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

            if (sign == Sign.POS) s += "+";
            s += coeff;
            s += pronumeral;
            s += " ";
            // s += type;
            return s;

        }

        internal void InvertValue()
        {
            if (sign == Sign.POS)
            {
                sign = Sign.NEG;
            }
            else sign = Sign.POS;
            coeff = -coeff;
        }
    }

    public enum Sign
    {
        POS, NEG
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
