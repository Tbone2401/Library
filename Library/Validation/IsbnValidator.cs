using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;

namespace Library.Validation
{
    [AttributeUsage(AttributeTargets.Property |
                    AttributeTargets.Field, AllowMultiple = false)]
    sealed public class IsbnValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            string possibleISBN = (string)value;
            if (CheckInvalidFormat(possibleISBN)) return false;
            TrimString(ref possibleISBN);
            return CheckISBNFormat(possibleISBN);
        }
        private bool CheckInvalidFormat(string possibleIsbn)
        {
            if (string.IsNullOrEmpty(possibleIsbn)) return true;

            return false;
        }
        private void TrimString(ref string possibleIsbn)
        {
            possibleIsbn = possibleIsbn.Trim();
            possibleIsbn = possibleIsbn.Replace("-", "");
        }

        private bool CheckISBNFormat(string possibleIsbn)
        {
            if (possibleIsbn.Length == 10)
            {
                return CheckIfLastDigitIsValidISBN10(possibleIsbn);
            }
            else if (possibleIsbn.Length == 13)
            {
                return CheckIfLastDigitIsValidISBN13(possibleIsbn);
            }
            return false;
        }

        private bool CheckIfLastDigitIsValidISBN13(string possibleIsbn)
        {
            int isbnSum = 0;
            for (int index = 0; index < possibleIsbn.Length - 1; ++index)
            {
                isbnSum += int.Parse(possibleIsbn[index].ToString()) * (index % 2 == 1 ? 3 : 1);
            }
            int checkValue = 10 - (isbnSum % 10);
            if (checkValue == 10) checkValue = 0;

            int lastValue = GetLastDigit(ref possibleIsbn);

            return checkValue == lastValue;

        }

        private bool CheckIfLastDigitIsValidISBN10(string possibleIsbn)
        {
            int isbnSum = 0;
            for (int index = 0; index < possibleIsbn.Length - 1; ++index)
            {
                isbnSum += int.Parse(possibleIsbn[index].ToString()) * (10 - index);
            }
            int checkValue = 11 - (isbnSum % 11);
            int lastValue = GetLastDigit(ref possibleIsbn);

            return checkValue == lastValue;

        }

        private int GetLastDigit(ref string possibleIsbn)
        {
            if (string.Compare(possibleIsbn[possibleIsbn.Length - 1].ToString(), "x", true) == 0)
            {
                return 10;
            }
            return int.Parse(possibleIsbn[possibleIsbn.Length - 1].ToString());
        }
    }
}
