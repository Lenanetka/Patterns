using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HTTPRequest
{
    public class RequestSync
    {
        private HttpClient client;
        private HttpResponseMessage response;
        private string baseURL;
        private string uriTemplate;
        private HttpMethod httpMethod;
        #region constructor
        public void setBaseURL(string baseURL)
        {
            this.baseURL = baseURL;
        }
        public void setUri(string uriTemplate)
        {
            this.uriTemplate = uriTemplate;
        }
        public void setHTTPMethod(HttpMethod httpMethod)
        {
            List<HttpMethod> supportedMethods = new List<HttpMethod> { HttpMethod.Get, HttpMethod.Delete, HttpMethod.Post, HttpMethod.Put };
            if (!supportedMethods.Contains(httpMethod)) throw new Exception("This HTTP method is not supported.");
            this.httpMethod = httpMethod;
        }
        public RequestSync(HttpMethod httpMethod, string baseURL, string uriTemplate)
        {
            setHTTPMethod(httpMethod);
            setBaseURL(baseURL);
            setUri(uriTemplate);
        }
        #endregion
        #region loginfo
        private string allInfo = "";
        public string infoAboutAllSentRequests()
        {
            return allInfo;
        }
        private void saveInfoAboutCurrentRequest()
        {
            allInfo += "\n***SEND REQUEST***\n" + infoAboutCurrentRequest();
        }
        public string infoAboutCurrentRequest()
        {
            return Task.Run(async () =>
            {
                return
                httpMethod.Method + " " + baseURL + uriTemplate +
                (parameters == null ? "" : "\nParameters:" + generateInfo(parameters)) +
                (contentType == null ? "" : "\nContent-Type:" + contentType) +
                (headers == null ? "" : "\nHeaders:" + generateInfo(headers)) +
                (body == null ? "" : "\nBody:" + generateInfo(body)) +
                (response == null ? "" : "\nResponse status code: " + response.StatusCode.ToString() + "\nResponse body:\n" + (await response.Content.ReadAsStringAsync()).Replace("{", "\n{\n").Replace("}", "\n}").Replace(",", ",\n"));
            }).Result;
        }
        private string generateInfo(Dictionary<string, string> info)
        {
            string insert = "";
            foreach (var i in info) insert += "\n" + i.Key + ":" + i.Value;
            return insert;
        }
        #endregion
        #region set request data
        private string contentType;
        private Dictionary<string, string> headers;
        private Dictionary<string, string> parameters;
        private Dictionary<string, string> body;        
        public void setContentType(string contentType)
        {
            this.contentType = contentType;
        }
        public void setHeaders(Dictionary<string, string> headers)
        {
            this.headers = headers;
        }
        public void setParameters(Dictionary<string, string> parameters)
        {
            this.parameters = parameters;
        }        
        public void setBody(Dictionary<string, string> body)
        {
            List<HttpMethod> supportedMethods = new List<HttpMethod> { HttpMethod.Post, HttpMethod.Put };
            if (!supportedMethods.Contains(httpMethod)) throw new Exception("This HTTP method does not require a body.");
            this.body = body;
        }        
        public void setData(Dictionary<string, string> headers, Dictionary<string, string> parameters)
        {
            if (headers != null) setHeaders(headers);
            if (parameters != null) setParameters(parameters);
        }
        
        public void setData(Dictionary<string, string> headers, Dictionary<string, string> parameters, Dictionary<string, string> body)
        {
            setData(headers, parameters);
            setBody(body);
        }  
        #endregion
        #region send request
        public HttpResponseMessage send()
        {
            return Task.Run(async () =>
            {
                client = new HttpClient();
                response = new HttpResponseMessage();
                Uri uri = parameters != null ? new UriTemplate(uriTemplate).BindByName(new Uri(baseURL), parameters) : new Uri(baseURL + uriTemplate);
                if (contentType != null) client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(contentType));
                if (headers != null) foreach (var head in headers) client.DefaultRequestHeaders.Add(head.Key, head.Value);
                switch (httpMethod.Method)
                {
                    case "GET":
                        response = await client.GetAsync(uri);
                        break;
                    case "DELETE":
                        response = await client.DeleteAsync(uri);
                        break;
                    case "POST":
                        response = await client.PostAsync(uri, new FormUrlEncodedContent(body == null ? new Dictionary<string, string>() : body));
                        break;
                    case "PUT":
                        response = await client.PutAsync(uri, new FormUrlEncodedContent(body == null ? new Dictionary<string, string>() : body));
                        break;
                }
                saveInfoAboutCurrentRequest();
                return response;
            }).Result;
        }
        #endregion
        #region response
        public HttpStatusCode responseStatusCode()
        {
            if (response == null) throw new Exception("Request is not sent yet");
            return response.StatusCode;
        }
        public string responseStringBody()
        {
            return Task.Run(async () =>
            {
                if (response == null) throw new Exception("Request is not sent yet");
                return await response.Content.ReadAsStringAsync();
            }).Result;
        }
        
        public JObject responseJsonBody()
        {
            return Task.Run(async () =>
            {
                if (response == null) throw new Exception("Request is not sent yet");
                string json = await response.Content.ReadAsStringAsync();
                return JObject.Parse(json.TrimStart(new char[] { '[' }).TrimEnd(new char[] { ']' }));
            }).Result;
        }       
        #endregion
    }
}
