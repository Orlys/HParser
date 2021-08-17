using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HParser.TypeConverters
{

    public class ListTypeConverter : ITypeConverter
    {
        public bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            if (!t.IsGenericType)
                return false;
            return typeof(List<>).IsAssignableFrom(t.GetGenericTypeDefinition());
        }

        public bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            if (content == null)
            {
                graph = null;
                return false;
            } 

            graph = ParseAsArray(content, provider, graphType);
            return true;
        }

        public string ToString(ITypeConverterProvider provider, object graph)
        {

            if (graph == null)
                return null;
            var t = graph.GetType();
            var listEleType = t.GetGenericArguments().Single();
            var arv = (ICollection)graph;
            if(arv.Count == 0)
                return null; 

            return string.Join(Separator, arv.Cast<object>().Select(v => Escape(v, provider, listEleType)));
        }



        private static string Escape(object v, ITypeConverterProvider p, Type t)
        {
            var g = p.GetTypeConverter(t);

            var s = g.ToString(p, v);

            var s1 = s.Replace(Quote, Quote + Quote);

            return (s == s1 && !s.Contains(Separator))
               ? s
               : Quote + s1 + Quote;
        }

        private const string Quote = "\'";
        private const string Separator = " ";

        private static IList ParseAsArray(string s, ITypeConverterProvider p, Type listType)
        {

            var t = listType.GetGenericArguments().Single();
            var a = (IList)Activator.CreateInstance(listType);
            var v = string.Empty;

            var state = 0;
            for (int i = 0; i < s.Length;)
            {
                char ch = s[i];

                switch (state)
                {
                    case 0:     //default
                        if (ch == Quote[default])
                        {
                            state = 2;
                        }
                        else if (ch == Separator[default])
                        {
                            //skip spaces in default mode
                        }
                        else
                        {
                            v += ch;
                            state = 1;
                        }
                        i++;
                        break;
                    case 1:     //reading unquoted value
                        if (ch == Separator[default])
                        {
                            var oc = p.GetTypeConverter(t);
                            if (oc.TryConvert(p, v, t, out var g))
                            {
                                a.Add(g);
                                v = string.Empty;
                                state = 0;
                            }
                        }
                        else
                        {
                            v += ch;
                        }
                        i++;
                        break;
                    case 2:     //reading quoted value
                        if (ch == Quote[default])
                        {
                            state = 3;
                        }
                        else
                        {
                            v += ch;
                        }
                        i++;
                        break;

                    case 3:     //after quote in quoted mode
                        if (ch == Quote[default])
                        {
                            v += ch;
                            state = 2;
                        }
                        else
                        {
                            var oc = p.GetTypeConverter(t);
                            if (oc.TryConvert(p, v, t, out var g))
                            {
                                a.Add(g);
                                v = string.Empty;
                                state = 0;
                            }
                        }
                        i++;
                        break;
                }
            }

            if (!string.IsNullOrEmpty(v))
            {
                var oc = p.GetTypeConverter(t);
                if (oc.TryConvert(p, v, t, out var g))
                {
                    a.Add(g);
                }
                //a.Add(v);
            }

            return a;
        }
    }
}
