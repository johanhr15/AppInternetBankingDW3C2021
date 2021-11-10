//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Web.Http;
//using API.Models;

//namespace API.Controllers
//{
//    [Authorize]
//    [RoutePrefix("api/inversion")]
//    public class InversionController : ApiController
//    {
//        private readonly string connectionString = ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString;



//        [HttpGet]
//        public IHttpActionResult GetInversion()
//        {


//            List<Inversion> inversionList = new List<Inversion>();
//            try
//            {
//                using (SqlConnection sqlConnection = new
//                    SqlConnection(connectionString))
//                {
//                    SqlCommand sqlCommand = new SqlCommand(@"select Codigo, CuentaOrigen, FondosInversion, Plazo, CodigoMoneda, Cantidad
//                                                            from Inversiones", sqlConnection);
//                    sqlConnection.Open();
//                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
//                    while (sqlDataReader.Read())
//                    {
//                        Inversion inversiones = new Inversion();
//                        inversiones.Codigo = sqlDataReader.GetInt32(0);
//                        inversiones.CuentaOrigen = sqlDataReader.GetInt32(1);
//                        inversiones.FondosInversion = sqlDataReader.GetString(2);
//                        inversiones.Plazo = sqlDataReader.GetString(3);
//                        inversiones.CodigoMoneda = sqlDataReader.GetString(4);
//                        inversiones.Cantidad = sqlDataReader.GetInt32(5);

//                        inversionList.Add(inversiones);
//                    }

//                    sqlConnection.Close();
//                }
//            }
//            catch (Exception ex)
//            {

//                return InternalServerError(ex);
//            }
//            return Ok(inversionList);
//        }


//        [HttpGet]
//        public IHttpActionResult GetInversion(int id)
//        {
//            Inversiones inversion = new Inversiones();
//            try
//            {
//                using (SqlConnection sqlConnection = new
//                    SqlConnection(connectionString))
//                {
//                    SqlCommand sqlCommand = new SqlCommand(@"select Codigo, CuentaOrigen, FondosInversion, Plazo, CodigoMoneda, Cantidad
//                                                            from Inversiones
//                                                            where Codigo  = @Codigo", sqlConnection);

//                    sqlCommand.Parameters.AddWithValue("@Codigo", id);
//                    sqlConnection.Open();
//                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
//                    while (sqlDataReader.Read())
//                    {
//                        inversion.Codigo = sqlDataReader.GetInt32(0);
//                        inversion.CuentaOrigen = sqlDataReader.GetInt32(1);
//                        inversion.IFondosInversion = sqlDataReader.GetString(2);
//                        inversion.Plazo = sqlDataReader.GetString(3);
//                        inversion.CodigoMoneda = sqlDataReader.GetString(4);
//                        inversion.Cantidad = sqlDataReader.GetInt32(5);
//                    }

//                    sqlConnection.Close();
//                }
//            }
//            catch (Exception ex)
//            {

//                return InternalServerError(ex);
//            }
//            return Ok(inversion);
//        }


//        [HttpPut]
//        public IHttpActionResult PutSolTarjetaCredito(Inversion inversiones)
//        {

//            if (inversiones != null)
//            {
//                if (ValidateInversionExistence(inversiones.Codigo))
//                {

//                    try
//                    {
//                        using (SqlConnection sqlConnection = new
//                            SqlConnection(connectionString))
//                        {
//                            SqlCommand sqlCommand = new
//                                SqlCommand(@"update inversion set CuentaOrigen = @CuentaOrigen,
//                                                           FondosInversion = @FondosInversion,
//                                                           Plazo = @Plazo,
//                                                           CodigoMoneda = @CodigoMoneda,
//                                                           Cantidad = @Cantidad
//                                                           where Codigo = @Codigo",
//                                                                    sqlConnection);
//                            sqlCommand.Parameters.AddWithValue("@Codigo", inversiones.Codigo);
//                            sqlCommand.Parameters.AddWithValue("@CuentaOrigen", inversiones.CuentaOrigen);
//                            sqlCommand.Parameters.AddWithValue("@FondosInversion", inversiones.FondosInversion);
//                            sqlCommand.Parameters.AddWithValue("@Plazo", inversiones.Plazo);
//                            sqlCommand.Parameters.AddWithValue("@CodigoMoneda", inversiones.CodigoMoneda);
//                            sqlCommand.Parameters.AddWithValue("@Cantidad", inversiones.Cantidad);


