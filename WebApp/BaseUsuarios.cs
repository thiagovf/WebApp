﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp
{
    public static class BaseUsuarios
    {
        public static IEnumerable<Usuario> Usuarios()
        {
            return new List<Usuario>()
            {
                new Usuario(){Nome = "Fulano", Senha = "123456"},
                new Usuario(){Nome = "Beltrano", Senha = "123456"},
                new Usuario(){Nome = "Cicrano", Senha = "123456"}
            };
        }
    }
}