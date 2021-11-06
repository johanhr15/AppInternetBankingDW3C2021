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
    [RoutePrefix("api/trasladoPension")]
    public class trasladoPensionController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetId(int id)
        {
            Traslado_Pensiones traslado_Pensiones = new Traslado_Pensiones();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT TRAS_CODIGO, TRAS_CLIENTE, TRAS_CLIENTE_CORREO,
                                                            TRAS_CLIENTE_TELEFONO, TRAS_ROP_DESTINO, TRAS_FCL_DESTINO
                                                            FROM Traslado_Pensiones
                                                            WHERE TRAS_CODIGO = @TRAS_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@CRE_CODIGO", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        traslado_Pensiones.TRAS_CODIGO = sqlDataReader.GetInt32(0);
                        traslado_Pensiones.TRAS_CLIENTE = sqlDataReader.GetInt32(1);
                        traslado_Pensiones.TRAS_CLIENTE_CORREO = sqlDataReader.GetString(2);
                        traslado_Pensiones.TRAS_CLIENTE_TELEFONO = sqlDataReader.GetString(3);
                        traslado_Pensiones.TRAS_ROP_DESTINO = sqlDataReader.GetString(4);
                        traslado_Pensiones.TRAS_FCL_DESTINO = sqlDataReader.GetString(5);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return Ok(traslado_Pensiones);
        }

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Traslado_Pensiones> traslado_Pensiones = new List<Traslado_Pensiones>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT TRAS_CODIGO, TRAS_CLIENTE, TRAS_CLIENTE_CORREO,
                                                            TRAS_CLIENTE_TELEFONO, TRAS_ROP_DESTINO, TRAS_FCL_DESTINO
                                                            FROM Traslado_Pensiones", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Traslado_Pensiones traslado_Pension = new Traslado_Pensiones();
                        traslado_Pension.TRAS_CODIGO = sqlDataReader.GetInt32(0);
                        traslado_Pension.TRAS_CLIENTE = sqlDataReader.GetInt32(1);
                        traslado_Pension.TRAS_CLIENTE_CORREO = sqlDataReader.GetString(2);
                        traslado_Pension.TRAS_CLIENTE_TELEFONO = sqlDataReader.GetString(3);
                        traslado_Pension.TRAS_ROP_DESTINO = sqlDataReader.GetString(4);
                        traslado_Pension.TRAS_FCL_DESTINO = sqlDataReader.GetString(5);
                        traslado_Pensiones.Add(traslado_Pension);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(traslado_Pensiones);
        }

        [HttpPost]
        public IHttpActionResult Ingresar(Traslado_Pensiones traslado_Pensiones)
        {
            if (traslado_Pensiones == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"INSERT INTO Traslado_Pensiones(TRAS_CLIENTE, TRAS_CLIENTE_CORREO,
                                                            TRAS_CLIENTE_TELEFONO, TRAS_ROP_DESTINO, TRAS_FCL_DESTINO)
                                                            VALUES (@TRAS_CLIENTE, @TRAS_CLIENTE_CORREO,
                                                            @TRAS_CLIENTE_TELEFONO, @TRAS_ROP_DESTINO, @TRAS_FCL_DESTINO)",
                                                            sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@TRAS_CLIENTE", traslado_Pensiones.TRAS_CLIENTE);
                    sqlCommand.Parameters.AddWithValue("@TRAS_CLIENTE_CORREO", traslado_Pensiones.TRAS_CLIENTE_CORREO);
                    sqlCommand.Parameters.AddWithValue("@TRAS_CLIENTE_TELEFONO", traslado_Pensiones.TRAS_CLIENTE_TELEFONO);
                    sqlCommand.Parameters.AddWithValue("@TRAS_ROP_DESTINO", traslado_Pensiones.TRAS_ROP_DESTINO);
                    sqlCommand.Parameters.AddWithValue("@TRAS_FCL_DESTINO", traslado_Pensiones.TRAS_FCL_DESTINO);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(traslado_Pensiones);
        }

        [HttpPut]
        public IHttpActionResult Actualizar(Traslado_Pensiones traslado_Pensiones)
        {
            if (traslado_Pensiones == null)
                return BadRequest();

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new
                        SqlCommand(@"UPDATE Traslado_Pensiones SET TRAS_CLIENTE = @TRAS_CLIENTE, 
                                                           TRAS_CLIENTE_CORREO = @TRAS_CLIENTE_CORREO,
                                                           TRAS_CLIENTE_TELEFONO = @TRAS_CLIENTE_TELEFONO,
                                                           TRAS_ROP_DESTINO = @TRAS_ROP_DESTINO,
                                                           TRAS_FCL_DESTINO = @TRAS_FCL_DESTINO
                                                           WHERE TRAS_CODIGO = @TRAS_CODIGO",
                                                            sqlConnection);
                    sqlCommand.Parameters.AddWithValue("@TRAS_CODIGO", traslado_Pensiones.TRAS_CODIGO);
                    sqlCommand.Parameters.AddWithValue("@TRAS_CLIENTE", traslado_Pensiones.TRAS_CLIENTE);
                    sqlCommand.Parameters.AddWithValue("@TRAS_CLIENTE_CORREO", traslado_Pensiones.TRAS_CLIENTE_CORREO);
                    sqlCommand.Parameters.AddWithValue("@TRAS_CLIENTE_TELEFONO", traslado_Pensiones.TRAS_CLIENTE_TELEFONO);
                    sqlCommand.Parameters.AddWithValue("@TRAS_ROP_DESTINO", traslado_Pensiones.TRAS_ROP_DESTINO);
                    sqlCommand.Parameters.AddWithValue("@TRAS_FCL_DESTINO", traslado_Pensiones.TRAS_FCL_DESTINO);

                    sqlConnection.Open();

                    int filasAfectadas = sqlCommand.ExecuteNonQuery();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok(traslado_Pensiones);
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
                        SqlCommand(@"DELETE Traslado_Pensiones WHERE TRAS_CODIGO = @TRAS_CODIGO", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@TRAS_CODIGO", id);

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
