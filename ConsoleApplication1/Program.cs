using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libInfra;
using System.Data;

namespace TestInfra
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                InfraWrapper wrapper = new InfraWrapper();
                wrapper.Login(@"EU Business Services", "TEST1234", "VSM");

                //Do things
                if (wrapper.SessionID != String.Empty)
                {
                    DataSet callresult = wrapper.RetrieveCallInfo("1638021");
                    DataTable dt = callresult.Tables[0];
                    String calldetails = ReturnDataFromResultSet(dt, "PROBLEM_DESC");
                    Console.WriteLine(calldetails);
                    wrapper.Logout(wrapper.SessionID);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
          
            Console.ReadLine();
        }

        private static String ReturnDataFromResultSet(DataTable dt, String fieldname)
        {
            if (dt == null)
            {
                return String.Empty;
            }

            // Datatable column 0 is the field name in the cell table
            // Datatable column 1 is the field value in the cell table

            String fName = String.Empty;
            String fValue = String.Empty;

            for (int rowcount = 0; rowcount <= dt.Rows.Count; rowcount++)
            {
                fName = dt.Rows[rowcount].ItemArray[0].ToString();
                fValue = dt.Rows[rowcount].ItemArray[1].ToString();
                if (fName == fieldname)
                {
                    return fValue;
                }
            }

            return fValue;
        }
    }
}
