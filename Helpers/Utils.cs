using System;
using System.Security.Cryptography;
using System.Text;

namespace Base.Helpers
{
    public static class Utils
    {
        public static string sha256_hash(string value)
        {
            var sb = new StringBuilder();

            using (var hash = SHA256.Create())            
            {
                var enc = Encoding.UTF8;
                var result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
        
        public static bool TimeBetween(DateTime check, DateTime start, DateTime end, bool inclusive = true)
        {
            var from = new DateTime(check.Year, check.Month, check.Day, 
                start.Hour, start.Minute, start.Second, start.Millisecond);
            var to = new DateTime(check.Year, check.Month, check.Day, 
                end.Hour, end.Minute, end.Second, end.Millisecond);

            if (inclusive)
                return from <= check && to >= check;

            return from < check && to > check;
        }
    }
    
    
}