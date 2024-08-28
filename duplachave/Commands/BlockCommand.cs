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
    public class BlockCommand : ICommandBlock
    {
        public string CommandStartName => "StartBlock";

        public string CommandEndName => "EndBlock";

        public string Execute(dynamic token, DataChave chave, Dictionary<int, string> parametros, List<ListaDePara> replace, string stringBetween)
        {
            if((token != null) && (parametros.Count == 0))
            {
                if (token.GetType().Name == "JArray")
                {
                    string stringComplet = string.Empty;
                    foreach (JToken JSonData in token)
                    {
                        stringComplet += Keys.ReplaceKeys(stringBetween, JSonData.ToString(), replace)["0"];

                    }
                    return stringComplet;
                }
                else if (token.GetType().Name == "JObject")
                {
                    return Keys.ReplaceKeys(stringBetween, token.ToString(), replace)["0"];
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
