﻿using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace TTCM.Utility
{
    public static class ExtensionHelper
    {
        public static string ToVnd(this decimal? giaTri)
        {
            if (giaTri == null)
            {
                return "";
            }
            else
            {
                return $"{giaTri.Value:#,##0.00} đ";
            }
        }
        public static string ToVnd2(this double giaTri)
        {
            
            return $"{giaTri:#,##0.00} đ";
            
        }

        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value == null ? default : JsonSerializer.Deserialize<T>(value);
        }
    }
}