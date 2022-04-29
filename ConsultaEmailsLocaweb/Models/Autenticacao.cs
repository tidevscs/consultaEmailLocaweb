using Novell.Directory.Ldap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultaEmailsLocaweb.Models
{
    public class Autenticacao
    {
        public string usuario { get; set; }
        public String senha { get; set; }

        public Boolean logado { get; set; }
        public Boolean autenticar( string p_usuario, string p_senha)
        {

             string usuario = @"hmsc\" + p_usuario;
             string senha = p_senha;
            using (var cn = new LdapConnection())
            {
                // connect
                cn.Connect("172.16.1.6", 389);
                // bind with an username and password
                // this how you can verify the password of an user
                try { 
                cn.Bind(usuario, senha);
                    if (cn.Bound)
                    {
                        this.logado = true;
                    }
                }
                catch
                {
                    this.logado = false;
                }
                // call ldap op
                // cn.Delete("<<userdn>>")
                // cn.Add(<<ldapEntryInstance>>)
                
            }

            return this.logado;


        }
    }
}