//                            sqlConnection.Open();
//                            sqlCommand.ExecuteNonQuery();
//                            sqlConnection.Close();
//                        }
//                    }
//                    catch (Exception ex)
//                    {
//                        return InternalServerError(ex);
//                    }


//                }
//                else
//                {

//                    inversiones = null;
//                }
//            }

//            return Ok(inversiones);
//        }


//        [HttpPost]
//        public IHttpActionResult PostInversiones(Inversion inversiones)
//        {

//            if (inversiones != null)
//            {

//                try
//                {
//                    using (SqlConnection sqlConnection = new
//                        SqlConnection(connectionString))
//                    {
//                        SqlCommand sqlCommand = new SqlCommand(@"insert into Inversion(CuentaOrigen, FondosInversion,
//                                PlazoO, CodigoMoneda, Cantidad)
//                                                            values (@CuentaOrigen, @FondosInversion,
//                                                            @Plazo, @CodigoMoneda, @Cantidad)",
//                                                                sqlConnection);

//                        sqlCommand.Parameters.AddWithValue("@CuentaOrigen", inversiones.CuentaOrigen);
//                        sqlCommand.Parameters.AddWithValue("@FondosInversion", inversiones.FondosInversion);
//                        sqlCommand.Parameters.AddWithValue("@Plazo", inversiones.Plazo);
//                        sqlCommand.Parameters.AddWithValue("@CodigoMoneda", inversiones.CodigoMoneda);
//                        sqlCommand.Parameters.AddWithValue("@Cantidad", inversiones.Cantidad);
//                        sqlConnection.Open();
//                        sqlCommand.ExecuteNonQuery();
//                        sqlConnection.Close();
//                    }
//                }
//                catch (Exception ex)
//                {
//                    return InternalServerError(ex);
//                }
//            }

//            return Ok(inversiones);
//        }


//        [HttpDelete]
//        public IHttpActionResult DeleteInversion(int id)
//        {

//            int deletedRows = -1;

//            if (ValidateInversionExistence(id))
//            {
//                try
//                {
//                    using (SqlConnection sqlConnection = new
//                            SqlConnection(connectionString))
//                    {
//                        SqlCommand sqlCommand = new
//                        SqlCommand(@"delete Inversion where Codigo = @Codigo", sqlConnection);
//                        sqlCommand.Parameters.AddWithValue("@Codigo", id);
//                        sqlConnection.Open();
//                        deletedRows = sqlCommand.ExecuteNonQuery();
//                        sqlConnection.Close();
//                    }
//                }
//                catch (Exception ex)
//                {
//                    return InternalServerError(ex);
//                }

//            }

//            return Ok(deletedRows);
//        }


//        private bool ValidateInversionExistence(int idSolitude)
//        {
//            bool idSolitudeExists = false;
//            int idSolitudeResult = 0;

//            try
//            {
//                using (SqlConnection sqlConnection = new
//                    SqlConnection(connectionString))
//                {
//                    SqlCommand sqlCommand = new SqlCommand(@"select Codigo  from Inversion                                                        
//                                                            where Codigo  = @Codigo", sqlConnection);

//                    sqlCommand.Parameters.AddWithValue("@Codigo", idSolitude);
//                    sqlConnection.Open();
//                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
//                    while (sqlDataReader.Read())
//                    {
//                        idSolitudeResult = sqlDataReader.GetInt32(0);

//                    }
//                    sqlConnection.Close();
//                }

//                if (idSolitudeResult == idSolitude)
//                {
//                    idSolitudeExists = true;
//                }
//            }
//            catch (Exception ex)
//            {
//                return false;
//            }
//            return idSolitudeExists;
//        }
//    }
//}