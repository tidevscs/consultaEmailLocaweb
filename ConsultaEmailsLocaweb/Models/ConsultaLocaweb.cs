using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ConsultaEmailsLocaweb.Models
{
    

public class Emails
{
    
    public Data data { get; set; }
    public Links links { get; set; }

    public Emails()
        {

        }
   
    
    public Emails buscar(string apiKey,string link)
        {


            Emails emails = new Emails();

            emails.preencherDados(apiKey, ref emails,link);
            
            

           

            return emails;

        }


        public string buscar_por_destinatario(string destinatario,string datainicial,string datafinal)
        {

            string url = "";

            string saida;

            url = "https://smtplw.com.br/panel/reports/delivered_details";
            url += "?status=delivered&period=custom";
            string dt_inicial = datainicial; //.Replace("/", "%2F");
            string dt_final = datafinal;//.Replace("/", "%2F");
            url += "&range_start="+ dt_inicial + "&range_end="+ dt_final ;
            url += "&per=25&search="+ destinatario + "&_=1651067821302";

            var requisicaoWeb = WebRequest.CreateHttp(url);
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";
            requisicaoWeb.Headers.Add("Cookie", "_ga=GA1.3.571504747.1629922620; _hjid=7be44a09-a251-4c3d-b2df-0614b0a2a786; _hjSessionUser_1884734=eyJpZCI6IjEwNDk3ZGQ5LTY2OGMtNTFkMS1hN2RjLTdkYzIxMmViYTQxMiIsImNyZWF0ZWQiOjE2Mzg4MDc3NTkyMjcsImV4aXN0aW5nIjp0cnVlfQ==; tracksale-widget=saocristovaomkt; _gid=GA1.3.1989569264.1651067659; cookie_tour=true; _hjSession_1884734=eyJpZCI6IjY1Yjc1MzYxLWMyODctNDIxOS05YjlmLTFjYjI5MDQ1YWM1MiIsImNyZWF0ZWQiOjE2NTEwNjc2NTk3MDQsImluU2FtcGxlIjp0cnVlfQ==; _hjIncludedInSessionSample=1; _hjIncludedInPageviewSample=1; _hjAbsoluteSessionInProgress=0; _session_id=ck9XdVl6VUpnOFBWSXNzM1FRUE9uZ21sL0RyUmYxN3dDbDBBRkFlazN3aXIrTkJSaWxubGFQUEQxMVBpanBNU1lpSXBjcGdMRytJeUdJS0laSUQ3Tzhzd1hFUk9HclMwYnNaMG9ERTdac2M2Ymc4UlR0QlhpaWxWazJwSXVNWnN5dnV6Ym80c2l0czRHbnNBZDN6Sm5wNlhHMFZ2d0lNV2x0Z2VMakg1SnI4bXlXRTB5RG5CWS9JY2VLMW1FVUhWU0VBNnRrcFNlTVoxbzIreXdHeEI3dHcxa3dETWEyTEE4NmtSazYrNUxubVhmSm5XMVdXdnE1K2xZYTgzWldKd2s1Vm9VRlh2YXl6cENGMVBTMWJvVlplWEh5dFhqOGxxRXNrUDFWcGkvMmtEeTZmYVlzaHlLRXBnTGJOUlBDSHl1UUdiWTduVXZQNFFkeERuSGdMY3p1ZStYVk9TTFdla3ZvVWliQmFBZGM2UjBLREZKMGxqN25SdFJOYmVZKzNERUM0L0NLakhIeUo3Vmk5QjdBNFExdHZRUWhaU0s1emVCaVRScHJ5NitYYz0tLVRWMk8zd2EyZ3FJWkN1SmxWR29FQ3c9PQ%3D%3D--b540678bfc29b4170744a30c68389596dcc1d9bc");

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                saida = System.Convert.ToString( objResponse);



                streamDados.Close();
                resposta.Close();
            }


            return saida;

        }

        public void preencherDados(string apiKey, ref Emails saida,string link)
        {
            string url = "";

            if(link != "" && link != null)
            {
                url = link ;
            }
            else
            {
                url = "https://api.smtplw.com.br/v1/messages?status=all&start_date=2022-04-11&end_date=2022-04-11&page=1&per=100";
            }

            var requisicaoWeb = WebRequest.CreateHttp(url);
            requisicaoWeb.Method = "GET";
            requisicaoWeb.UserAgent = "RequisicaoWebDemo";
            requisicaoWeb.Headers.Add("x-auth-token", apiKey);

            using (var resposta = requisicaoWeb.GetResponse())
            {
                var streamDados = resposta.GetResponseStream();
                StreamReader reader = new StreamReader(streamDados);
                object objResponse = reader.ReadToEnd();
                var emails = JsonConvert.DeserializeObject<Emails>(objResponse.ToString());

                saida = emails;
                streamDados.Close();
                resposta.Close();
            }
            


        }

}

public class Data
{
    public List<Message> messages { get; set; }
}

public class Message
{
    public int id { get; set; }
    public string subject { get; set; }
    public string sender { get; set; }
    public string recipient { get; set; }
    public DateTime sent_at { get; set; }
    public int account_id { get; set; }
    public DateTime created_at { get; set; }
    public DateTime updated_at { get; set; }
    public string uid { get; set; }
    public string status { get; set; }
    public object bounce_code { get; set; }
    public object bounced_at { get; set; }
    public string pool { get; set; }
    public object x_smtplw { get; set; }
    public bool is_spam { get; set; }
    public string date { get; set; }
    public DateTime? opened_at { get; set; }
    public object hard_bounce { get; set; }
    public object api_message_id { get; set; }
    public string bounce_description { get; set; }
}

public class Links
{
    public string self { get; set; }
    public string next { get; set; }
    public string prev { get; set; }
    public string first { get; set; }
    public string last { get; set; }
}


}
