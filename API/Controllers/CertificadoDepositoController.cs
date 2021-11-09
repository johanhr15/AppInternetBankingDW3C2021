using API.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [Authorize]
    [RoutePrefix("api/CertificadoDeposito")]
    public class CertificadoDepositoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Certificado_Deposito certificado_Deposito = new Certificado_Deposito();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, CodigoUsuario, CodigoCuenta, CodigoMoneda, Monto, Interes, FechaInicio, FechaFin
                                                           FROM Certificado_Deposito
                                                           WHERE Codigo = @Codigo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        certificado_Deposito.Codigo = sqlDataReader.GetInt32(0);
                        certificado_Deposito.CodigoUsuario = sqlDataReader.GetInt32(1);
                        certificado_Deposito.CodigoCuenta = sqlDataReader.GetInt32(2);
                        certificado_Deposito.CodigoMoneda = sqlDataReader.GetInt32(3);
                        certificado_Deposito.Monto = sqlDataReader.GetInt32(4);
                        certificado_Deposito.Interes = sqlDataReader.GetString(5);
                        certificado_Deposito.FechaInicio = sqlDataReader.GetDateTime(6);
                        certificado_Deposito.FechaFin = sqlDataReader.GetDateTime(7);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(certificado_Deposito);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Certificado_Deposito> certificado_Depositos = new List<Certificado_Deposito>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, CodigoUsuario, CodigoCuenta, CodigoMoneda, Monto, Interes, FechaInicio, FechaFin
                                                           FROM Certificado_Deposito", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Certificado_Deposito certificado_Deposito = new Certificado_Deposito();
                        certificado_Deposito.Codigo = sqlDataReader.GetInt32(0);
                        certificado_Deposito.CodigoUsuario = sqlDataReader.GetInt32(1);
                        certificado_Deposito.CodigoCuenta = sqlDataReader.GetInt32(2);
                        certificado_Deposito.CodigoMoneda = sqlDataReader.GetInt32(3);
                        certificado_Deposito.Monto = sqlDataReader.GetDecimal(4);
                        certificado_Deposito.Interes = sqlDataReader.GetString(5);
                        certificado_Deposito.FechaInicio = sqlDataReader.GetDateTime(6);
                        certificado_Deposito.FechaFin = sqlDataReader.GetDateTime(7);

                        certificado_Depositos.Add(certificado_Deposito);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(certificado_Depositos);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Certificado_Deposito certificado_Deposito)
        {
            if (certificado_Deposito == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Certificado_Deposito (CodigoUsuario, CodigoCuenta, CodigoMoneda, Monto, Interes, FechaInicio, FechaFin)
                                                             VALUES (@CodigoUsuario, @CodigoCuenta, @CodigoMoneda, @Monto, @Interes, @FechaInicio, @FechaFin) ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CodigoUsuario", certificado_Deposito.CodigoUsuario);
                    sqlCommand.Parameters.AddWithValue("@CodigoCuenta", certificado_Deposito.CodigoCuenta);
                    sqlCommand.Parameters.AddWithValue("@CodigoMoneda", certificado_Deposito.CodigoMoneda);
                    sqlCommand.Parameters.AddWithValue("@Monto", certificado_Deposito.Monto);
                    sqlCommand.Parameters.AddWithValue("@Interes", certificado_Deposito.Interes);
                    sqlCommand.Parameters.AddWithValue("@FechaInicio", certificado_Deposito.FechaInicio);
                    sqlCommand.Parameters.AddWithValue("@FechaFin", certificado_Deposito.FechaFin);


                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(certificado_Deposito);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Certificado_Deposito certificado_Deposito)
        {
            if (certificado_Deposito == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE Certificado_Deposito SET CodigoUsuario = @CodigoUsuario,
                                                                             CodigoCuenta = @CodigoCuenta,
                                                                             CodigoMoneda = @CodigoMoneda,
                                                                             Monto = @Monto,
                                                                             Interes = @Interes,
                                                                             FechaInicio = @FechaInicio,
                                                                             FechaFin = @FechaFin
                                                             WHERE Codigo = @Codigo ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", certificado_Deposito.Codigo);
                    sqlCommand.Parameters.AddWithValue("@CodigoUsuario", certificado_Deposito.CodigoUsuario);
                    sqlCommand.Parameters.AddWithValue("@CodigoCuenta", certificado_Deposito.CodigoCuenta);
                    sqlCommand.Parameters.AddWithValue("@CodigoMoneda", certificado_Deposito.CodigoMoneda);
                    sqlCommand.Parameters.AddWithValue("@Monto", certificado_Deposito.Monto);
                    sqlCommand.Parameters.AddWithValue("@Interes", certificado_Deposito.Interes);
                    sqlCommand.Parameters.AddWithValue("@FechaInicio", certificado_Deposito.FechaInicio);
                    sqlCommand.Parameters.AddWithValue("@FechaFin", certificado_Deposito.FechaFin);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(certificado_Deposito);
        }

        [HttpDelete]
        public IHttpActionResult Eliminar(int id)
        {
            if (id < 1)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE Certificado_Deposito WHERE Codigo = @Codigo ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(id);
        }
    }
}