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
    [RoutePrefix("api/SolicitudCompraCasa")]
    public class SolicitudCompraCasaController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Compra_Casa solicitud_compra_casa = new Compra_Casa();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, CodigoUsuario, CodigoMoneda, TipoCasa, TasaInteres, ValorCasa, Prima, PlazoMeses, FechaInicio, Estado
                                                           FROM Compra_Casa
                                                           WHERE Codigo = @Codigo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", id);
                    sqlConnection.Open();

                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();

                    while (sqlDataReader.Read())
                    {
                        solicitud_compra_casa.Codigo = sqlDataReader.GetInt32(0);
                        solicitud_compra_casa.CodigoUsuario = sqlDataReader.GetInt32(1);
                        solicitud_compra_casa.CodigoMoneda = sqlDataReader.GetInt32(2);
                        solicitud_compra_casa.TipoCasa = sqlDataReader.GetString(3);
                        solicitud_compra_casa.TasaInteres = sqlDataReader.GetInt32(4);
                        solicitud_compra_casa.ValorCasa = sqlDataReader.GetInt32(5);
                        solicitud_compra_casa.Prima = sqlDataReader.GetInt32(6);
                        solicitud_compra_casa.PlazoMeses = sqlDataReader.GetInt32(7);
                        solicitud_compra_casa.FechaInicio = sqlDataReader.GetDateTime(8);
                        solicitud_compra_casa.Estado = sqlDataReader.GetString(9);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(solicitud_compra_casa);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Compra_Casa> solicitud_Compra_Casas = new List<Compra_Casa>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, CodigoUsuario, CodigoMoneda, TipoCasa, TasaInteres, ValorCasa, Prima, PlazoMeses, FechaInicio, Estado
                                                           FROM Compra_Casa", sqlConnection);

                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Compra_Casa solicitud_compra_casa = new Compra_Casa();
                        solicitud_compra_casa.Codigo = sqlDataReader.GetInt32(0);
                        solicitud_compra_casa.CodigoUsuario = sqlDataReader.GetInt32(1);
                        solicitud_compra_casa.CodigoMoneda = sqlDataReader.GetInt32(2);
                        solicitud_compra_casa.TipoCasa = sqlDataReader.GetString(3);
                        solicitud_compra_casa.TasaInteres = sqlDataReader.GetInt32(4);
                        solicitud_compra_casa.ValorCasa = sqlDataReader.GetInt32(5);
                        solicitud_compra_casa.Prima = sqlDataReader.GetInt32(6);
                        solicitud_compra_casa.PlazoMeses = sqlDataReader.GetInt32(7);
                        solicitud_compra_casa.FechaInicio = sqlDataReader.GetDateTime(8);
                        solicitud_compra_casa.Estado = sqlDataReader.GetString(9);

                        solicitud_Compra_Casas.Add(solicitud_compra_casa);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(solicitud_Compra_Casas);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Compra_Casa solicitud_compra_casa)
        {
            if (solicitud_compra_casa == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Compra_Casa (CodigoUsuario, CodigoMoneda, TipoCasa, TasaInteres, ValorCasa, Prima, PlazoMeses, FechaInicio, Estado)
                                                             VALUES (@CodigoUsuario, @CodigoMoneda, @TipoCasa, @TasaInteres, @ValorCasa, @Prima, @PlazoMeses, @FechaInicio, @Estado) ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CodigoUsuario", solicitud_compra_casa.CodigoUsuario);
                    sqlCommand.Parameters.AddWithValue("@CodigoMoneda", solicitud_compra_casa.CodigoMoneda);
                    sqlCommand.Parameters.AddWithValue("@TipoCasa", solicitud_compra_casa.TipoCasa);
                    sqlCommand.Parameters.AddWithValue("@TasaInteres", solicitud_compra_casa.TasaInteres);
                    sqlCommand.Parameters.AddWithValue("@ValorCasa", solicitud_compra_casa.ValorCasa);
                    sqlCommand.Parameters.AddWithValue("@Prima", solicitud_compra_casa.Prima);
                    sqlCommand.Parameters.AddWithValue("@PlazoMeses", solicitud_compra_casa.PlazoMeses);
                    sqlCommand.Parameters.AddWithValue("@FechaInicio", solicitud_compra_casa.FechaInicio);
                    sqlCommand.Parameters.AddWithValue("@Estado", solicitud_compra_casa.Estado);


                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(solicitud_compra_casa);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Compra_Casa solicitud_compra_casa)
        {
            if (solicitud_compra_casa == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"UPDATE Compra_Casa SET CodigoUsuario = @CodigoUsuario,
                                                                             CodigoMoneda = @CodigoMoneda,
                                                                             TipoCasa = @TipoCasa,
                                                                             TasaInteres = @TasaInteres,
                                                                             ValorCasa = @ValorCasa,
                                                                             Prima = @Prima,
                                                                             PlazoMeses = @PlazoMeses,
                                                                             FechaInicio = @FechaInicio,
                                                                             Estado = @Estado
                                                             WHERE Codigo = @Codigo ", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@Codigo", solicitud_compra_casa.Codigo);
                    sqlCommand.Parameters.AddWithValue("@CodigoUsuario", solicitud_compra_casa.CodigoUsuario);
                    sqlCommand.Parameters.AddWithValue("@CodigoMoneda", solicitud_compra_casa.CodigoMoneda);
                    sqlCommand.Parameters.AddWithValue("@TipoCasa", solicitud_compra_casa.TipoCasa);
                    sqlCommand.Parameters.AddWithValue("@TasaInteres", solicitud_compra_casa.TasaInteres);
                    sqlCommand.Parameters.AddWithValue("@ValorCasa", solicitud_compra_casa.ValorCasa);
                    sqlCommand.Parameters.AddWithValue("@Prima", solicitud_compra_casa.Prima);
                    sqlCommand.Parameters.AddWithValue("@PlazoMeses", solicitud_compra_casa.PlazoMeses);
                    sqlCommand.Parameters.AddWithValue("@FechaInicio", solicitud_compra_casa.FechaInicio);
                    sqlCommand.Parameters.AddWithValue("@Estado", solicitud_compra_casa.Estado);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(solicitud_compra_casa);
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
                    SqlCommand sqlCommand = new SqlCommand(@"DELETE Compra_Casa WHERE Codigo = @Codigo ", sqlConnection);

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