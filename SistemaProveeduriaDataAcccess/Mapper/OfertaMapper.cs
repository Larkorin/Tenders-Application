using DTOs;
using SistemaProveeduriaDataAcccess.DAOs;

namespace SistemaProveeduriaDataAcccess.Mapper
{
    public class OfertaMapper : ISqlStatements, IObjectMapper
    {
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var oferta = new Oferta
            {
                Id = GetIntValue(row, "ID_OFERTA"),
                Oferente = GetIntValue(row, "ID_USUARIO"),
                Licitacion = GetIntValue(row, "ID_LICITACION"),
                Estado = GetStringValue(row, "ESTADO"),
                Precio = GetDoubleValue(row, "PRECIO"),
                Producto = null
            };

            return oferta;
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
                var oferta = BuildObject(row);
                lstResults.Add(oferta);
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
            var sqlOferta = new SqlOperations { ProcedureName = "CRE_OFERTA_PR" };
            var oferta = (Oferta)entity;

            sqlOferta.AddVarcharParam("P_ESTADO", oferta.Estado);
            sqlOferta.AddDoubleParam("P_PRECIO", oferta.Precio);
            sqlOferta.AddIntParam("P_ID_USUARIO", oferta.Oferente);
            sqlOferta.AddIntParam("P_ID_LICITACION", oferta.Licitacion);

            return sqlOferta;
        }

        public SqlOperations GetDeleteStatement(BaseEntity entity)
        {
            var sqlOferta = new SqlOperations { ProcedureName = "DEL_OFERTA_PR" };

            var oferta = (Oferta)entity;

            sqlOferta.AddIntParam("P_DOCUMENTO", oferta.Id);

            return sqlOferta;
        }

        public SqlOperations GetRetriveAllStatement()
        {
            var sqlOferta = new SqlOperations { ProcedureName = "RET_ALL_OFERTA_PR" };
            return sqlOferta;
        }
        public SqlOperations GetRetriveStatement(int id)
        {
            var sqlOferta = new SqlOperations { ProcedureName = "RET_OFERTA_BY_ID_PR" };
            sqlOferta.AddIntParam("P_ID_OFERTA", id);

            return sqlOferta;
        }

        public SqlOperations GetUpdateStatement(BaseEntity entity)
        {
            var sqlOferta = new SqlOperations { ProcedureName = "UPDATE_OFERTA_PR" };

            var oferta = (Oferta)entity;
            sqlOferta.AddIntParam("P_ID_OFERTA", oferta.Id);
            sqlOferta.AddVarcharParam("P_ESTAOO", oferta.Estado);
            sqlOferta.AddDoubleParam("P_PRECIO", oferta.Precio);
            sqlOferta.AddIntParam("P_ID_USUARIO", oferta.Oferente);
            sqlOferta.AddIntParam("P_ID_LICITACION", oferta.Licitacion);

            return sqlOferta;
        }

        public SqlOperations GetOfertaProductStatement(int ofertaId, int productId, int quantity)
        {
            var sqlLicitacion = new SqlOperations { ProcedureName = "CRE_OFERTA_PRODUCTO_PR" };

            sqlLicitacion.AddIntParam("P_ID_OFERTA", ofertaId);
            sqlLicitacion.AddIntParam("P_ID_PRODUCTO", productId);
            sqlLicitacion.AddIntParam("P_CANTIDAD", quantity);

            return sqlLicitacion;
        }

        public SqlOperations GetRetriveOfferByTenderIdStatement(int id)
        {
            var sqlOferta = new SqlOperations { ProcedureName = "RET_OFERTAS_BY_LICITACION_ID_PR" };
            sqlOferta.AddIntParam("P_ID_LICITACION", id);

            return sqlOferta;
        }

        public SqlOperations GetRetriveOfferByUserIdStatement(int id)
        {
            var sqlOferta = new SqlOperations { ProcedureName = "RET_OFERTA_BY_USUARIO_ID_PR" };
            sqlOferta.AddIntParam("P_ID_USUARIO", id);

            return sqlOferta;
        }

        public SqlOperations GetRetriveProductsByOfferIdStatement(int id)
        {
            var sqlOferta = new SqlOperations { ProcedureName = "RET_PRODUCTOS_BY_OFERTA_PR" };
            sqlOferta.AddIntParam("P_ID_OFERTA", id);

            return sqlOferta;
        }

        protected int GetIntValue(Dictionary<string, object> dic, string attName)
        {
            var val = dic[attName];
            if (dic.ContainsKey(attName) && (val is int || val is decimal))
                return (int)dic[attName];

            return -1;
        }

        protected double GetDoubleValue(Dictionary<string, object> dic, string attName)
        {
            object val;
            if (dic.TryGetValue(attName, out val) && (val is int || val is double || val is decimal))
                return Convert.ToDouble(val);

            return -1.0;
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
    }
}