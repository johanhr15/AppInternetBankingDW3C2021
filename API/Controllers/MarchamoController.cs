using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.Http;
using API.Models;

namespace API.Controllers
{
    [Authorize]
    [RoutePrefix("api/Marchamo")]
    public class MarchamoController : ApiController
    {

        private readonly string connectionString = ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString;

        // GET: api/Marchamo
        [HttpGet]
        public IHttpActionResult GetMarchamo()
        {
            
            List<Marchamo> marchamoList = new List<Marchamo>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"select idMarchamo, idPlaca, monto, seguroVehiculo , aporteCSV, impuestoPropiedad, 
                                                            impuestoMunicipalidad, timbreFS, iva 
                                                            from Marchamo", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Marchamo marchamo = new Marchamo();
                        marchamo.idMarchamo = sqlDataReader.GetInt32(0);
                        marchamo.idPlaca = sqlDataReader.GetString(1);
                        marchamo.monto = sqlDataReader.GetDecimal(2);
                        marchamo.seguroVehiculo = sqlDataReader.GetDecimal(3);
                        marchamo.aporteCSV = sqlDataReader.GetDecimal(4);
                        marchamo.impuestoPropiedad = sqlDataReader.GetDecimal(5);
                        marchamo.impuestoMunicipalidad = sqlDataReader.GetDecimal(6);
                        marchamo.timbreFS = sqlDataReader.GetDecimal(7);
                        marchamo.iva = sqlDataReader.GetDecimal(8);
                        marchamoList.Add(marchamo);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return Ok(marchamoList);
        }

        // GET: api/Marchamo/id
        [HttpGet]
        public IHttpActionResult GetMarchamo(int id)
        {
            Marchamo marchamo = new Marchamo();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"select idMarchamo, idPlaca, monto, seguroVehiculo , aporteCSV, impuestoPropiedad, 
                                                            impuestoMunicipalidad, timbreFS, iva 
                                                            from marchamo
                                                            where idMarchamo  = @idMarchamo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@idMarchamo", id);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        marchamo.idMarchamo = sqlDataReader.GetInt32(0);
                        marchamo.idPlaca = sqlDataReader.GetString(1);
                        marchamo.monto = sqlDataReader.GetDecimal(2);
                        marchamo.seguroVehiculo = sqlDataReader.GetDecimal(3);
                        marchamo.aporteCSV = sqlDataReader.GetDecimal(4);
                        marchamo.impuestoPropiedad = sqlDataReader.GetDecimal(5);
                        marchamo.impuestoMunicipalidad = sqlDataReader.GetDecimal(6);
                        marchamo.timbreFS = sqlDataReader.GetDecimal(7);
                        marchamo.iva = sqlDataReader.GetDecimal(8);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
            return Ok(marchamo);
        }

