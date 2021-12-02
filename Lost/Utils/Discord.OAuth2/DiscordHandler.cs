using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Text.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Lost.Model;
using Lost.DataAccess.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Principal;
using System.Linq;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Discord.OAuth2
{
    internal class DiscordHandler : OAuthHandler<DiscordOptions>
    {
        public DiscordHandler(IOptionsMonitor<DiscordOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
        }

        protected override async Task<AuthenticationTicket> CreateTicketAsync(ClaimsIdentity identity, AuthenticationProperties properties, OAuthTokenResponse tokens)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, Options.UserInformationEndpoint);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokens.AccessToken);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await Backchannel.SendAsync(request, Context.RequestAborted);
            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Failed to retrieve Discord user information ({response.StatusCode}).");

            

            var payload = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            var context = new OAuthCreatingTicketContext(new ClaimsPrincipal(identity), properties, Context, Scheme, Options, Backchannel, tokens, payload.RootElement);
            context.RunClaimActions();

            var discriminator = identity.Claims.ToList().FirstOrDefault(c => c.Type.Contains("discriminator"));
            string discordId = identity.Name + "#" + discriminator.Value;
            Utilisateur utilisateur = await UtilisateurDal.GetUtilisateurByDiscordId(discordId);
            if (utilisateur != null)
            {
                Claim iden = identity.Claims.ToList().FirstOrDefault(c => c.Value == identity.Name);
                identity.RemoveClaim(iden);
                Claim newClaim = new Claim(iden.Type, utilisateur.Personne.ToString(), iden.ValueType, iden.Issuer, iden.OriginalIssuer, iden.Subject);
                identity.AddClaim(newClaim);
                identity.AddClaim(new Claim(ClaimTypes.Role, utilisateur.Role));
            }

            await Events.CreatingTicket(context);
            return new AuthenticationTicket(context.Principal, context.Properties, Scheme.Name);
        }
    }
}
