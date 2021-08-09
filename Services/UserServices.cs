using gambistWinForm.Models;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace gambistWinForm.Services
{
    public class UserServices
    {
        static HttpClient client = new HttpClient();

        static UserServices()
        {
            client.BaseAddress = new Uri("http://localhost:8080/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public bool Login(LoginModel loginModel)
        {
            try
            {
                Task<HttpResponseMessage> addState = client.PostAsJsonAsync("authentification/login", loginModel);
                addState.Wait();

                if (addState.Result.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var contentResult = addState.Result.Content.ReadAsAsync<JObject>().Result;
                    if (contentResult["status"].ToString() == "200") 
                    {
                        return true;
                    }
                    else 
                    {
                        throw new Exception(contentResult["message"].ToString());
                    }
                }
                else
                {
                    throw new Exception("Authentification échouée. Vérifiez vos entrées");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
