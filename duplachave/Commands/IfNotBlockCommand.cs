using duplachave.Interface;
using duplachave.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace duplachave.Commands
{
    public class IfNotBlockCommand : ICommandBlock
    {
        public string CommandStartName => "IfNotBlock";

        public string CommandEndName => "IfNotEnd";

        public string Execute(dynamic token, DataChave chave, Dictionary<int, string> parametros, List<ListaDePara> replace, string stringBetween)
        {
            if ((token != null) && (parametros.Count == 2))
            {
                if (token.GetType().Name == "JArray")
                {
                    string stringComplet = string.Empty;
                    foreach (JToken JSonData in token)
                    {
                        var iftoken = JSonData.SelectToken(parametros[0]);

                        if ((iftoken != null) && (iftoken.ToString() != parametros[1]))
                        {
                            stringComplet += Keys.ReplaceKeys(stringBetween, JSonData.ToString(), replace);
                        }
                    }
                    return stringComplet;
                }
                else if (token.GetType().Name == "JObject")
                {
                    var iftoken = token.SelectToken(parametros[0]);

                    if ((iftoken != null) && (iftoken.ToString() != parametros[1]))
                    {
                        return Keys.ReplaceKeys(stringBetween, token.ToString(), replace);
                    }
                    else
                    {
                        return string.Empty;
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
