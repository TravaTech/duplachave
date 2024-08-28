using duplachave.Interface;
using duplachave.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace duplachave.Commands
{
    public class IfCommand : ICommandKey
    {
        public string CommandName => "IfLine";

        public string Execute(dynamic token, DataChave chave, Dictionary<int, string> parametros, List<ListaDePara> replace)
        {
            if ((token != null) && (parametros.Count == 2))
            {
                if(token.Value.ToString() == parametros[0])
                {
                    return parametros[1];
                }
                else
                {
                    return token.Value.ToString();
                }
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
