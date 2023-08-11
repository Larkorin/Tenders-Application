using DTOs;
using SistemaProveeduriaDataAcccess.CRUD;
using System.Collections;

namespace SistemaProveeduriaCORE
{
    public class UsuarioManager
    {
        private UsuarioCrudFactory _userDataAccessLayer = new();
        private ContrasenaCrudFactory _contrasenaDataAccessLayer = new();
        private PermisoCrudFactory _permisoDataAccessLayer = new(); 

        public void Create(Usuario usuario, String contrasena)
        {
            Usuario newUsuario = _userDataAccessLayer.CreateWithReturn(usuario);
            
            Contrasena usuarioContrasena = new Contrasena { 
                Valor = contrasena,
                Estado = "ACTIVO",
                FechaActualizacion = DateTime.Now,
                Usuario = newUsuario,
            };

            _contrasenaDataAccessLayer.Create(usuarioContrasena);

        }

        public Usuario RetrieveById(int id)
        {

            Usuario usuario = _userDataAccessLayer.Retrieve<Usuario>(id);

            if (usuario == null)
            {
                throw new ArgumentException("El usuario no existe");
            }

            return usuario;
        }

        public List<Usuario> RetrieveAll()
        {

            List<Usuario> usuarios = _userDataAccessLayer.RetrieveAll<Usuario>();

            return usuarios;
        }

        public void Update(Usuario usuario)
        {

            Usuario existingUsuario = _userDataAccessLayer.Retrieve<Usuario>(usuario.Id);

            if (existingUsuario == null)
            {
                throw new ArgumentException("El usuario no existe");
            }

            _userDataAccessLayer.Update(usuario);
        }

        public void Delete(int id)
        {

            Usuario usuario = _userDataAccessLayer.Retrieve<Usuario>(id);
            
            if (usuario == null)
            {
                throw new ArgumentException("El usuario no existe");
            }

            _userDataAccessLayer.Delete(usuario);
        }

        public void AssignPermissions(List<int> idsPermisos, int userId)
        {
            List<int> newPermissions = new List<int>();
            List<int> updatedPermissions = new List<int>();

            foreach (int idPermiso in idsPermisos)
            {
                
                String status = _userDataAccessLayer.RetrievePermissions<String>(idPermiso, userId);

                if (status != null && status == "INACTIVO")
                {
                    updatedPermissions.Add(idPermiso);   
                } 
                else if (status == null) 
                {
                    newPermissions.Add(idPermiso);
                }
            }

            foreach (int idNewPermiso in newPermissions)
            {
                _userDataAccessLayer.AssignPermissions(idNewPermiso, userId);
            }

            foreach (int idPermisoToUpdate in updatedPermissions)
            {
                _userDataAccessLayer.UpdateUserPermissions(idPermisoToUpdate, userId, "ACTIVO");
            }
        }

        public void RemovePermissions(List<int> idsPermisos, int userId)
        {
            List<int> updatedPermissions = new List<int>();

            foreach (int idPermiso in idsPermisos)
            {
                String status = _userDataAccessLayer.RetrievePermissions<String>(idPermiso, userId);

                if (status != null && status == "ACTIVO")
                {
                    updatedPermissions.Add(idPermiso);
                }
            }

            foreach (int idPermisoToUpdate in updatedPermissions)
            {
                _userDataAccessLayer.UpdateUserPermissions(idPermisoToUpdate, userId, "INACTIVO");
            }
        }

        public List<Permiso> GetUserPermissions(int userId)
        {
            List<Permiso> permisos = _permisoDataAccessLayer.RetrieveUserPermisssions<Permiso>(userId);

            return permisos;
        }
        public Usuario RetrieveByLogin(string email, string password)
        {

            Usuario usuario = _userDataAccessLayer.RetrieveLogin<Usuario>(email, password);

            if (usuario == null)
            {
                throw new ArgumentException("El correo o la contraseña son incorrectos");
            }

            return usuario;
        }

        public Usuario RetrieveByPhone(string phone)
        {

            Usuario usuario = _userDataAccessLayer.RetrievePhone<Usuario>(phone);

            if (usuario == null)
            {
                throw new ArgumentException("No existe un usuario con el numero de telefono indicado");
            }

            return usuario;
        }

        public Usuario RetrieveByEmail(string email)
        {

            Usuario usuario = _userDataAccessLayer.RetrieveEmail<Usuario>(email);

            if (usuario == null)
            {
                throw new ArgumentException("No existe un usuario con el correo eletronico indicado");
            }

            return usuario;
        }
    }
}
