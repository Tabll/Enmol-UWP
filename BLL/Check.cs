﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
    public class Check
    {
        public static bool CheckUserWordAndPasswordEnter(string UserName, string Password)
        {
            Regex emailRegular = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
            Regex phoneNumberRegular = new Regex("^\\d{11}$");
            return (emailRegular.IsMatch(UserName) || phoneNumberRegular.IsMatch(UserName));
        }

        public static bool CheckPhoneNumber(string PhoneNumber)
        {
            Regex phoneNumberRegular = new Regex("^\\d{11}$");
            return phoneNumberRegular.IsMatch(PhoneNumber);
        }

        public static bool CheckEmailName(string EmailName)
        {
            Regex emailRegular = new Regex("^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$");
            return emailRegular.IsMatch(EmailName);
        }
    }
}
