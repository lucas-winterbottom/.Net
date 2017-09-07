using System;

namespace Calculator
{
    internal class ErrorHandler
    {
        internal static void ExitWithMessage(Error e)
        {
            Console.WriteLine(e.ToString());
            Environment.Exit(0);
        }
    }

    internal enum Error
    {
        NaN, NoEquals, NoPronumeral, InvalidCharacters, MissingCalc, DivByZero
    }
}
