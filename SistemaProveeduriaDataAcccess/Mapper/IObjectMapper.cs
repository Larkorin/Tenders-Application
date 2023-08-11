using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaProveeduriaDataAcccess.Mapper
{
    public interface IObjectMapper
    {
        List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows);
        BaseEntity BuildObject(Dictionary<string, object> row);
    }
}
