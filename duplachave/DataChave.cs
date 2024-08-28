using duplachave.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace duplachave
{
    public class DataChave
    {
        public string chave { get; set; }
        public List<DataComando> comandos { get; set; }

        public DataChave()
        {
            comandos = new List<DataComando>();
        }

        public string fullchave()
        {
            return "{{" + chave + "}}";
        }

        public string fullchave(string comando)
        {
            return "{{" + chave + ":" + comando + "}}";
        }

        public string fullchave(DataComando comando)
        {
            if (comando == null)
            {
                return "{{" + chave + "}}";
            }
            else
            {
                return "{{" + chave + ":" + comando.fullcomando + "}}";
            }
        }
        public bool ContemComando(string comando)
        {
            return comandos.Exists(x => x.fullcomando == comando);
        }
        public void AddComando(string comando)
        {
            string[] subComando = comando.Split('(');
            if (!ContemComando(comando))
            {
                DataComando data = new DataComando();
                data.fullcomando = comando;
                data.comando = subComando[0];
                data.parametros = GetParameters(comando);
                comandos.Add(data);
            }
        }

        public static Dictionary<int, string> GetParameters(string comand)
        {
            Dictionary<int, string> parameters = new Dictionary<int, string>();

            string[] chaves = comand.Split('(');
            if (chaves.Length > 1)
            {
                int poss = 0;
                foreach (var parametro in chaves[1].Replace(")", string.Empty).Split(','))
                {
                    parameters.Add(poss, parametro);
                    poss++;
                }
            }

            return parameters;
        }
    }
}
