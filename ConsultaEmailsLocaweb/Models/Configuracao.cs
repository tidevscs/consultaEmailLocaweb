using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaEmailsLocaweb.Models
{
    public class Configuracao
    {
        private static IConfiguration _configuration;

        
        public Configuracao(IConfiguration Configuration)
        {
            _configuration = Configuration;

        }

        public static string get_apy_key()
        {
            string apiKey = _configuration["APIKey"];

            return apiKey;
        }

    }
}
