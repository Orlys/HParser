using System;

namespace HParser
{
    public interface ITypeConvertService
    {
        TGraph ToGraph<TGraph>(string content);
        object ToGraph(Type graphType, string content);
        string ToString<TGrpah>(TGrpah graoh);
    }
}