using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using API.Models;

namespace API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Sol_Tarjeta_Credito")]
    public class Sol_Tarjeta_CreditoController : ApiController
    {

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString;


        // GET: api/Sol_Tarjeta_Credito
        [HttpGet]
        public IHttpActionResult GetSolTarjetaCredito()
        {


            List<Sol_Tarjeta_Credito> tarjetaCreditoList = new List<Sol_Tarjeta_Credito>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"select idSolTarjeta, cedula, fechaNacimiento, ingresoMensual, condicionLaboral, idTipoTarjeta,nombreEmpresa,
                                                            telefonoTrabajo,puesto,tiempoLaborar,telefonoContacto
                                                            from Sol_Tarjeta_Credito", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Sol_Tarjeta_Credito sol_Tarjeta_Credito = new Sol_Tarjeta_Credito();
                        sol_Tarjeta_Credito.idSolTarjeta = sqlDataReader.GetInt32(0);
                        sol_Tarjeta_Credito.cedula = sqlDataReader.GetInt32(1);
                        sol_Tarjeta_Credito.fechaNacimiento = sqlDataReader.GetDateTime(2);
                        sol_Tarjeta_Credito.ingresoMensual = sqlDataReader.GetDecimal(3);
                        sol_Tarjeta_Credito.condicionLaboral = sqlDataReader.GetString(4);
                        sol_Tarjeta_Credito.idTipoTarjeta = sqlDataReader.GetInt32(5);
                        sol_Tarjeta_Credito.nombreEmpresa = sqlDataReader.GetString(6);
                        sol_Tarjeta_Credito.telefonoTrabajo = sqlDataReader.GetInt32(7);
                        sol_Tarjeta_Credito.puesto = sqlDataReader.GetString(8);
                        sol_Tarjeta_Credito.tiempoLaborar = sqlDataReader.GetString(9);
                        sol_Tarjeta_Credito.telefonoContacto = sqlDataReader.GetInt32(10);
                        tarjetaCreditoList.Add(sol_Tarjeta_Credito);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return Ok(tarjetaCreditoList);
        }

        // GET: api/Sol_Tarjeta_Credito/id
        [HttpGet]
        public IHttpActionResult GetTarjetaCredito(int id)
        {
            Sol_Tarjeta_Credito sol_Tarjeta_Credito = new Sol_Tarjeta_Credito();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"select idSolTarjeta, cedula, fechaNacimiento, ingresoMensual, condicionLaboral, idTipoTarjeta,nombreEmpresa,
                                                            telefonoTrabajo,puesto,tiempoLaborar,telefonoContacto
                                                            from Sol_Tarjeta_Credito
                                                            where idSolTarjeta  = @idSolTarjeta", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@idSolTarjeta", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        sol_Tarjeta_Credito.idSolTarjeta = sqlDataReader.GetInt32(0);
                        sol_Tarjeta_Credito.cedula = sqlDataReader.GetInt32(1);
                        sol_Tarjeta_Credito.fechaNacimiento = sqlDataReader.GetDateTime(2);
                        sol_Tarjeta_Credito.ingresoMensual = sqlDataReader.GetDecimal(3);
                        sol_Tarjeta_Credito.condicionLaboral = sqlDataReader.GetString(4);
                        sol_Tarjeta_Credito.idTipoTarjeta = sqlDataReader.GetInt32(5);
                        sol_Tarjeta_Credito.nombreEmpresa = sqlDataReader.GetString(6);
                        sol_Tarjeta_Credito.telefonoTrabajo = sqlDataReader.GetInt32(7);
                        sol_Tarjeta_Credito.puesto = sqlDataReader.GetString(8);
                        sol_Tarjeta_Credito.tiempoLaborar = sqlDataReader.GetString(9);
                        sol_Tarjeta_Credito.telefonoContacto = sqlDataReader.GetInt32(10);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return Ok(sol_Tarjeta_Credito);
        }

        // PUT: api/Sol_Tarjeta_Credito
        [HttpPut]
        public IHttpActionResult PutSolTarjetaCredito(Sol_Tarjeta_Credito sol_Tarjeta_Credito)
        {

            if (sol_Tarjeta_Credito != null)
            {
                if (ValidateTarjetaCreditoExistence(sol_Tarjeta_Credito.idSolTarjeta))
                {

                    try
                    {
                        using (SqlConnection sqlConnection = new
                            SqlConnection(connectionString))
                        {
                            SqlCommand sqlCommand = new
                                SqlCommand(@"update Sol_Tarjeta_Credito set cedula = @cedula,
                                                           fechaNacimiento = @fechaNacimiento,
                                                           ingresoMensual = @ingresoMensual,
                                                           condicionLaboral = @condicionLaboral,
                                                           idTipoTarjeta = @idTipoTarjeta, 
                                                           nombreEmpresa = @nombreEmpresa,
                                                           telefonoTrabajo = @telefonoTrabajo,
                                                            puesto = @puesto,
                                                            tiempoLaborar = @tiempoLaborar,
                                                            telefonoContacto = @telefonoContacto
                                                           where idSolTarjeta = @idSolTarjeta",
                                                                    sqlConnection);
                            sqlCommand.Parameters.AddWithValue("@idSolTarjeta", sol_Tarjeta_Credito.idSolTarjeta);
                            sqlCommand.Parameters.AddWithValue("@cedula", sol_Tarjeta_Credito.cedula);
                            sqlCommand.Parameters.AddWithValue("@fechaNacimiento", sol_Tarjeta_Credito.fechaNacimiento);
                            sqlCommand.Parameters.AddWithValue("@ingresoMensual", sol_Tarjeta_Credito.ingresoMensual);
                            sqlCommand.Parameters.AddWithValue("@condicionLaboral", sol_Tarjeta_Credito.condicionLaboral);
                            sqlCommand.Parameters.AddWithValue("@idTipoTarjeta", sol_Tarjeta_Credito.idTipoTarjeta);
                            sqlCommand.Parameters.AddWithValue("@nombreEmpresa", sol_Tarjeta_Credito.nombreEmpresa);
                            sqlCommand.Parameters.AddWithValue("@telefonoTrabajo", sol_Tarjeta_Credito.telefonoTrabajo);
                            sqlCommand.Parameters.AddWithValue("@puesto", sol_Tarjeta_Credito.puesto);
                            sqlCommand.Parameters.AddWithValue("@tiempoLaborar", sol_Tarjeta_Credito.tiempoLaborar);
                            sqlCommand.Parameters.AddWithValue("@telefonoContacto", sol_Tarjeta_Credito.telefonoContacto);

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

                    sol_Tarjeta_Credito = null;
                }
            }

            return Ok(sol_Tarjeta_Credito);
        }

        // POST: api/Marchamo
        [HttpPost]
        public IHttpActionResult PostSolTarjetaCredito(Sol_Tarjeta_Credito sol_Tarjeta_Credito)
        {

            if (sol_Tarjeta_Credito != null)
            {

                try
                {
                    using (SqlConnection sqlConnection = new
                        SqlConnection(connectionString))
                    {
                        SqlCommand sqlCommand = new SqlCommand(@"insert into Sol_Tarjeta_Credito(cedula, fechaNacimiento,
                                ingresoMensual, condicionLaboral, idTipoTarjeta,nombreEmpresa,
                                telefonoTrabajo,puesto,tiempoLaborar,telefonoContacto)
                                                            values (@cedula, @fechaNacimiento,
                                                            @ingresoMensual, @condicionLaboral, @idTipoTarjeta,@nombreEmpresa,
                                                            @telefonoTrabajo, @puesto, @tiempoLaborar, @telefonoContacto)",
                                                                sqlConnection);

                        sqlCommand.Parameters.AddWithValue("@cedula", sol_Tarjeta_Credito.cedula);
                        sqlCommand.Parameters.AddWithValue("@fechaNacimiento", sol_Tarjeta_Credito.fechaNacimiento);
                        sqlCommand.Parameters.AddWithValue("@ingresoMensual", sol_Tarjeta_Credito.ingresoMensual);
                        sqlCommand.Parameters.AddWithValue("@condicionLaboral", sol_Tarjeta_Credito.condicionLaboral);
                        sqlCommand.Parameters.AddWithValue("@idTipoTarjeta", sol_Tarjeta_Credito.idTipoTarjeta);
                        sqlCommand.Parameters.AddWithValue("@nombreEmpresa", sol_Tarjeta_Credito.nombreEmpresa);
                        sqlCommand.Parameters.AddWithValue("@telefonoTrabajo", sol_Tarjeta_Credito.telefonoTrabajo);
                        sqlCommand.Parameters.AddWithValue("@puesto", sol_Tarjeta_Credito.puesto);
                        sqlCommand.Parameters.AddWithValue("@tiempoLaborar", sol_Tarjeta_Credito.tiempoLaborar);
                        sqlCommand.Parameters.AddWithValue("@telefonoContacto", sol_Tarjeta_Credito.telefonoContacto);
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

            return Ok(sol_Tarjeta_Credito);
        }

        // DELETE: api/Marchamo/5
        [HttpDelete]
        public IHttpActionResult DeleteSolTarjetaCredito(int id)
        {

            int deletedRows = -1;

            if (ValidateTarjetaCreditoExistence(id))
            {
                try
                {
                    using (SqlConnection sqlConnection = new
                            SqlConnection(connectionString))
                    {
                        SqlCommand sqlCommand = new
                        SqlCommand(@"delete Sol_Tarjeta_Credito where idSolTarjeta = @idSolTarjeta", sqlConnection);
                        sqlCommand.Parameters.AddWithValue("@idSolTarjeta", id);
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


        private bool ValidateTarjetaCreditoExistence(int idSolitude)
        {
            bool idSolitudeExists = false;
            int idSolitudeResult = 0;

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"select idSolTarjeta  from Sol_Tarjeta_Credito                                                        
                                                            where idSolTarjeta  = @idSolTarjeta", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@idSolTarjeta", idSolitude);
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
            catch (Exception)
            {
                return false;
            }
            return idSolitudeExists;
        }



    }
}