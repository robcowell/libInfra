using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libInfra;
using System.Data;
using System.IO;

namespace TestInfra
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                InfraWrapper wrapper = new InfraWrapper();
                wrapper.Login(@"EU Business Webservice", "TEST1234", "VSM");

                //Do things
                if (wrapper.SessionID != String.Empty)
                {
                    DataSet callresult = wrapper.RetrieveCallInfo("~372029");
                    if (callresult.Tables.Count > 0)
                    {
                        DataTable dt = callresult.Tables[0];
                        String calldetails = ReturnDataFromResultSet(dt, "PROBLEM_DESC");
                        Console.WriteLine(calldetails);
                    }
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

            
            String fName = String.Empty;
            String fValue = String.Empty;

           

           
            fValue = dt.Rows[0][fieldname].ToString();
            return fValue;    
        }
    }
}
