using System;
using System.Text.RegularExpressions;

namespace ProfileBook.Validation
{
    public class Validator
    {
        private const string LoginReg = @"^(?=.*[A-Za-z0-9]$)[A-Za-z][A-Za-z\d.-]{3,16}$";
        private const string PasswordReg = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)([a-zA-Z\d]){8,16}$";

        static Validator()
        {
            Login = new Validator(LoginReg);
            Password = new Validator(PasswordReg);
        }
        private Validator(string pat)
        {
            Pat = pat;
        }

        public static bool StringValid(string input, Validator valid)
        {
            return !String.IsNullOrEmpty(input) &&
                Regex.IsMatch(input, valid.Pat);
        }
        public static Validator Login { get; }
        public static Validator Password { get; }

        private string Pat { get; }

    }
}
