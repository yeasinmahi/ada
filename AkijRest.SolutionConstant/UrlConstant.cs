namespace AkijRest.SolutionConstant
{
    public class UrlConstant
    {
        // constants related with Identity Server starts
        //******Identity Server Address for published. Comment out to active link*******
        //public const string IdentityServer = "https://agvdi3.akij.net/IdentityServer";

        //******Identity Server Address for Development. Comment out to active link******
        public const string IdentityServer = "https://localhost:44347";

        public const string IdentityServerToken = IdentityServer + "/connect/token";
        public const string IdentityServerAuthorize = IdentityServer + "/connect/authorize";
        public const string IdentityServerUserInfo = IdentityServer + "/connect/userinfo";
        public const string IdentityServerResources = IdentityServer + "/resources";
        // constants related with Identity Server ends

        //******WebClient Address for published. Comment out to active link*******
        //public const string WebClient = "https://agvdi3.akij.net/MenuLoad";

        //******WebClient Address for Development. Comment out to active link*******
        public const string WebClient = "https://localhost:44328";
    }
}