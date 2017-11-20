using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class User
    {
        private string UserName;
        private string UserPassword;
        private string UserTelephone;
        private string UserEmail;
        private string UserID;
        private string RelationshipUserID;

        User()
        {
            
        }

        public string GetUserID()
        {
            return UserID;
        }

        public string GetRelationshipUserID()
        {
            return RelationshipUserID;
        }

        public string GetUserName()
        {
            return UserName;
        }

        public string GetUserPassword()
        {
            return UserPassword;
        }

        public string GetUserTelephone()
        {
            return UserTelephone;
        }

        public string GetUserEmail()
        {
            return UserEmail;
        }
    }
}
