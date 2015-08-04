using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsRaizTest.Classes
{
    public class SQLHelper
    {

        public static SqlParameter[] GetSQLParams(string[] paramsNames, object[] paramsValues)
        { 
            if (paramsNames.Length!=paramsValues.Length)
            {
            return null;
            }
            else
            {
                List<SqlParameter> lst=new List<SqlParameter>();
                for (int i=0;i<paramsNames.Length;i++)
                {
                    lst.Add(new SqlParameter(paramsNames[i], paramsValues[i]));
                }
                return lst.ToArray();
            }
            
        }

        public static OperationResult SQLCommandNoResult(string sqlConString,string commandText,string[] paramsNames, object[] paramsValues)
        {
            string result=string.Empty;
            
            using (SqlConnection  sqlcon=new SqlConnection(sqlConString))
            using (SqlCommand sqlcomm = new SqlCommand())
            {
                sqlcomm.Connection = sqlcon;
                sqlcomm.CommandText = commandText;
                if (paramsNames!=null)
                {
                    sqlcomm.Parameters.AddRange(GetSQLParams(paramsNames,paramsValues));
                }
                try
                {
                    sqlcon.Open();
                    int RecordCounts = sqlcomm.ExecuteNonQuery();
                    if (RecordCounts > 0)
                        result="Запись успешно добавлена в базу";
                    else
                        result="Непредвиденная ошибка. Запись не была добавлена. Обратитесь в службу поддержки";

                }
                catch (Exception exc)
                {
                    result=exc.Message;
                }
                finally
                {
                    sqlcon.Close();
                }
            }

            return new OperationResult(true, result);
        }



        public static SqlDataAdapter SQLDAForAdmin(string sqlConString,out string[] columnNames)
        {


            columnNames = new string[] {"ID",
            "Фамилия",
            "Имя",
            "Отчество",
            "Серия паспорта",
            "Номер паспорта",
            "Знание интерфейсов",
            "Знание русского языка",
            "Знание архитектуры" };


            SqlDataAdapter sqlDA = new SqlDataAdapter();
            SqlConnection SQLCon = new SqlConnection(sqlConString);
            sqlDA.SelectCommand = new SqlCommand(@"SELECT  
            ID,
            FamilyName,
            FirstName,
            Patronymic,
            PassportSerial,
            PassportNumber,
            KnowlOfInterfaces,
            KnowlOfRussian,
            KnowlOfArchitecture FROM PeopleInfo", SQLCon);

            sqlDA.UpdateCommand = new SqlCommand(@"UPDATE PeopleInfo SET 
            FamilyName=@famname,
            FirstName=@firname,
            Patronymic=@patromymic,
            PassportSerial=@passpser,
            PassportNumber=@passpnum,
            KnowlOfInterfaces=@knwlifaces,
            KnowlOfRussian=@knwlrussian,
            KnowlOfArchitecture=@knwlarch
            WHERE ID=@id
            ", SQLCon);

            sqlDA.UpdateCommand.Parameters.Add("@id", SqlDbType.Int, int.MaxValue, "ID");
            sqlDA.UpdateCommand.Parameters.Add("@famname", SqlDbType.NVarChar, 250, "FamilyName");
            sqlDA.UpdateCommand.Parameters.Add("@firname", SqlDbType.NVarChar, 250, "FirstName");
            sqlDA.UpdateCommand.Parameters.Add("@patromymic", SqlDbType.NVarChar, 250, "Patronymic");
            sqlDA.UpdateCommand.Parameters.Add("@passpser", SqlDbType.NVarChar, 4, "PassportSerial");
            sqlDA.UpdateCommand.Parameters.Add("@passpnum", SqlDbType.NVarChar, 6, "PassportNumber");
            sqlDA.UpdateCommand.Parameters.Add("@knwlifaces", SqlDbType.Bit, 1, "KnowlOfInterfaces");
            sqlDA.UpdateCommand.Parameters.Add("@knwlrussian", SqlDbType.Bit, 1, "KnowlOfRussian");
            sqlDA.UpdateCommand.Parameters.Add("@knwlarch", SqlDbType.Bit, 1, "KnowlOfArchitecture");

            sqlDA.DeleteCommand = new SqlCommand(@"DELETE FROM PeopleInfo WHERE ID=@id", SQLCon);
            sqlDA.DeleteCommand.Parameters.Add("@id", SqlDbType.Int, int.MaxValue, "ID");

            return sqlDA;
        }
    }
}
