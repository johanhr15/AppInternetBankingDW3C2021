using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using API.Models;

namespace API.Controllers
{
    [Authorize]
    [RoutePrefix("api/inversion")]
    public class InversionController : ApiController
    {
        private readonly string connectionString = ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString;



        [HttpGet]
        public IHttpActionResult GetInversion()
        {


            List<Inversion> inversionList = new List<Inversion>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"select Codigo, CuentaOrigen, FondosInversion, Plazo, CodigoMoneda, Cantidad
                                                            from Inversion", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Inversion inversion = new Inversion();
                        inversion.Codigo = sqlDataReader.GetInt32(0);
                        inversion.CuentaOrigen = sqlDataReader.GetInt32(1);
                        inversion.FondosInversion = sqlDataReader.GetString(2);
                        inversion.Plazo = sqlDataReader.GetString(3);
                        inversion.CodigoMoneda = sqlDataReader.GetInt32(4);
                        inversion.Cantidad = sqlDataReader.GetInt32(5);

                        inversionList.Add(inversion);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return Ok(inversionList);
        }


        [HttpGet]
        public IHttpActionResult GetInversion(int id)
        {
            Inversion inversion = new Inversion();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"select Codigo, CuentaOrigen, FondosInversion, Plazo, CodigoMoneda, Cantidad
                                                            from Inversion
                                                            where Codigo  = @Codigo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        inversion.Codigo = sqlDataReader.GetInt32(0);
                        inversion.CuentaOrigen = sqlDataReader.GetInt32(1);
                        inversion.FondosInversion = sqlDataReader.GetString(2);
                        inversion.Plazo = sqlDataReader.GetString(3);
                        inversion.CodigoMoneda = sqlDataReader.GetInt32(4);
                        inversion.Cantidad = sqlDataReader.GetInt32(5);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return Ok(inversion);
        }


        [HttpPut]
        public IHttpActionResult PutInversion(Inversion inversion)
        {

            if (inversion != null)
            {
                if (ValidateInversionExistence(inversion.Codigo))
                {

                    try
                    {
                        using (SqlConnection sqlConnection = new
                            SqlConnection(connectionString))
                        {
                            SqlCommand sqlCommand = new
                                SqlCommand(@"update inversion set CuentaOrigen = @CuentaOrigen,
                                                           FondosInversion = @FondosInversion,
                                                           Plazo = @Plazo,
                                                           CodigoMoneda = @CodigoMoneda,
                                                           Cantidad = @Cantidad
                                                           where Codigo = @Codigo",
                                                                    sqlConnection);
                            sqlCommand.Parameters.AddWithValue("@Codigo", inversion.Codigo);
                            sqlCommand.Parameters.AddWithValue("@CuentaOrigen", inversion.CuentaOrigen);
                            sqlCommand.Parameters.AddWithValue("@FondosInversion", inversion.FondosInversion);
                            sqlCommand.Parameters.AddWithValue("@Plazo", inversion.Plazo);
                            sqlCommand.Parameters.AddWithValue("@CodigoMoneda", inversion.CodigoMoneda);
                            sqlCommand.Parameters.AddWithValue("@Cantidad", inversion.Cantidad);


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

                    inversion = null;
                }
            }

            return Ok(inversion);
        }


        [HttpPost]
        public IHttpActionResult PostInversion(Inversion inversion)
        {

            if (inversion != null)
            {

                try
                {
                    using (SqlConnection sqlConnection = new
                        SqlConnection(connectionString))
                    {
                        SqlCommand sqlCommand = new SqlCommand(@"insert into Inversion(CuentaOrigen, FondosInversion,
                                PlazoO, CodigoMoneda, Cantidad)
                                                            values (@CuentaOrigen, @FondosInversion,
                                                            @Plazo, @CodigoMoneda, @Cantidad)",
                                                                sqlConnection);

                        sqlCommand.Parameters.AddWithValue("@CuentaOrigen", inversion.CuentaOrigen);
                        sqlCommand.Parameters.AddWithValue("@FondosInversion", inversion.FondosInversion);
                        sqlCommand.Parameters.AddWithValue("@Plazo", inversion.Plazo);
                        sqlCommand.Parameters.AddWithValue("@CodigoMoneda", inversion.CodigoMoneda);
                        sqlCommand.Parameters.AddWithValue("@Cantidad", inversion.Cantidad);
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

            return Ok(inversion);
        }


        [HttpDelete]
        public IHttpActionResult DeleteInversion(int id)
        {

            int deletedRows = -1;

            if (ValidateInversionExistence(id))
            {
                try
                {
                    using (SqlConnection sqlConnection = new
                            SqlConnection(connectionString))
                    {
                        SqlCommand sqlCommand = new
                        SqlCommand(@"delete Inversion where Codigo = @Codigo", sqlConnection);
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


        private bool ValidateInversionExistence(int idSolitude)
        {
            bool idSolitudeExists = false;
            int idSolitudeResult = 0;

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"select Codigo  from Inversion                                                        
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
