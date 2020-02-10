using Microsoft.Extensions.Configuration;
using Novell.Directory.Ldap;
using System;
using System.Threading.Tasks;

namespace Infrastrucure.Security
{
    public class AuthenticationService : IAuthentication
    {
        private readonly IConfiguration configuration;

        public AuthenticationService(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public async Task<bool> ValidateUser(string username, string password)
        {
            var result = false;
            try
            {
                await Task.Run(() =>
                {
                    //Create new instance of LDAP Connection
                    using (var connection = new LdapConnection())
                    {

                        //Connect to ldap connection using host and default port
                        connection.Connect(GetHost(), LdapConnection.DEFAULT_PORT);
                        if (connection.Connected) // if connected then proceeed and validate user credentials
                        {
                            Console.WriteLine($"Login Attempt for {username}.");
                            connection.Bind(GetDN(username), password);
                            if (connection.Bound)
                            {
                                Console.WriteLine($"Login Attempt for {username} SUCCESS.");
                                result = true;
                            }
                        }
                    }
                });
            }
            catch (Exception e)
            {
                Console.WriteLine($"Login Attempt for {username} has FAILED. Unable to Connect to Server. Error: {e}");

            }
            return result;
        }

        public async Task<bool> SignInUser(string username)
        {
            return await Task.FromResult(true);
        }
        private string GetHost()
        {
            var host = configuration.GetSection("LDAP:HOST").Value;
            Console.WriteLine($"Fetching HOST from configuration file.");
            return host;
        }

        private string GetDN(string loginId)
        {

            var dn = $"{configuration.GetSection("LDAP:DOMAIN").Value}\\{loginId}";

            Console.WriteLine($"Fetching DN from  Configuration file.");
            return dn;
        }


    }
}




