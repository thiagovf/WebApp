using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp
{
    public class Usuario
    {
        public string Nome { get; set; }

        public string Senha { get; set; }

        public string[] Papeis { get; set; }
    }
}