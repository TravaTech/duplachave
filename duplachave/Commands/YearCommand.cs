﻿using duplachave.Interface;
using duplachave.Model;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace duplachave.Commands
{
    public class YearCommand : ICommandKey
    {
        public string CommandName => "Year";

        public string Execute(dynamic token, DataChave chave, Dictionary<int, string> parametros, List<ListaDePara> replace)
        {
            if ((token != null) && (token.Type == JTokenType.Date) && (parametros.Count == 0))
            {
                return ((DateTime)token.Value).ToString("yyyy");

            }
            else
            {
                return string.Empty;
            }
        }
    }
}
