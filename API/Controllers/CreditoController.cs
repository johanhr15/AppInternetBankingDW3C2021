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
    [RoutePrefix("api/Credito")]
    public class CreditoController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Credito credito = new Credito();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT CRE_CODIGO, CRE_COD_CLIENTE, CRE_COD_MONEDA,
                                                            CRE_BANCO, CRE_PLAZO, CRE_INICIO, CRE_MONTO, CRE_INGRESOS
                                                            FROM Credito
                                                            WHERE CRE_CODIGO = @CRE_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CRE_CODIGO", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        credito.CRE_CODIGO = sqlDataReader.GetInt32(0);
                        credito.CRE_COD_CLIENTE = sqlDataReader.GetInt32(1);
                        credito.CRE_COD_MONEDA = sqlDataReader.GetInt32(2);
                        credito.CRE_BANCO = sqlDataReader.GetString(3);
                        credito.CRE_PLAZO = sqlDataReader.GetDecimal(4);
                        credito.CRE_INICIO = sqlDataReader.GetDateTime(5);
                        credito.CRE_MONTO = sqlDataReader.GetDecimal(6);
                        credito.CRE_INGRESOS = sqlDataReader.GetDecimal(7);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return Ok(credito);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Credito> creditos = new List<Credito>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT CRE_CODIGO, CRE_COD_CLIENTE, CRE_COD_MONEDA,
                                                            CRE_BANCO, CRE_PLAZO, CRE_INICIO, CRE_MONTO, CRE_INGRESOS
                                                            FROM credito", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Credito credito = new Credito();
                        credito.CRE_CODIGO = sqlDataReader.GetInt32(0);
                        credito.CRE_COD_CLIENTE = sqlDataReader.GetInt32(1);
                        credito.CRE_COD_MONEDA = sqlDataReader.GetInt32(2);
                        credito.CRE_BANCO = sqlDataReader.GetString(3);
                        credito.CRE_PLAZO = sqlDataReader.GetDecimal(4);
                        credito.CRE_INICIO = sqlDataReader.GetDateTime(5);
                        credito.CRE_MONTO = sqlDataReader.GetDecimal(6);
                        credito.CRE_INGRESOS = sqlDataReader.GetDecimal(7);
                        creditos.Add(credito);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(creditos);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Credito credito)
        {
            if (credito == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Credito(CRE_COD_CLIENTE, CRE_COD_MONEDA,
                                                            CRE_BANCO, CRE_PLAZO, CRE_INICIO, CRE_MONTO, CRE_INGRESOS)
                                                            VALUES (@CRE_COD_CLIENTE, @CRE_COD_MONEDA,
                                                            @CRE_BANCO, @CRE_PLAZO, @CRE_INICIO, @CRE_MONTO, @CRE_INGRESOS )",
                                                            sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CRE_COD_CLIENTE", credito.CRE_COD_CLIENTE);
                    sqlCommand.Parameters.AddWithValue("@CRE_COD_MONEDA", credito.CRE_COD_MONEDA);
                    sqlCommand.Parameters.AddWithValue("@CRE_BANCO", credito.CRE_BANCO);
                    sqlCommand.Parameters.AddWithValue("@CRE_PLAZO", credito.CRE_PLAZO);
                    sqlCommand.Parameters.AddWithValue("@CRE_INICIO", credito.CRE_INICIO);
                    sqlCommand.Parameters.AddWithValue("@CRE_MONTO", credito.CRE_MONTO);
                    sqlCommand.Parameters.AddWithValue("@CRE_INGRESOS", credito.CRE_INGRESOS);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(credito);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Credito credito)
        {
            if (credito == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"UPDATE credito SET CRE_COD_CLIENTE = @CRE_COD_CLIENTE, 
                                                           CRE_COD_MONEDA = @CRE_COD_MONEDA,
                                                           CRE_BANCO = @CRE_BANCO,
                                                           CRE_PLAZO = @CRE_PLAZO,
                                                           CRE_MONTO = @CRE_MONTO,
                                                           CRE_INGRESOS = @CRE_INGRESOS
                                                           WHERE CRE_CODIGO = @CRE_CODIGO",
                                                            sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@CRE_CODIGO", credito.CRE_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@CRE_COD_CLIENTE", credito.CRE_COD_CLIENTE);
                    sqlCommand.Parameters.AddWithValue("@CRE_COD_MONEDA", credito.CRE_COD_MONEDA);
                    sqlCommand.Parameters.AddWithValue("@CRE_BANCO", credito.CRE_BANCO);
                    sqlCommand.Parameters.AddWithValue("@CRE_PLAZO", credito.CRE_PLAZO);
                    sqlCommand.Parameters.AddWithValue("@CRE_INICIO", credito.CRE_INICIO);
                    sqlCommand.Parameters.AddWithValue("@CRE_MONTO", credito.CRE_MONTO);
                    sqlCommand.Parameters.AddWithValue("@CRE_INGRESOS", credito.CRE_INGRESOS);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(credito);
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
                    SqlCommand sqlCommand = new
                        SqlCommand(@"DELETE Credito WHERE CRE_CODIGO = @CRE_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CRE_CODIGO", id);

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
