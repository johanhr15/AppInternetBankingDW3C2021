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
    [RoutePrefix("api/Usuario")]
    public class UsuarioController : ApiController
    {
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();
            try
            {
                using (SqlConnection sqlConnection = new
                    SqlConnection(ConfigurationManager.ConnectionStrings["INTERNET_BANKING"].ConnectionString))
                {
                    SqlCommand sqlCommand = new SqlCommand(@"SELECT Codigo, Identificacion, Nombre, Username, 
	                                                        Password, Email, FechaNacimiento, Estado
                                                        FROM   Usuario", sqlConnection);
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = sqlCommand.ExecuteReader();
                    while (sqlDataReader.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.Codigo = sqlDataReader.GetInt32(0);
                        usuario.Identificacion = sqlDataReader.GetString(1);
                        usuario.Nombre = sqlDataReader.GetString(2);
                        usuario.Username = sqlDataReader.GetString(3);
                        usuario.Password = sqlDataReader.GetString(4);
                        usuario.Email = sqlDataReader.GetString(5);
                        usuario.FechaNacimiento = sqlDataReader.GetDateTime(6);
                        usuario.Estado = sqlDataReader.GetString(7);
                        usuarios.Add(usuario);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
            return Ok(usuarios);
        }
    }
}