using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace input_validate_class
{
    internal class input_validate
    {
        private static bool is_valid;
        private static string error_msg = "";
        private static int special_characters;
        private static int spaces;
        private static int upper_case_letters;
        private static int digits;

        private static (int, int, int, int) Character_types(string word)
        {
            // Returns the count of special characters, spaces, uppercase letters, and digits in the given string.
            // Args: word(string) - The string to be analyzed.
            // Returns: Item1: specialCharacters (integer)
            //          Item2: spaces (integer)
            //          Item3: upperCaseLetters (integer)
            //          Item4: digits (integer)

            special_characters = 0;
            spaces = 0;
            upper_case_letters = 0;
            digits = 0;

            foreach (char c in word)
            {
                if (!char.IsLetter(c) && c != ' ')
                {
                    special_characters++;
                }
                if (c == ' ')
                {
                    spaces++;
                }
                if (char.IsUpper(c))
                {
                    upper_case_letters++;
                }
                if (char.IsDigit(c))
                {
                    digits++;
                }
            }

            return (special_characters, spaces, upper_case_letters, digits);
        }

        private static bool RepeatedThreeTimes(string word)
        {
            // Returns true if the same character repeats three times consecutively in the given word; otherwise, returns false.
            for (int i = 0; i < word.Length - 2; i++)
            {
                if (word[i] == word[i + 1] && word[i + 1] == word[i + 2])
                {
                    return true;
                }
            }
            return false;
        }


        public static (bool, string) Name_validation(string word)
        {
            // Validates if the given string is in 'name' format.
            // Args: word(string) - The string to be validated.
            // Returns: Item1: isValid (boolean)
            //          Item2: errorMessage (string)
            //---------------------------------------------------------------
            // If the word is null or has 0 characters --> (false, error message)
            // If the word length is not between 1 and 25 --> (false, error message)
            // If the word contains one or more special characters --> (false, error message)
            // Other cases --> (true, error message (empty string))

            is_valid = true;
            error_msg = "";
            word = word.Trim();
            special_characters = Character_types(word).Item1;

            if (string.IsNullOrEmpty(word))
            {
                error_msg += "This area is required";
                is_valid = false;
            }
            else if (word.Length <= 1 || word.Length >= 25)

            {
                error_msg += "Your name must be at least two and at most 25 characters\n";
                is_valid = false;
            }
            if (special_characters > 0)
            {
                error_msg += "Your name cannot contain special characters";
                is_valid = false;
            }
            return (is_valid, error_msg);
        }

        public static (bool, string) Email_validation(string email)
        {
            // Validates if the given string is in email format.
            // Args: email(string) - The string to be validated.
            // Returns: Item1: isValid (boolean)
            //          Item2: errorMessage (string)
            //----------------------------------------------------
            // If the email is null or has 0 characters --> (false, error message)
            // If the email does not contain '@' or '.' --> (false, error message)
            // If the email contains spaces --> (false, error message)
            // If the part before '@', between '@' and '.', or after '.' does not contain at least 2 characters --> (false, error message)
            // Other cases --> (true, error message (empty string))

            is_valid = true;
            error_msg = "";
            email = email.Trim();
            (special_characters, spaces, _, _) = Character_types(email);


            if (string.IsNullOrEmpty(email))
            //mail bos mu kontrolu
            {
                error_msg += "This area is required";
                is_valid = false;
            }
            else if (!email.Contains("@") || !email.Contains("."))
            {
                error_msg += "Mail format must be like: 'someone@example.com'\n";
                is_valid = false;
            }
            else if (spaces > 0)
            {
                error_msg += "Your email adress cannot contain spaces.\n";
                is_valid = false;
            }
            else
            {
                int atIndex = email.IndexOf("@");
                int dotIndex = email.IndexOf(".");

                string ad = email.Substring(0, atIndex);
                string domain = email.Substring(atIndex + 1, dotIndex - atIndex - 1);
                string uzanti = email.Substring(dotIndex + 1, email.Length - dotIndex - 1);

                if (ad.Length < 2)
                {
                    error_msg += "Your email adress must have at least 2 characters before the '@' symbol.\n";
                    is_valid = false;
                }
                if (domain.Length < 2)
                {
                    error_msg += "At least a 2-character domain name is required.\n";
                    is_valid = false;

                }
                else if (Character_types(domain).Item1 > 0)
                {
                    error_msg += "Domain cannot contain special characters.\n";
                    is_valid = false;
                }
                if (uzanti.Length < 2)
                {
                    error_msg += "At least 2-Characters extension is required\n";
                    is_valid = false;
                }

                else if (Character_types(uzanti).Item1 > 0)
                {
                    error_msg += "Extension cannot contains special characters\n";
                    is_valid = false;
                }

            }
            return (is_valid, error_msg);
        }

        public static (bool, string) Password_validation(string Sifre)
        {
            is_valid = true;
            error_msg = "";
            (special_characters, spaces, upper_case_letters, _) = Character_types(Sifre);

            if (string.IsNullOrEmpty(Sifre))
            {
                is_valid = false;
                error_msg += "This area is required.\n";

            }
            else if (Sifre.Length < 10 || Sifre.Length > 30)
            {
                is_valid = false;
                error_msg += "Your password must be at least 10 characters at most 30 characters \n";
            }
            else
            {
                if (spaces > 0)
                {
                    is_valid = false;
                    error_msg += "Your password cannot contain spaces.\n";
                }
                if (special_characters < 1)
                {
                    is_valid = false;
                    error_msg += "Your password must contain at least 1 special character.\n";
                }
                if (upper_case_letters < 1)
                {
                    is_valid = false;
                    error_msg += "Your password must contain at least 1 upper character.\n";
                }
                if (RepeatedThreeTimes(Sifre))
                {
                    is_valid = false;
                    error_msg += "A character canont be used 3 times consecutively in the password.\n";
                }

            }

            return (is_valid, error_msg);
        }

        public static (bool, string) PhoneNum_validation(string TelNo)
        {
            is_valid = true;
            error_msg = "";

            if (string.IsNullOrEmpty(TelNo))
            {
                is_valid = false;
                error_msg += "This area is required\n";

            }
            else if (TelNo.Length != 10)
            {
                is_valid = false;
                error_msg += "Please enter your telephone number as 5xx xxx xxx xx xx with 10 digits.\n";
            }
            else if (Character_types(TelNo).Item4 != 10)
            {
                is_valid = false;
                error_msg += "Check your phone number.\n";
            }
            else if (!TelNo.StartsWith("5"))
            {
                is_valid = false;
                error_msg += "Phone number must start with '5'.\n";
            }
            return (is_valid, error_msg);
        }

        public static (bool, string) Id_validation(string tckn)
        {
            is_valid = true;
            error_msg = "";
            if (string.IsNullOrEmpty(tckn))
            {
                is_valid = false;
                error_msg += "This area is required\n";

            }
            else if (tckn.Length != 11)
            {
                is_valid = false;
                error_msg += "Your ID number must consist of 11 digits";
            }
            else if (Character_types(tckn).Item4 != 11)
            {
                is_valid = false;
                error_msg += "Check your ID number.\n";
            }
            else if (tckn.StartsWith("0"))
            {
                is_valid = false;
                error_msg += "Your ID number cannot start with '0'\n";
            }
            return (is_valid, error_msg);
        }

        public static (bool, string) Age_validation(string yas)
        {
            is_valid = true;
            error_msg = "";
            if (string.IsNullOrEmpty(yas))
            {
                is_valid = false;
                error_msg += "This area is required\n";

            }
            else if (Character_types(yas).Item4 != yas.Length)
            {
                is_valid = false;
                error_msg += "Check your age.";
            }
            else if (int.Parse(yas) <= 0 || int.Parse(yas) >= 150)
            {
                is_valid = false;
                error_msg += "Check your age.";
            }
            return (is_valid, error_msg);
        }
    }

}

