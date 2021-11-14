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
        public IHttpActionResult GetAhorros()
        {


            List<Ahorro> ahorroList = new List<Ahorro>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"select Codigo, CuentaOrigen, Monto, Plazo, TipoAhorro
                                                            from Ahorro", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Ahorro ahorro = new Ahorro();
                        ahorro.Codigo = sqlDataReader.GetInt32(0);
                        ahorro.CuentaOrigen = sqlDataReader.GetInt32(1);
                        ahorro.Monto = sqlDataReader.GetInt32(2);
                        ahorro.Plazo = sqlDataReader.GetInt32(3);
                        ahorro.TipoAhorro = sqlDataReader.GetString(4);
                        
                        ahorroList.Add(ahorro);
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
        public IHttpActionResult GetAhorro(int id)
        {
            Ahorro ahorro = new Ahorro();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"select Codigo, CuentaOrigen, Monto, Plazo, TipoAhorro
                                                            from Ahorro
                                                            where Codigo  = @Codigo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        ahorro.Codigo = sqlDataReader.GetInt32(0);
                        ahorro.CuentaOrigen = sqlDataReader.GetInt32(1);
                        ahorro.Monto = sqlDataReader.GetInt32(2);
                        ahorro.Plazo = sqlDataReader.GetInt32(3);
                        ahorro.TipoAhorro = sqlDataReader.GetString(4);
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
        public IHttpActionResult PutSolTarjetaCredito(Ahorro ahorro)
        {

            if (ahorro != null)
            {
                if (ValidateAhorroExistence(ahorro.Codigo))
                {

                    try
                    {
                        using (SqlConnection sqlConnection = new
                            SqlConnection(connectionString))
                        {
                            SqlCommand sqlCommand = new
                                SqlCommand(@"update inversion set CuentaOrigen = @CuentaOrigen,
                                                           Monto = @Monto,
                                                           Plazo = @Plazo,
                                                           TipoAhorro = @TipoAhorro
                                                           where Codigo = @Codigo",
                                                                    sqlConnection);
                            sqlCommand.Parameters.AddWithValue("@Codigo", ahorro.Codigo);
                            sqlCommand.Parameters.AddWithValue("@CuentaOrigen", ahorro.CuentaOrigen);
                            sqlCommand.Parameters.AddWithValue("@Monto", ahorro.Monto);
                            sqlCommand.Parameters.AddWithValue("@Plazo", ahorro.Plazo);
                            sqlCommand.Parameters.AddWithValue("@TipoAhorro", ahorro.TipoAhorro);
                            

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

                    ahorro = null;
                }
            }

            return Ok(ahorro);
        }

        
        [HttpPost]
        public IHttpActionResult PostAhorros(Ahorro ahorro)
        {

            if (ahorro != null)
            {

                try
                {
                    using (SqlConnection sqlConnection = new
                        SqlConnection(connectionString))
                    {
                        SqlCommand sqlCommand = new SqlCommand(@"insert into Ahorro(CuentaOrigen, Monto,
                                Plazo, TipoAhorro)
                                                            values (@CuentaOrigen, @Monto,
                                                            @Plazo, @TipoAhorro)",
                                                                sqlConnection);

                        sqlCommand.Parameters.AddWithValue("@CuentaOrigen", ahorro.CuentaOrigen);
                        sqlCommand.Parameters.AddWithValue("@Monto", ahorro.Monto);
                        sqlCommand.Parameters.AddWithValue("@Plazo", ahorro.Plazo);
                        sqlCommand.Parameters.AddWithValue("@TipoAhorro", ahorro.TipoAhorro);
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

            return Ok(ahorro);
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
                        SqlCommand(@"delete Ahorro where Codigo = @Codigo", sqlConnection);
                        sqlCommand.Parameters.AddWithValue("@Codigo", id);
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
                    SqlCommand sqlCommand = new SqlCommand(@"select Codigo  from Ahorro                                                        
                                                            where Codigo  = @Codigo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", idSolitude);
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
            catch (Exception exc)
            {
                return false;
            }
            return idSolitudeExists;
        }
    }
}
