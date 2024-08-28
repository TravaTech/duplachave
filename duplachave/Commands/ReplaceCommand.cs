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
    public class ReplaceCommand : ICommandKey
    {
        public string CommandName => "Replace"; 

        public string Execute(dynamic token, DataChave chave, Dictionary<int, string> parametros, List<ListaDePara> replace)
        {
            if ((token != null) && (parametros.Count == 2))
            {
                ListaDePara dePara = replace.Where(x => x.Nome == parametros[0]).FirstOrDefault();
                if ((dePara.Valores.ContainsKey(token.Value.ToString().ToLower())) && (dePara.Valores[token.Value.ToString().ToLower()].ContainsKey(parametros[1])))
                    return dePara.Valores[token.Value.ToString().ToLower()][parametros[1]];
                else
                    return string.Empty;

            }
            else
            {
                return string.Empty;
            }
        }
    }
}
