using FirebaseAdmin.Auth;
using FirebaseAdmin;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace FindJobAPI
{
    public class FirebaseAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly FirebaseAuth _auth;

        public FirebaseAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock, FirebaseApp app) : base(options, logger, encoder, clock)
        {
            _auth = FirebaseAuth.GetAuth(app);
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Context.Request.Headers.ContainsKey("Authorization")) return AuthenticateResult.NoResult();
            var bearer = Context.Request.Headers["Authorization"].ToString();
            if (bearer is null || !bearer.StartsWith("Bearer ")) return AuthenticateResult.Fail("Invalid scheme");

            var token = bearer.Substring("Bearer ".Length);

            try
            {
                var firebaseToken = await _auth.VerifyIdTokenAsync(token);
                return AuthenticateResult.Success(CreateAuthenticationTicket(firebaseToken));
            }
            catch (Exception ex)
            {
                return AuthenticateResult.Fail(ex);
            }
        }

        private AuthenticationTicket CreateAuthenticationTicket(FirebaseToken firebaseToken)
        {
            var claimsPrincipal = new ClaimsPrincipal(new List<ClaimsIdentity>()
        {
            new(ToClaims(firebaseToken.Claims), nameof(ClaimsIdentity))
        });

            return new AuthenticationTicket(claimsPrincipal, JwtBearerDefaults.AuthenticationScheme);
        }

        private static IEnumerable<Claim> ToClaims(IReadOnlyDictionary<string, object> claims)
        {
            return new List<Claim>
        {
            new(FirebaseUserClaimType.Id, claims.GetValueOrDefault("user_id", "").ToString() ?? string.Empty),
            new(FirebaseUserClaimType.Email, claims.GetValueOrDefault("email", "").ToString() ?? string.Empty),
            new(FirebaseUserClaimType.Username, claims.GetValueOrDefault("name", "").ToString() ?? string.Empty),
            new(FirebaseUserClaimType.Avatar, claims.GetValueOrDefault("picture", "").ToString() ?? string.Empty),
            new(FirebaseUserClaimType.Phone, claims.GetValueOrDefault("phone", "").ToString() ?? string.Empty),
            new(FirebaseUserClaimType.Admin, claims.GetValueOrDefault("admin", "").ToString() ?? string.Empty),
        };
        }
    }

    public static class FirebaseUserClaimType
    {
        public const string Id = "id";
        public const string Email = "email";
        public const string Username = "username";
        public const string Avatar = "avatar";
        public const string Phone = "phone";
        public const string Admin = "admin";
    }
}
