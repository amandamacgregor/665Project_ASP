﻿//Using Demo 7

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// add

using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace HappyEarthConsignment.Models
{
    public static class SessionExtensions
    {
        public static void SetObject<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public static T GetObject<T>(this ISession session, string key)
        {
            var value = session.GetString(key);

            // if value is null, return null, else the deserialized object

            return (value == null) ? default : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
