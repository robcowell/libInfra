using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace libInfra
{
    public class InfraWrapper
    {
        private static ServiceManagerClient client;
        private String sUsername = String.Empty;
        private String sPassword = String.Empty;
        private String sDatabase = String.Empty;
        private String sSessionID = String.Empty;

        public static ServiceManagerClient Client
        {
            get
            {
                return client ?? (client = new ServiceManagerClient());
            }
        }

        public string SessionID
        {
            get
            {
                return sSessionID;
            }
            set
            {
                sSessionID = value;
            }
        }

        public InfraWrapper()
        {
            
        }

        public APIReturn Login(String sUsername, String sPassword, String sDatabase)
        {
            APIReturn apireturn = APIReturn.API_ERROR_LOGINFAIL;
            String sMessage = String.Empty;
            String sID = String.Empty;

            try
            {
                apireturn = Client.Login(sUsername,sPassword,sDatabase,out sMessage,out sID);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            sSessionID = sID;

            return apireturn;
        }

        public APIReturn Logout(String sessionID)
        {
            APIReturn apireturn = APIReturn.API_ERROR;

            String sMessage = String.Empty;

            try
            {
                apireturn = Client.Logout(sessionID, out sMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return apireturn;
        }

        public DataSet RetrieveCallInfo(String callnumber)
        {
            String lEntityRef = buildLookupString(callnumber);

            String sMessage = String.Empty;
            DataSet resultset = new DataSet();
            try
            {
                Client.CallRetrieve(SessionID, null, null, null, lEntityRef, null, out sMessage, out resultset);
                Console.WriteLine(sMessage);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
            return resultset;
        }

        private String buildLookupString(Object input)
        {
            String strBuilder = String.Empty;
            if (input != null)
            {
                if (input.GetType() == typeof(int))
                {
                    strBuilder = "~" + input.ToString();
                }
                else
                {
                    strBuilder = input.ToString();
                }
            }

            return strBuilder;    
        }

        

    }
}
