using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Api_SOP.LibaryHelper
{
    public class SqlHelper
    {
        public static T ExcuteCommandToModel<T>(string connect, string command)
        {
            var list = new List<T>();

            var k = new SqlConnection(connect);

            using (SqlConnection conn = k)
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(command, conn);

                var dt = cmd.ExecuteReader();

                list = DataReaderMapToList<T>(dt);

                conn.Close();
            }

            return list.FirstOrDefault();
        }
        private static List<T> DataReaderMapToList<T>(SqlDataReader dr)
        {
            List<T> list = new List<T>();
            T obj = default(T);
            while (dr.Read())
            {
                obj = Activator.CreateInstance<T>();
                foreach (PropertyInfo prop in obj.GetType().GetProperties())
                {
                    try
                    {
                        if (!object.Equals(dr[prop.Name], DBNull.Value))
                        {
                            prop.SetValue(obj, dr[prop.Name], null);
                        }
                    }
                    catch
                    {
                        continue;
                    }

                }
                list.Add(obj);
            }
            return list;
        }
        public static MessageReport ExcuteCommandToMesageReport(string connect, string command)
        {
            var result = new MessageReport(false, "error");

            var k = new SqlConnection(connect);

            using (SqlConnection conn = k)
            {
                try
                {

                    conn.Open();
                    SqlCommand cmd = new SqlCommand(command, conn);

                    var dt = cmd.ExecuteReader();

                    conn.Close();

                    result = new MessageReport(true, "Success");
                }
                catch (Exception ex)
                {
                    result = new MessageReport(false, ex.Message);
                }
            }

            return result;
        }

    }
}
