using ServiceStack.Common.Web;
using ServiceStack.Logging;
using ServiceStack.ServiceHost;
using ServiceStack.WebHost.Endpoints.Support;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
using System.Web;

namespace SQ.Core.Services
{

    public class ApiCustomHttpHeaders
    {
        public static string UserId = "X-CUSTOM-API-USERID";
        public static string Signature = "X-CUSTOM-SIGNATURE";
        public static string Date = "X-CUSTOM-DATE";
    }
    public class UserRepository
    {
        public User GetById(int idUser)
        {
            return new User()
            {
                Id = 1,
                Name = "Jhon",
                Secret = "levismad",
                IsEnabled = true
            };
        }
    }
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Secret { get; set; }
        public bool IsEnabled { get; set; }
    }

    ///
    /// The filter will be execute on every request for every DTO or RestService with this Attribute:
    ///
    public class AuthSignatureRequired :
      ServiceStack.ServiceInterface.RequestFilterAttribute, IHasRequestFilter
    {
        private static readonly ILog Logger =
        LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public UserRepository UserRepository { get; set; }

        public new int Priority
        {
            // =0 Run after
            get { return -1; }
        }

        private bool CanExecute(IHttpRequest req)
        {
            DateTime requestDate;
            if (!DateTime.TryParse(ApiSignature.GetDate(req), out requestDate))
            {
                throw new SecurityException("You must provide a valid request date in the headers.");
            }

            var difference = requestDate.Subtract(DateTime.Now);
            if (difference.TotalMinutes > 15 || difference.TotalMinutes < -15)
            {
                throw new SecurityException(string.Format(
                        "The request timestamp must be within 15 minutes of the server time. Your request is {0} minutes compared to the server. Server time is currently {1} {2}",
                        difference.TotalMinutes,
                        DateTime.Now.ToLongDateString(),
                        DateTime.Now.ToLongTimeString()));
            }

            var userId = ApiSignature.GetUserId(req);
            if (userId <= 0)
            {
                throw new SecurityException("You must provide a valid API User Id with your request");
            }

            var signature = ApiSignature.GetSignature(req);
            if (string.IsNullOrEmpty(signature))
            {
                throw new SecurityException("You must provide a valid request signature (hash)");
            }

            var user = UserRepository.GetById(userId);
            if (user == null || user.Id == 0)
            {
                throw new SecurityException("Your API user id could not be found.");
            }

            if (!user.IsEnabled)
                throw new SecurityException("Your API user account has been disabled.");

            if (signature == ApiSignature.CreateToken(req, user.Secret))
            {
                Logger.InfoFormat("Successfully Authenticated {0}:{1} via signature hash", user.Id, user.Name);
                return true;
            }

            throw new SecurityException("Your request signature (hash) is invalid.");
        }

        public override void Execute(IHttpRequest req, IHttpResponse res, object requestDto)
        {
            var authErrorMessage = "";
            try
            {
                // Perform security check
                if (CanExecute(req))
                    return;
            }
            catch (Exception ex)
            {
                authErrorMessage = ex.Message;
                Logger.ErrorFormat("Blocked unauthorized request: {0} {1} by ip = {2} due to {3}",
                        req.HttpMethod,
                        req.AbsoluteUri,
                        req.UserHostAddress ?? "unknown",
                        authErrorMessage);
            }

            // Security failed!
            var message = "You are not authorized. " + authErrorMessage;
            //throw new HttpError(HttpStatusCode.Unauthorized, message);

            res.StatusCode = (int)HttpStatusCode.Unauthorized;
            res.StatusDescription = message;
            res.AddHeader(HttpHeaders.WwwAuthenticate, string.Format("{0} realm=\"{1}\"", "", "custom api"));
            res.ContentType = ContentType.PlainText;
            res.Write(message);
            res.Close();
        }
    }

    /// Static class will perform the flattening of the request and creation
    /// of the hash. This is designed to be used by the server and could be distributed
    /// as part of an SDK. The Test.aspx example uses this class.
    ///
    public static class ApiSignature
    {
        private static readonly ILog Logger =
        LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        /// Used by SDK and clients to make requests, so we must use the HttpWebRequest class
        public static string CreateToken(HttpWebRequest webRequest, string secret)
        {
            return CreateToken(
              FlattenRequestDetails(webRequest.Method,
                                    webRequest.RequestUri.AbsoluteUri,
                                    webRequest.ContentType,
                                    webRequest.Date.ToUniversalTime().ToString("r")
                ), secret);
        }

        /// Used by Server so we must use the Service Stack IHttpRequest
        ///
        public static string CreateToken(IHttpRequest request, string secret)
        {
            return CreateToken(
              FlattenRequestDetails(request.HttpMethod,
                                    request.AbsoluteUri,
                                    request.ContentType,
                                    GetDate(request)
                ), secret);
        }

        private static string CreateToken(string message, string secret)
        {
            // don't allow null secrets
            secret = secret ?? "";
            var encoding = new System.Text.ASCIIEncoding();
            byte[] keyByte = encoding.GetBytes(secret);
            byte[] messageBytes = encoding.GetBytes(message);
            using (var hmacsha256 = new System.Security.Cryptography.HMACSHA256(keyByte))
            {
                byte[] hashmessage = hmacsha256.ComputeHash(messageBytes);
                return Convert.ToBase64String(hashmessage);
            }
        }

        private static string FlattenRequestDetails(
        string httpMethod, string url, string contentType, string date)
        {
            // If it is a GET then we don't care about the contentType since there will never be contentTypes with GET.
            if (httpMethod.ToUpper() == "GET")
                contentType = "";

            var message = string.Format("{0}{1}{2}{3}", httpMethod, url, contentType, date);
            Logger.Debug("Request message to hash: " + message);
            return message;
        }

        /// If the user is providing the date via the custom header then the server
        /// will use that for the hash. Otherwise we check for the default "Date" header.
        /// This is nessary since some consumers can't control the date header in their web requests
        ///
        public static string GetDate(IHttpRequest request)
        {
            return request.Headers[ApiCustomHttpHeaders.Date] ?? request.Headers["Date"] ?? "";
        }

        public static int GetUserId(IHttpRequest req)
        {
            int userId = 0;
            var user = req.Headers[ApiCustomHttpHeaders.UserId] ?? "";
            int.TryParse(user, out userId);
            return userId;
        }

        public static string GetSignature(IHttpRequest req)
        {
            return req.Headers[ApiCustomHttpHeaders.Signature] ?? "";
        }
    }
}