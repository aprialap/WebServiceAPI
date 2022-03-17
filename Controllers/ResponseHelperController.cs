using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Data;
using System.Text;

namespace WebServiceAPI.Controllers
{
    public class ResponseHelperController
    {
        public string Response_Object(DataTable dt, string code, string msg)
        {
            var JSON_String = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                JSON_String.Append("{");
                JSON_String.Append("\"code\" : \"" + code + "\",");
                JSON_String.Append("\"message\" : \"" + msg + "\",");
                JSON_String.Append("\"data\" : ");
                JSON_String.Append("[");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JSON_String.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            JSON_String.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JSON_String.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        JSON_String.Append("}");
                    }
                    else
                    {
                        JSON_String.Append("},");
                    }
                }
                JSON_String.Append("]}");
            }
            return JSON_String.ToString();
        }

        public  string Response_FirstObject(DataTable dt, string code, string msg)
        {
            var JSON_String = new StringBuilder();
            if (dt.Rows.Count > 0)
            {
                JSON_String.Append("{");
                JSON_String.Append("\"code\" : \"" + code + "\",");
                JSON_String.Append("\"message\" : \"" + msg + "\",");
                JSON_String.Append("\"data\" : ");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    JSON_String.Append("{");
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        if (j < dt.Columns.Count - 1)
                        {
                            JSON_String.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == dt.Columns.Count - 1)
                        {
                            JSON_String.Append("\"" + dt.Columns[j].ColumnName.ToString() + "\":" + "\"" + dt.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == dt.Rows.Count - 1)
                    {
                        JSON_String.Append("}");
                    }
                    else
                    {
                        JSON_String.Append("},");
                    }
                }
                JSON_String.Append("}");
            }
            return JSON_String.ToString();
        }

    }
}