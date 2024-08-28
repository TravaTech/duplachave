using duplachave.Model;
using System.Text.RegularExpressions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using duplachave.Interface;
using duplachave.Custon;

namespace duplachave
{
    public class Keys
    {
        public static DataValues GetKeys(string texto)
        {
            var regex = new Regex(@"{{([a-zA-Z0-9!:@#$&()\[\]\\-`\-.+,/\""]+)}}");
            DataValues values = new DataValues();
            foreach (Match i in regex.Matches(texto))
            {
                values.AddChave(i.Value);
            }
            return values;
        }

        public static Dictionary<string, string> ReplaceKeys(string texto, object json)
        {
            return ReplaceKeys(texto, JsonConvert.SerializeObject(json), new List<ListaDePara>());
        }

        public static Dictionary<string, string> ReplaceKeys(string texto, object json, List<ListaDePara> listReplace)
        {
            return ReplaceKeys(texto, JsonConvert.SerializeObject(json), listReplace);
        }


        public static Dictionary<string, string> ReplaceKeys(string texto, string json)
        {
            return ReplaceKeys(texto, json, new List<ListaDePara>());
        }

        public static Dictionary<string, string> ReplaceKeys(string texto, string json, List<ListaDePara> listReplace)
        {
            DataValues chaves = GetKeys(texto);
            Dictionary<string, string> retornos = new Dictionary<string, string>();

            dynamic Dados = (JObject)JsonConvert.DeserializeObject(json);

            List<ReplaseIndex> listIndex = new List<ReplaseIndex>();

            if (chaves.indexs.Count > 0)
            {
                foreach (var index in chaves.indexs)
                {
                    if (index.index == -1)
                    {
                        var token = Dados.SelectToken(index.chave);
                        if (token != null)
                        {
                            if (token.GetType().Name == "JArray")
                            {
                                index.maxIndex = token.Count;
                                for (int i = 0; i < index.maxIndex; i++)
                                {
                                    ReplaseIndex indexRep = new ReplaseIndex();
                                    indexRep.indexVar = index.indexVar + "=" + i.ToString();
                                    indexRep.chaveDe = index.chave + "[" + index.indexVar + "]";
                                    indexRep.chavePara = index.chave + "[" + i.ToString() + "]";
                                    listIndex.Add(indexRep);
                                }
                            }
                        }
                    }
                }
            }
            else
            {
                ReplaseIndex index = new ReplaseIndex();
                index.indexVar = "0";
                index.chaveDe = "";
                index.chavePara = "";
                listIndex.Add(index);
            }


            List<ICommandKey> Commands = new List<ICommandKey>();
            Commands.Add(new Commands.YearCommand());
            Commands.Add(new Commands.DateCommand());
            Commands.Add(new Commands.TimeCommand());
            Commands.Add(new Commands.DateTimeCommand());
            Commands.Add(new Commands.ReplaceCommand());
            Commands.Add(new Commands.LengthCommand());
            Commands.Add(new Commands.IfCommand());
            Commands.Add(new Commands.FormatIntCommand());

            List<ICommandBlock> CommandsBlock = new List<ICommandBlock>();
            CommandsBlock.Add(new Commands.BlockCommand());
            CommandsBlock.Add(new Commands.IfBlockCommand());
            CommandsBlock.Add(new Commands.IfNotBlockCommand());

            foreach (var index in listIndex)
            {
                string retorno = texto;
                foreach (DataChave chave in chaves.chaves)
                {
                    string chaveStr = chave.chave;
                    if (!string.IsNullOrEmpty(index.chaveDe))
                    {
                        chaveStr = chave.chave.Replace(index.chaveDe, index.chavePara);
                    }

                    var token = Dados.SelectToken(chaveStr);

                    if (token == null)
                    {
                        token = Dados[chaveStr];
                    }

                    if (token != null)
                    {
                        if (texto.Contains(chave.fullchave()))
                        {
                            retorno = retorno.Replace(chave.fullchave(), token.Value.ToString());
                        }
                    }
                    else
                    {
                        retorno = retorno.Replace(chave.fullchave(), string.Empty);
                    }

                    foreach (ICommandKey command in Commands)
                    {
                        foreach (DataComando comando in chave.comandos)
                        {
                            if (comando.comando == command.CommandName)
                            {
                                retorno = retorno.Replace(chave.fullchave(comando), command.Execute(token, chave, comando.parametros, listReplace));
                            }
                        }
                    }

                    foreach (ICommandBlock command in CommandsBlock)
                    {
                        foreach (DataComando comando in chave.comandos)
                        {
                            if ((comando.comando == command.CommandStartName) && chave.ContemComando(command.CommandEndName))
                            {
                                foreach (var stringBetween in StringCuston.getBetween(retorno, chave.fullchave(comando), chave.fullchave(command.CommandEndName)))
                                {
                                    retorno = retorno.Replace(chave.fullchave(comando) + stringBetween + chave.fullchave(command.CommandEndName),
                                        command.Execute(token, chave, comando.parametros, listReplace, stringBetween));
                                }
                            }
                        }
                    }
                }
                retornos.Add(index.indexVar, retorno);
            }
            return retornos;
        }

    }
}
