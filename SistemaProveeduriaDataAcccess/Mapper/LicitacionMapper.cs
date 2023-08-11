using DTOs;
using SistemaProveeduriaDataAcccess.CRUD;
using SistemaProveeduriaDataAcccess.DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaProveeduriaDataAcccess.Mapper
{
    public class LicitacionMapper : ISqlStatements, IObjectMapper
    {
        UsuarioCrudFactory _userCrudFactory = new UsuarioCrudFactory();
        EscuelaCrudFactory _escuelaCrudFactory = new EscuelaCrudFactory();
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            byte[] codigoByte = GetByteValue(row, "CODIGO_QR");
            //string codigoString = Encoding.UTF8.GetString(codigoByte);
            var licitacion = new Licitacion
            {
                Id = GetIntValue(row, "ID_LICITACION"),
                UsuarioEncargado = GetIntValue(row, "ID_USUARIO_ENCARGADO"),
                UsuarioProveedor = GetIntValue(row, "ID_USUARIO_PROVEEDOR"),
                Escuela = GetIntValue(row, "ID_ESCUELA"),
                Titulo = GetStringValue(row, "TITULO"),
                Descripcion = GetStringValue(row, "DESCRIPCION"),
                Estado = GetStringValue(row, "ESTADO"),
                CodigoQR = "",
                Producto = null,
                Presupuesto = GetDoubleValue(row, "PRESUPUESTO"),
                FechaCreacion = GetDateTimeValue(row, "FECHA_CREACION"),
                FechaCierreOfertas = GetDateTimeValue(row, "FECHA_CIERRE_OFERTAS"),
                FechaCierreLicitacion = GetDateTimeValue(row, "FECHA_CIERRE_LICITACION"),
                FechaEntrega = GetDateTimeValue(row, "FECHA_ENTREGA")
            };
            return licitacion;
        }

        public BaseEntity BuildProductObject(Dictionary<string, object> row)
        {
            var producto = new Producto
            {
                Id = GetIntValue(row, "ID_PRODUCTO"),
                UnidadMedida = GetStringValue(row, "UNIDAD_MEDIDA"),
                Nombre = GetStringValue(row, "NOMBRE"),
                Cantidad = GetIntValue(row, "CANTIDAD")
            };
            return producto;
        }


        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var licitacion = BuildObject(row);
                lstResults.Add(licitacion);
            }
            return lstResults;
        }

        public List<BaseEntity> BuildProductObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var producto = BuildProductObject(row);
                lstResults.Add(producto);
            }
            return lstResults;
        }

        public SqlOperations GetCreateStatement(BaseEntity entity)
        {
            var sqlLicitacion = new SqlOperations { ProcedureName = "CRE_LICITACION_PR" };
            var licitacion = (Licitacion)entity;
            byte[] codigoBytes = Encoding.UTF8.GetBytes(licitacion.CodigoQR);

            sqlLicitacion.AddVarcharParam("P_TITULO", licitacion.Titulo);
            sqlLicitacion.AddVarcharParam("P_DESCRIPCION", licitacion.Descripcion);
            sqlLicitacion.AddVarcharParam("P_ESTADO", licitacion.Estado);
            sqlLicitacion.AddByteParam("P_DOCUMENTO", codigoBytes);
            sqlLicitacion.AddDoubleParam("P_PRESUPUESTO", licitacion.Presupuesto);
            sqlLicitacion.AddDateTimeParam("P_FECHA_CREACION", licitacion.FechaCreacion);
            sqlLicitacion.AddDateTimeParam("P_FECHA_CIERRE_OFERTAS", licitacion.FechaCierreOfertas);
            sqlLicitacion.AddDateTimeParam("P_FECHA_CIERRE_LICITACION", licitacion.FechaCierreLicitacion);
            sqlLicitacion.AddDateTimeParam("P_FECHA_ENTREGA", licitacion.FechaEntrega);
            sqlLicitacion.AddIntParam("P_ID_USUARIO_ENCARGADO", licitacion.UsuarioEncargado);
            sqlLicitacion.AddIntParam("P_ID_USUARIO_PROVEEDOR", licitacion.UsuarioProveedor);

            return sqlLicitacion;
        }

        public SqlOperations GetDeleteStatement(BaseEntity entity)
        {
            var sqlLicitacion = new SqlOperations { ProcedureName = "DEL_LICITACION_PR" };

            var licitacion = (Licitacion)entity;

            sqlLicitacion.AddIntParam("P_ID_LICITACION", licitacion.Id);

            return sqlLicitacion; ;
        }

        public SqlOperations GetRetriveAllStatement()
        {
            var sqlLicitacion = new SqlOperations { ProcedureName = "RET_ALL_LICITACION_PR" };
            return sqlLicitacion;
        }

        public SqlOperations GetRetriveStatement(int id)
        {
            var sqlLicitacion = new SqlOperations { ProcedureName = "RET_LICITACION_BY_ID_PR" };
            sqlLicitacion.AddIntParam("P_ID_LICITACION", id);

            return sqlLicitacion;
        }

        public SqlOperations GetUpdateStatement(BaseEntity entity)
        {
            var sqlLicitacion = new SqlOperations { ProcedureName = "UPDATE_LICITACION_PR" };

            var licitacion = (Licitacion)entity;
            byte[] codigoBytes = Encoding.UTF8.GetBytes(licitacion.CodigoQR);

            sqlLicitacion.AddIntParam("P_ID_LICITACION", licitacion.Id);
            sqlLicitacion.AddVarcharParam("P_TITULO", licitacion.Titulo);
            sqlLicitacion.AddVarcharParam("P_DESCRIPCION", licitacion.Descripcion);
            sqlLicitacion.AddVarcharParam("P_ESTADO", licitacion.Estado);
            sqlLicitacion.AddByteParam("P_CODIGO_QR", codigoBytes);
            sqlLicitacion.AddDoubleParam("P_PRESUPUESTO", licitacion.Presupuesto);
            sqlLicitacion.AddDateTimeParam("P_FECHA_CREACION", licitacion.FechaCreacion);
            sqlLicitacion.AddDateTimeParam("P_FECHA_CIERRE_OFERTAS", licitacion.FechaCierreOfertas);
            sqlLicitacion.AddDateTimeParam("P_FECHA_CIERRE_LICITACION", licitacion.FechaCierreLicitacion);
            sqlLicitacion.AddDateTimeParam("P_FECHA_ENTREGA", licitacion.FechaEntrega);
            sqlLicitacion.AddIntParam("P_ID_USUARIO_ENCARGADO", licitacion.UsuarioEncargado);
            sqlLicitacion.AddIntParam("P_ID_ESCUELA", licitacion.Escuela);

            return sqlLicitacion;
        }

        public SqlOperations GetLicitacionProductStatement(int licitacionId, int productId, int quantity)
        {
            var sqlLicitacion = new SqlOperations { ProcedureName = "CRE_LICITACION_PRODUCTO_PR" };

            sqlLicitacion.AddIntParam("P_ID_LICITACION", licitacionId);
            sqlLicitacion.AddIntParam("P_ID_PRODUCTO", productId);
            sqlLicitacion.AddIntParam("P_CANTIDAD", quantity);
            sqlLicitacion.AddVarcharParam("P_ESTADO", "ACTIVO");

            return sqlLicitacion;
        }

        public SqlOperations GetRetrieveAllLicitacionProductsStatement(int licitacionId, int productId, int quantity)
        {
            var sqlLicitacion = new SqlOperations { ProcedureName = "RET_ALL_LICITACION_PRODUCTO_PR" };

            return sqlLicitacion;
        }

        public SqlOperations GetLicitacionProductByIdStatement(int licitacionId, int productId)
        {
            var sqlLicitacion = new SqlOperations { ProcedureName = "RET_LICITACION_PRODUCTO_BY_ID_PR" };

            sqlLicitacion.AddIntParam("ID_LICITACION", licitacionId);
            sqlLicitacion.AddIntParam("ID_PRODUCTO", productId);

            return sqlLicitacion;
        }

        public SqlOperations GetUpdateLicitacionProductStatement(int licitacionId, int productId, int quantity)
        {
            var sqlLicitacion = new SqlOperations { ProcedureName = "UPDATE_LICITACION_PRODUCTO_PR" };

            sqlLicitacion.AddIntParam("P_ID_LICITACION", licitacionId);
            sqlLicitacion.AddIntParam("P_ID_PRODUCTO", productId);
            sqlLicitacion.AddIntParam("P_CANTIDAD", quantity);


            return sqlLicitacion;
        }

        public SqlOperations GetAllProductsByLicitacionStatement(int licitacionId)
        {
            var sqlLicitacion = new SqlOperations { ProcedureName = "RET_LICITACION_PRODUCTO_BY_LICITACIONO_ID_PR" };

            sqlLicitacion.AddIntParam("P_ID_LICITACION", licitacionId);


            return sqlLicitacion;
        }

        public SqlOperations GetCreateWithReturnStatement(BaseEntity entity)
        {
            var sqlLicitacion = new SqlOperations { ProcedureName = "CRE_LICITACION_WITH_RETURN_PR" };
            var licitacion = (Licitacion)entity;
            //byte[] codigoBytes = Encoding.UTF8.GetBytes(licitacion.CodigoQR);
            byte[] codigoBytes = Encoding.UTF8.GetBytes("");

            sqlLicitacion.AddVarcharParam("P_TITULO", licitacion.Titulo);
            sqlLicitacion.AddVarcharParam("P_DESCRIPCION", licitacion.Descripcion);
            sqlLicitacion.AddVarcharParam("P_ESTADO", licitacion.Estado);
            sqlLicitacion.AddByteParam("P_CODIGO_QR", codigoBytes);
            sqlLicitacion.AddDoubleParam("P_PRESUPUESTO", licitacion.Presupuesto);
            sqlLicitacion.AddDateTimeParam("P_FECHA_CREACION", licitacion.FechaCreacion);
            sqlLicitacion.AddDateTimeParam("P_FECHA_CIERRE_OFERTAS", licitacion.FechaCierreOfertas);
            sqlLicitacion.AddDateTimeParam("P_FECHA_CIERRE_LICITACION", licitacion.FechaCierreLicitacion);
            sqlLicitacion.AddDateTimeParam("P_FECHA_ENTREGA", licitacion.FechaEntrega);
            sqlLicitacion.AddIntParam("P_ID_USUARIO_ENCARGADO", licitacion.UsuarioEncargado);
            sqlLicitacion.AddIntParam("P_ID_ESCUELA", licitacion.Escuela);

            return sqlLicitacion;
        }

        public SqlOperations GetAdjudicarLicitacionOfertaStatement(int licitacionId, int ofertaId) {
           
            var sqlLicitacion = new SqlOperations { ProcedureName = "SP_ADJUDICAR_OFERTA" };

            sqlLicitacion.AddIntParam("P_ID_LICITACION", licitacionId);
            sqlLicitacion.AddIntParam("P_ID_OFERTA", ofertaId);

            return sqlLicitacion;
        }

        protected int GetIntValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && (val is int || val is decimal))
                return (int)dic[attName];

            return -1;
        }
        protected string GetStringValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && (val is string))
                return (string)dic[attName];

            return null;
        }

        protected DateTime GetDateTimeValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && (val is DateTime))
                return (DateTime)dic[attName];

            return DateTime.MinValue;
        }

        protected byte[] GetByteValue(Dictionary<string, object> dic, string attName)
        {
            if (dic.ContainsKey(attName) && dic[attName] is byte[])
                return (byte[])dic[attName];

            return null;
        }
        protected double GetDoubleValue(Dictionary<string, object> dic, string attName)
        {
            object val;
            if (dic.TryGetValue(attName, out val) && (val is int || val is double || val is decimal))
                return Convert.ToDouble(val);

            return -1.0;
        }
    }
}
