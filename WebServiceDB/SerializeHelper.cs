using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace WebServiceDB
{
    public class SerializeHelper
    {
        public static string ToString(object obj)
        {
            JsonSerializerSettings serializeSetting = new JsonSerializerSettings();
            serializeSetting.ContractResolver = new CustomResolver();
            serializeSetting.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            serializeSetting.PreserveReferencesHandling = PreserveReferencesHandling.None;

            return JsonConvert.SerializeObject(obj, serializeSetting);
        }
    }

    class CustomResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            JsonProperty prop = base.CreateProperty(member, memberSerialization);
            var propInfo = member as PropertyInfo;
            if (propInfo != null)
            {
                if (propInfo.GetMethod.IsVirtual && !propInfo.GetMethod.IsFinal)
                {
                    prop.ShouldSerialize = obj => false;
                }
            }
            return prop;
        }
    }
}