using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace PlasmaShared
{
    public class CommandJsonSerializer
    {
        private readonly Dictionary<string, Type> knownTypes = new Dictionary<string, Type>();
        //   NetJSONSettings ns = new NetJSONSettings() {DateFormat = NetJSONDateFormat.ISO, UseEnumString = false};
        private JsonSerializerSettings settings = new JsonSerializerSettings() { Formatting = Formatting.None, NullValueHandling = NullValueHandling.Ignore};

        public CommandJsonSerializer(IEnumerable<Type> types)
        {
            NetJSON.NetJSON.IncludeFields = false;
            RegisterTypes(types.ToArray());
        }


        public object DeserializeLine(string line)
        {
            if (!string.IsNullOrEmpty(line))
            {
                var parts = line.Split(new[] { " " }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length != 2) throw new Exception(string.Format("Invalid json data {0} : {1}", this, line));
                Type type;
                if (!knownTypes.TryGetValue(parts[0], out type)) throw new Exception(string.Format("Invalid json type {0} : {1}", this, parts[0]));
                return JsonConvert.DeserializeObject(parts[1], type, settings) ?? type.GetConstructor(new Type[] { })?.Invoke(null);
            }
            return null;
        }

        public void RegisterTypes(params Type[] types)
        {
            foreach (var t in types) knownTypes[t.Name] = t;
        }

        public string SerializeToLine<T>(T value)
        {
            var send = $"{typeof(T).Name} {JsonConvert.SerializeObject(value, settings)}\n";
            return send;
        }
    }
}