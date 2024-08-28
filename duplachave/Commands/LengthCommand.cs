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
    public class LengthCommand : ICommandKey
    {
        public string CommandName => "Length";

        public string Execute(dynamic token, DataChave chave, Dictionary<int, string> parametros, List<ListaDePara> replace)
        {
            if ((token != null) && (token.GetType().Name == "JArray") && (parametros.Count == 0))
            {
                return token.Count.ToString();

            }
            else
            {
                return string.Empty;
            }
        }
    }
}
