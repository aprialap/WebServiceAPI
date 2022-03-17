using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace WebServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public ResponseHelperController _response{ get; }

        public AuthenticationController(IConfiguration configuration)
        {
            _configuration = configuration;
            _response = new ResponseHelperController();
           //INAC.Model.FinanceModel.InsertUpdateExpense()
            
        }

        [HttpGet]
        public string list()
        {

            string query = @"SELECT * FROM Authentication";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {

                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

         return _response.Response_Object(table, "200", "success");
        }

        [HttpGet("detail")]
        public string getDetail(string username, string password)
        {
            string query = @"SELECT * FROM Authentication where Username = '"+username+"' and Password = '"+password+"'";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("AppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                 myCon.Open();
                 using (SqlCommand myCommand = new SqlCommand(query, myCon))
                 {
                    
                     myReader = myCommand.ExecuteReader();
                     table.Load(myReader); 
                     myReader.Close();
                     myCon.Close();
                }
            } 
            return _response.Response_FirstObject(table, "200", "success");
        }
    }
}
