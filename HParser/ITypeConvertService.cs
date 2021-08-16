using System;

namespace HParser
{
    public interface ITypeConvertService
    {
        object ToGraph(Type graphType, string content);
        string ToString<TGrpah>(TGrpah graoh);
    }
}