        // PUT: api/Marchamo
        [HttpPut]
        public IHttpActionResult PutMarchamo(Marchamo marchamo)
        {

            if (marchamo != null)
            {
                if (ValidateMarchamoExistence(marchamo.idMarchamo))
                {

                    try
                    {
                        using (SqlConnection sqlConnection = new
                            SqlConnection(connectionString))
                        {
                            SqlCommand sqlCommand = new
                                SqlCommand(@"update marchamo set idPlaca = @idPlaca, 
                                                           monto = @monto,
                                                           seguroVehiculo = @seguroVehiculo,
                                                           aporteCSV = @aporteCSV,
                                                           impuestoPropiedad = @impuestoPropiedad,
                                                           impuestoMunicipalidad = @impuestoMunicipalidad, 
                                                          timbreFS = @timbreFS,
                                                           iva = @iva
                                                           where idMarchamo = @idMarchamo",
                                                                    sqlConnection);
                            sqlCommand.Parameters.AddWithValue("@idMarchamo", marchamo.idMarchamo);
                            sqlCommand.Parameters.AddWithValue("@idPlaca", marchamo.idPlaca);
                            sqlCommand.Parameters.AddWithValue("@monto", marchamo.monto);
                            sqlCommand.Parameters.AddWithValue("@seguroVehiculo", marchamo.seguroVehiculo);
                            sqlCommand.Parameters.AddWithValue("@aporteCSV", marchamo.aporteCSV);
                            sqlCommand.Parameters.AddWithValue("@impuestoPropiedad", marchamo.impuestoPropiedad);
                            sqlCommand.Parameters.AddWithValue("@impuestoMunicipalidad", marchamo.impuestoMunicipalidad);
                            sqlCommand.Parameters.AddWithValue("@timbreFS", marchamo.timbreFS);
                            sqlCommand.Parameters.AddWithValue("@iva", marchamo.iva);

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

                    marchamo = null;
                }

            }
            return Ok(marchamo);
        }

        // POST: api/Marchamo
        [HttpPost]
        public IHttpActionResult PostMarchamo(Marchamo marchamo)
        {


            if (marchamo != null)
            {
                if (!ValidatePlateExistence(marchamo.idPlaca))
                {

                    try
                    {
                        using (SqlConnection sqlConnection = new
                            SqlConnection(connectionString))
                        {
                            SqlCommand sqlCommand = new SqlCommand(@"insert into marchamo(idPlaca, monto, seguroVehiculo , aporteCSV, impuestoPropiedad, 
                                                            impuestoMunicipalidad, timbreFS, iva)
                                                            values (@idPlaca, @monto, @seguroVehiculo, @aporteCSV, @impuestoPropiedad,
                                                                    @impuestoMunicipalidad, @timbreFS, @iva)",
                                                                    sqlConnection);

                            sqlCommand.Parameters.AddWithValue("@idPlaca", marchamo.idPlaca);
                            sqlCommand.Parameters.AddWithValue("@monto", marchamo.monto);
                            sqlCommand.Parameters.AddWithValue("@seguroVehiculo", marchamo.seguroVehiculo);
                            sqlCommand.Parameters.AddWithValue("@aporteCSV", marchamo.aporteCSV);
                            sqlCommand.Parameters.AddWithValue("@impuestoPropiedad", marchamo.impuestoPropiedad);
                            sqlCommand.Parameters.AddWithValue("@impuestoMunicipalidad", marchamo.impuestoMunicipalidad);
                            sqlCommand.Parameters.AddWithValue("@timbreFS", marchamo.timbreFS);
                            sqlCommand.Parameters.AddWithValue("@iva", marchamo.iva);
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

                    marchamo = null;
                }
            }
            return Ok(marchamo);
        }

        // DELETE: api/Marchamo/id
        [HttpDelete]
        public IHttpActionResult DeleteMarchamo(int id)
        {

            int deletedRows = -1;


                if (ValidateMarchamoExistence(id))
            {
                try
                {
                    using (SqlConnection sqlConnection = new
                            SqlConnection(connectionString))
                    {
                        SqlCommand sqlCommand = new
                        SqlCommand(@"delete Marchamo where idMarchamo = @idMarchamo", sqlConnection);
                        sqlCommand.Parameters.AddWithValue("@idMarchamo", id);
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



        private bool ValidatePlateExistence(string idPlaca)
        {
            bool carPlateExists = false;
            string carPlateResult = string.Empty;

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"select idPlaca   from marchamo                                                       
                                                            where idPlaca  = @idPlaca", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@idPlaca", idPlaca);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        carPlateResult = sqlDataReader.GetString(0);
                       
                    }
                    sqlConnection.Close();
                }

                if (carPlateResult ==  idPlaca) {
                    carPlateExists = true;
                }
            }
            catch (Exception)
            {
                return false ;
            }
            return carPlateExists;
        }



        private bool ValidateMarchamoExistence(int idMarchamo)
        {
            bool idMarchamoExists = false;
            int idMarchamoResult = 0;

            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(connectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"select idMarchamo  from marchamo                                                        
                                                            where idMarchamo  = @idMarchamo", sqlConnection);

                    sqlCommand.Parameters.AddWithValue("@idMarchamo", idMarchamo);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        idMarchamoResult = sqlDataReader.GetInt32(0);

                    }
                    sqlConnection.Close();
                }

                if (idMarchamoResult == idMarchamo)
                {
                    idMarchamoExists = true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return idMarchamoExists;
        }


    }
}