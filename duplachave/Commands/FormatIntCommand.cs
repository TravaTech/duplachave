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
    public class FormatIntCommand : ICommandKey
    {
        public string CommandName => "FormatInt";

        public string Execute(dynamic token, DataChave chave, Dictionary<int, string> parametros, List<ListaDePara> replace)
        {
            if ((token != null) && (token.Type == JTokenType.Integer) && (parametros.Count == 1))
            {
                switch (parametros[0])
                {
                    case "maxsql":
                        if ((int)token.Value < 0)
                            return "(MAX)";
                        else
                            return string.Format("({0})", (int)token.Value);
                    default:
                        return string.Format("{0}", (int)token.Value);
                }

            }
            else
            {
                return string.Empty;
            }
        }
    }
}
