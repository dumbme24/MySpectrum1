using MySpectrum.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MySpectrum.ValidationService
{
    public class DataValidation
    {
        List<User> users;

        public bool isValidInput(string input, string messgaField)
        {
            if (input.Length > 0)
            {
                return true;
            }
            else
            {
                //Toast.MakeText(this, "Please enter the valid " + messgaField, ToastLength.Short).Show();
                return false;
            }
        }

        public bool ValidatePasswordString(string userPassword, string messageField)
        {
            if (userPassword.Length > 0)
            {
                //string validationPattern = @"^[a-zA-Z][a-zA-Z0-9]*$";
                string validationPattern = @"^(?=.*\d)(?=.*[a-zA-Z]).{5,12}$";

                var regex = new Regex(validationPattern);
                bool regexValidation = regex.IsMatch(userPassword);

                //if above regexValidation validation is true then check for the consecutive character sequence
                if (regexValidation)
                {
                    if (StringHasConsecutiveCharacters(userPassword, 2))
                    {
                        //ToastMessage("Password has a consecutive sequence of characters.");
                        return false;
                    }
                    else
                    {
                        return regexValidation; //this should be true
                    }
                }
                else
                {
                    return false;
                }

            }

            else
                return false;
        }

        public bool StringHasConsecutiveCharacters(string source, int sequenceLength)
        {
            return Regex.IsMatch(source, "([a-zA-Z0-9])\\1{" + (sequenceLength - 1) + "}");
        }

        public bool FieldValidation(User newUser, string database_Path)
        {
            if (!isValidInput(newUser.FirstName, "First Name"))
            {
                return false;
            }

            if (!isValidInput(newUser.LastName, "Last Name"))
            {
                return false;
            }

            if (!isValidInput(newUser.Gender, "Gender"))
            {
                return false;
            }

            if (!isValidInput(newUser.Age, "Age"))
            {
                return false;
            }

            if (!isValidInput(newUser.UserName, "UserName"))
            {
                return false;
            }
            else if (UserNameExist( newUser.UserName, database_Path))
            {
                return false;
            }

            if (!ValidatePasswordString(newUser.UserPassworrd, "Password"))
            {
                //Toast.MakeText(this, "Please enter the valid Password ", ToastLength.Short).Show();
                return false;
            }

            else
            { return true; }
        }

        protected bool UserNameExist(string userName, string database_Path)
        {
            users = new List<User>();
            users = MySpectrum.DataAccessLayer.SQLiteHelper.ReadData(database_Path);

            foreach (var user in users)
            {
                if (user.UserName == userName)
                {
                    //UserName already exist.
                    //ToastMessage("Username " + userName + " already exist.");
                    //DI call for ShowMessage
                    return true;
                }
            }

            return false;
        }
    }
}
