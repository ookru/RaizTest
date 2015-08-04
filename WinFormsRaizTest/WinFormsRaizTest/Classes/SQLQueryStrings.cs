using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsRaizTest.Classes
{
    public static class SQLQueryStrings
    {
        public static string GetUserRole = "SELECT ur.UserRole FROM UserRoles ur WHERE ur.Login=@login";
        public static string[] GetUserRoleParamName=new string[] {"@login"};
        
        public static string InsertPeopleInfo = @"INSERT INTO PeopleInfo VALUES (@FamilyName,@FirstName,@Patronymic,@PassportSerial,@PassportNumber,@BitInterfaces,@BitRussian,@BitArchitecture)";
        public static string[] InsertPeopleParams = new string[] {"@FamilyName","@FirstName","@Patronymic","@PassportSerial","@PassportNumber","@BitInterfaces","@BitRussian","@BitArchitecture"};


    }
}
