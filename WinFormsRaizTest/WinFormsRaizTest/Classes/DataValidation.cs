using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsRaizTest.Classes
{
    public static class DataValidation
    {
        //Проверка паспортных данных
        public static OperationResult ValidatePassportData(string pserial,string pnumber)
        {
            string result = string.Empty;
            
            if (pserial.Length != 4)
            {
                result="Серия паспорта может содержать только 4 цифры";
                return new OperationResult(false, result);
            }
            if (pserial.Any(x => !Char.IsDigit(x)))
            {
                result="Серия паспорта может содержать только цифры";
                return new OperationResult(false, result);
            }

            if (pnumber.Length != 6)
            {
                result="Номер паспорта может содержать только 6 цифр";
                return new OperationResult(false, result);
            }
            if (pnumber.Any(x => !Char.IsDigit(x)))
            {
                result="Номер паспорта может содержать только цифры";
                return new OperationResult(false, result);
            }
            return new OperationResult(true,result);
            
        }
    }
}
