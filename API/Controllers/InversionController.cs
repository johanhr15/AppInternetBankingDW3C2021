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


            List<Inversiones> inversionList = new List<Inversiones>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"select INV_CODIGO, INV_CUENTAORIGEN, INV_FONDOSINV, INV_PLAZO, INV_MONEDA, INV_MONTO
                                                            from Inversiones", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Inversiones inversiones = new Inversiones();
                        inversiones.INV_CODIGO = sqlDataReader.GetInt32(0);
                        inversiones.INV_CUENTAORIGEN = sqlDataReader.GetInt32(1);
                        inversiones.INV_FONDOSINV = sqlDataReader.GetString(2);
                        inversiones.INV_PLAZO = sqlDataReader.GetString(3);
                        inversiones.INV_MONEDA = sqlDataReader.GetString(4);
                        inversiones.INV_MONTO = sqlDataReader.GetInt32(5);

                        inversionList.Add(inversiones);
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
            Inversiones inversion = new Inversiones();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"select INV_CODIGO, INV_CUENTAORIGEN, INV_FONDOSINV, AH_PLAZO, INV_MONEDA, INV_MONTO
                                                            from Inversiones
                                                            where INV_CODIGO  = @INV_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@AH_CODIGO", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        inversion.INV_CODIGO = sqlDataReader.GetInt32(0);
                        inversion.INV_CUENTAORIGEN = sqlDataReader.GetInt32(1);
                        inversion.INV_FONDOSINV = sqlDataReader.GetString(2);
                        inversion.INV_PLAZO = sqlDataReader.GetString(3);
                        inversion.INV_MONEDA = sqlDataReader.GetString(4);
                        inversion.INV_MONTO = sqlDataReader.GetInt32(5);
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
        public IHttpActionResult PutSolTarjetaCredito(Inversiones inversiones)
        {

            if (inversiones != null)
            {
                if (ValidateInversionExistence(inversiones.INV_CODIGO))
                {

                    try
                    {
                        using (SqlConnection sqlConnection = new
                            SqlConnection(connectionString))
                        {
                            SqlCommand sqlCommand = new
                                SqlCommand(@"update inversion set INV_CUENTAORIGEN = @INV_CUENTAORIGEN,
                                                           INV_FONDOSINV = @INV_FONDOSINV,
                                                           INV_PLAZO = @INV_PLAZO,
                                                           INV_MONEDA = @INV_MONEDA,
                                                           INV_MONTO = @INV_MONTO
                                                           where INV_CODIGO = @INV_CODIGO",
                                                                    sqlConnection);
                            sqlCommand.Parameters.AddWithValue("@INV_CODIGO", inversiones.INV_CODIGO);
                            sqlCommand.Parameters.AddWithValue("@INV_CUENTAORIGEN", inversiones.INV_CUENTAORIGEN);
                            sqlCommand.Parameters.AddWithValue("@INV_FONDOSINV", inversiones.INV_FONDOSINV);
                            sqlCommand.Parameters.AddWithValue("@INV_PLAZO", inversiones.INV_PLAZO);
                            sqlCommand.Parameters.AddWithValue("@INV_MONEDA", inversiones.INV_MONEDA);
                            sqlCommand.Parameters.AddWithValue("@INV_MONTO", inversiones.INV_MONTO);


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

                    inversiones = null;
                }
            }

            return Ok(inversiones);
        }


        [HttpPost]
        public IHttpActionResult PostInversiones(Inversiones inversiones)
        {

            if (inversiones != null)
            {

                try
                {
                    using (SqlConnection sqlConnection = new
                        SqlConnection(connectionString))
                    {
                        SqlCommand sqlCommand = new SqlCommand(@"insert into Inversion(INV_CUENTAORIGEN, INV_FONDOSINV,
                                INV_PLAZO, INV_MONEDA, INV_MONTO)
                                                            values (@INV_CUENTAORIGEN, @INV_FONDOSINV,
                                                            @INV_PLAZO, @INV_MONEDA, @INV_MONTO)",
                                                                sqlConnection);

                        sqlCommand.Parameters.AddWithValue("@INV_CUENTAORIGEN", inversiones.INV_CUENTAORIGEN);
                        sqlCommand.Parameters.AddWithValue("@INV_FONDOSINV", inversiones.INV_FONDOSINV);
                        sqlCommand.Parameters.AddWithValue("@INV_PLAZO", inversiones.INV_PLAZO);
                        sqlCommand.Parameters.AddWithValue("@INV_MONEDA", inversiones.INV_MONEDA);
                        sqlCommand.Parameters.AddWithValue("@INV_MONTO", inversiones.INV_MONTO);
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

            return Ok(inversiones);
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
                        SqlCommand(@"delete Inversion where INV_CODIGO = @INV_CODIGO", sqlConnection);
                        sqlCommand.Parameters.AddWithValue("@INV_CODIGO", id);
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
                    SqlCommand sqlCommand = new SqlCommand(@"select INV_CODIGO  from Inversion                                                        
                                                            where INV_CODIGO  = @INV_AHORRO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@INV_CODIGO", idSolitude);
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
