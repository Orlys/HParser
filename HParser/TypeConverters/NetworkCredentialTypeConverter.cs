using System;
using System.Linq;
using System.Net;

namespace HParser.TypeConverters
{
    public class NetworkCredentialTypeConverter : ITypeConverter
    {

        public bool CanConvert(ITypeConverterProvider provider, Type t)
        {
            return t == typeof(NetworkCredential);
        }

        public bool TryConvert(ITypeConverterProvider provider, string content, Type graphType, out object graph)
        {
            graph = ToNetworkCredential(content);
            return true;
        }

        public string ToString(ITypeConverterProvider provider, object graph)
        {
            var nc = graph as NetworkCredential;
            return ToFriendlyString(nc);
        }
           

        private static NetworkCredential ToNetworkCredential(string s)
        {

            if (s == null) return null;

            var domain = ResolveDomainString(ref s);
            var username = ResolveUsernameString(ref s);
            var password = string.Empty;
            if (string.IsNullOrEmpty(username))
            {
                username = NormalizeRestPart(s);
            }
            else
            {
                password = NormalizeRestPart(s);
            }
            return new NetworkCredential(username, password, domain);
        }

        private static string ToFriendlyString(NetworkCredential credential)
        {
            if (credential == null) return null;

            string usernameAndPassword;

            if (string.IsNullOrEmpty(credential.UserName) && string.IsNullOrEmpty(credential.Password))
            {
                usernameAndPassword = string.Empty;
            }
            else if (string.IsNullOrEmpty(credential.UserName))
            {
                usernameAndPassword = $":{PackPassword(credential.Password)}";
            }
            else if (string.IsNullOrEmpty(credential.Password))
            {
                usernameAndPassword = credential.UserName;
            }
            else
            {
                usernameAndPassword = $"{credential.UserName}:{PackPassword(credential.Password)}";
            }

            return string.IsNullOrEmpty(credential.Domain)
               ? usernameAndPassword
               : $"{usernameAndPassword}@{credential.Domain}";
        }


        private static string PackPassword(string password)
        {
            return password?.Replace(":", "\\:").Replace("@", "\\@");
        }



        private static string NormalizeRestPart(string s)
        {
            return Normalize(s);
        }

        private static string ResolveDomainString(ref string s)
        {
            var domain = string.Empty;
            var atSignSearchStartIndex = s.Length - 1;
        SearchAtSign:
            var atSignIndex = s.LastIndexOf('@', atSignSearchStartIndex);
            if (atSignIndex != -1 && atSignIndex != 0) // 有找到正確的 @ 符號
            {
                var theSignBeforeAtSignIndex = s.ElementAtOrDefault(atSignIndex - 1);
                var theSignAfterAtSignIndex = atSignIndex + 1;
                // todo: theSignBeforeAtSignIndex == '\0'
                if (theSignBeforeAtSignIndex != '\\') // 如果 @ 符號前面不是 \
                {
                    // 解析到  [credential]@[host]
                    domain = s.Substring(theSignAfterAtSignIndex);
                    domain = Normalize(domain);
                    s = s.Remove(atSignIndex);
                }
                else
                {
                    atSignSearchStartIndex = atSignIndex - 1;
                    goto SearchAtSign;
                }
            }

            return domain;
        }

        private static string Normalize(string s)
        {
            return s?.Replace("\\@", "@")
                .Replace("\\:", ":");
        }

        private static string ResolveUsernameString(ref string s)
        {
            var username = string.Empty;
            var colonSignSearchStartIndex = 0;
        SearchColonSign:
            var colonSignIndex = s.IndexOf(':', colonSignSearchStartIndex);
            if (colonSignIndex != -1 && colonSignIndex != (s.Length - 1)) // 有找到正確的 : 符號
            {
                var theSignBeforeColonSignIndex = s.ElementAtOrDefault(colonSignIndex - 1);
                if (theSignBeforeColonSignIndex != '\\') // 如果 : 符號前面不是 \
                {
                    // 不需轉譯
                    // 解析到  [user]:[password]
                    username = s.Substring(0, colonSignIndex);
                    username = Normalize(username);
                    s = s.Remove(0, colonSignIndex + 1);
                }
                else
                {
                    colonSignSearchStartIndex = colonSignIndex + 1;
                    goto SearchColonSign;
                }
            }

            return username;
        }
    }
}
