using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;
using System.Configuration;
using System.Data.SqlClient;

namespace API.Controllers
{
    [Authorize]
    [RoutePrefix("api/ahorro")]
    public class AhorroController : ApiController
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString;


        
        [HttpGet]
        public IHttpActionResult GetAhorro()
        {


            List<Ahorro> ahorroList = new List<Ahorro>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"select AH_CODIGO, AH_CUENTAORIGEN, AH_MONTO, AH_PLAZO, AH_TIPOAHO
                                                            from Ahorros", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Ahorro ahorros = new Ahorro();
                        ahorros.AH_CODIGO = sqlDataReader.GetInt32(0);
                        ahorros.AH_CUENTAORIGEN = sqlDataReader.GetInt32(1);
                        ahorros.AH_MONTO = sqlDataReader.GetInt32(2);
                        ahorros.AH_PLAZO = sqlDataReader.GetString(3);
                        ahorros.AH_TIPOAHO = sqlDataReader.GetString(4);
                        
                        ahorroList.Add(ahorros);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return Ok(ahorroList);
        }

        
        [HttpGet]
        public IHttpActionResult GetTarjetaCredito(int id)
        {
            Ahorro ahorro = new Ahorro();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"select AH_CODIGO, AH_CUENTAORIGEN, AH_MONTO, AH_PLAZO, AH_TIPOAH
                                                            from Ahorros
                                                            where AH_CODIGO  = @AH_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@INV_CODIGO", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        ahorro.AH_CODIGO = sqlDataReader.GetInt32(0);
                        ahorro.AH_CUENTAORIGEN = sqlDataReader.GetInt32(1);
                        ahorro.AH_MONTO = sqlDataReader.GetInt32(2);
                        ahorro.AH_PLAZO = sqlDataReader.GetString(3);
                        ahorro.AH_TIPOAHO = sqlDataReader.GetString(4);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return Ok(ahorro);
        }

        
        [HttpPut]
        public IHttpActionResult PutSolTarjetaCredito(Ahorro ahorros)
        {

            if (ahorros != null)
            {
                if (ValidateAhorroExistence(ahorros.AH_CODIGO))
                {

                    try
                    {
                        using (SqlConnection sqlConnection = new
                            SqlConnection(connectionString))
                        {
                            SqlCommand sqlCommand = new
                                SqlCommand(@"update inversion set AH_CUENTAORIGEN = @AH_CUENTAORIGEN,
                                                           AH_MONTO = @AH_MONTO,
                                                           AH_PLAZO = @AH_PLAZO,
                                                           AH_TIPOAHO = @AH_TIPOAHO
                                                           where AH_CODIGO = @AH_CODIGO",
                                                                    sqlConnection);
                            sqlCommand.Parameters.AddWithValue("@AH_CODIGO", ahorros.AH_CODIGO);
                            sqlCommand.Parameters.AddWithValue("@AH_CUENTAORIGEN", ahorros.AH_CUENTAORIGEN);
                            sqlCommand.Parameters.AddWithValue("@AH_MONTO", ahorros.AH_MONTO);
                            sqlCommand.Parameters.AddWithValue("@AH_PLAZO", ahorros.AH_PLAZO);
                            sqlCommand.Parameters.AddWithValue("@AH_TIPOAHO", ahorros.AH_TIPOAHO);
                            

                            sqlConnection.Open();
                            sqlCommand.ExecuteNonQuery();
                            sqlConnection.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        return InternalServerError(ex);
                    }


                }
                else
                {

                    ahorros = null;
                }
            }

            return Ok(ahorros);
        }

        
        [HttpPost]
        public IHttpActionResult PostAhorros(Ahorro ahorros)
        {

            if (ahorros != null)
            {

                try
                {
                    using (SqlConnection sqlConnection = new
                        SqlConnection(connectionString))
                    {
                        SqlCommand sqlCommand = new SqlCommand(@"insert into Ahorro(AH_CUENTAORIGEN, AH_MONTO,
                                AH_PLAZO, AH_TIPOAHO)
                                                            values (@AH_CUENTAORIGEN, @AH_MONTO,
                                                            @AH_PLAZO, @AH_TIPOAHO)",
                                                                sqlConnection);

                        sqlCommand.Parameters.AddWithValue("@AH_CUENTAORIGEN", ahorros.AH_CUENTAORIGEN);
                        sqlCommand.Parameters.AddWithValue("@AH_MONTO", ahorros.AH_MONTO);
                        sqlCommand.Parameters.AddWithValue("@AH_PLAZO", ahorros.AH_PLAZO);
                        sqlCommand.Parameters.AddWithValue("@AH_TIPOAHO", ahorros.AH_TIPOAHO);
                        sqlConnection.Open();
                        sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                    }
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }
            }

            return Ok(ahorros);
        }

        
        [HttpDelete]
        public IHttpActionResult DeleteAhorro(int id)
        {

            int deletedRows = -1;

            if (ValidateAhorroExistence(id))
            {
                try
                {
                    using (SqlConnection sqlConnection = new
                            SqlConnection(connectionString))
                    {
                        SqlCommand sqlCommand = new
                        SqlCommand(@"delete Ahorro where AH_CODIGO = @AH_CODIGO", sqlConnection);
                        sqlCommand.Parameters.AddWithValue("@AH_CODIGO", id);
                        sqlConnection.Open();
                        deletedRows = sqlCommand.ExecuteNonQuery();
                        sqlConnection.Close();
                    }
                }
                catch (Exception ex)
                {
                    return InternalServerError(ex);
                }

            }

            return Ok(deletedRows);
        }


        private bool ValidateAhorroExistence(int idSolitude)
        {
            bool idSolitudeExists = false;
            int idSolitudeResult = 0;

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"select AH_CODIGO  from Ahorro                                                        
                                                            where AH_CODIGO  = @AH_AHORRO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AH_CODIGO", idSolitude);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        idSolitudeResult = sqlDataReader.GetInt32(0);

                    }
                    sqlConnection.Close();
                }

                if (idSolitudeResult == idSolitude)
                {
                    idSolitudeExists = true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return idSolitudeExists;
        }
    }
}
