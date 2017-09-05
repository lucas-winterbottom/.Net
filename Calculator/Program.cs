using System;
using System.Collections.Generic;
using System.Linq;

namespace Calculator
{
    class MainClass
    {
        private static InputReader reader;

        public static void Main(string[] args)
        {
            reader = new InputReader();
            Solver solver = new Solver(reader.Lhs, reader.Rhs);
            solver.Solve();
        }
    }
}
