using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace WebServiceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CostCenterController : ControllerBase
    {
        public readonly IConfiguration _configuration;
        public ResponseHelperController _response { get; }

        public CostCenterController(IConfiguration configuration)
        {
            _configuration = configuration;
            _response = new ResponseHelperController();

        }

        [HttpGet("save")]
        public string insert(string code, string name, string costCenterID = null)
        {
            
            //MIT.Utility.GetConnectionString("", "", "", "", "")
            string connection = "Data Source=149.129.215.61\\SQLEXPRESS,49232;Initial Catalog=DEV_FINAC_TIGER;User ID=tigeradmin;Password=Admin1234!";

            MIT.Model.BaseModel.DBConnection = new SqlConnection(connection);
            if (MIT.Model.BaseModel.DBConnection.State != ConnectionState.Open)
            {
                MIT.Model.BaseModel.DBConnection.Open();
            }

            //MIT.Model.BaseModel.DBTransaction = MIT.Model.BaseModel.DBConnection.BeginTransaction();

            //try
            //{
            //    //insert ke model
            //    FINAC.Model.AccountingModel.InsertUpdateCostCenter(code, name, costCenterID);

            //    MIT.Model.BaseModel.DBTransaction.Commit();
            //}
            //catch (Exception)
            //{

            //    MIT.Model.BaseModel.DBTransaction.Rollback();
            //}
            //finally
            //{
            //    MIT.Model.BaseModel.DBConnection.Dispose();
            //}



            string query = @"SELECT * FROM CostCenter where code = '" + code + "' and name = '" + name + "'";
            DataTable table = new DataTable();
            string sqlDataSource = connection;
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
