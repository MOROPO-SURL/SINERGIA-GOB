using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MOROPO.SINERGIAGOB.Core.Interop.API.Common.Models;
using MOROPO.SINERGIAGOB.Core.Presentation.Web.Frontend.Options;
using MOROPO.Framework.Guama.Core.Persistence.Repositories;
using MOROPO.Framework.Guama.Core.Presentation.Web.Security.Providers.Facade;
using MOROPO.Framework.Guama.Security.Personal.Persistence.Entities;

namespace MOROPO.SINERGIAGOB.Core.Presentation.Web.Frontend.Controllers.API
{
    [Route("api/fwrk/[controller]")]
    [ApiController]
    public class SecurityController : Framework.Guama.Core.Presentation.Web.Common.Controllers.BaseWebController
    {
        private readonly Microsoft.Extensions.Options.IOptions<JwtOptions> jwtOptions;
        private readonly IIdentityProviderFacade identityProvider;
        private readonly IEntityRepository<User, string> userRepository;

        public SecurityController(IEntityRepository<User, string> userRepository, IIdentityProviderFacade identityProvider, Microsoft.Extensions.Options.IOptions<JwtOptions> jwtOptions, Microsoft.Extensions.Localization.IStringLocalizer<SecurityController> localizer, ILogger<SecurityController> logger) : base(localizer, logger)
        {
            this.jwtOptions = jwtOptions;
            this.identityProvider = identityProvider;
            this.userRepository = userRepository;
        }

        [HttpPost("[action]")]
        public ActionResult<AuthenticatedSessionModel> Authenticate([FromBody] AuthenticationIntentModel model)
        {
            if (ModelState.IsValid)
            {
                var userIdentity = identityProvider.ProvideIdentity(model.User, model.Password, Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme);

                if (userIdentity != null)
                {
                    if (userIdentity.HasClaim(match => match.Type.Equals(Framework.Guama.Core.Presentation.Web.Security.Constants.ClaimTypes.AccountDisabled) && match.Value.Equals(true.ToString())))
                    {
                        return Forbid(Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme);
                    }

                    var user = userRepository.Get(model.User);

                    return GenerateToken(userIdentity, user);
                }

                return Unauthorized();
            }

            return BadRequest();
        }

        [HttpPost("[action]")]
        [Microsoft.AspNetCore.Authorization.Authorize]
        public ActionResult ChangePassword([FromBody] UserPasswordChangeRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var identityManager = identityProvider as Framework.Guama.Core.Presentation.Web.Security.Managers.Facade.IIdentityManagerFacade;

                if (identityManager.TryModifyPassword(User.Identity.Name, model.OldPassword, model.NewPassword))
                {
                    return Ok();
                }
            }

            return BadRequest();
        }

        #region Token Utility Methods

        private ActionResult GenerateToken(System.Security.Claims.ClaimsIdentity userClaims, User user)
        {
            var token = GenerateJwtToken(userClaims);

            var model = new AuthenticatedSessionModel
            {
                Token = token,
                Type = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme,
                ExpiresInSecond = jwtOptions.Value.ExpiresInSecond,
                User = new Interop.API.Common.Models.Display.UserDisplayModel()
            };

            model.User.Import(user);

            return Json(model);
        }

        private string GenerateJwtToken(System.Security.Claims.ClaimsIdentity userIdentity)
        {
            var key = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtOptions.Value.Key));
            var creds = new Microsoft.IdentityModel.Tokens.SigningCredentials(key, Microsoft.IdentityModel.Tokens.SecurityAlgorithms.HmacSha256);
            var expires = System.DateTime.Now.AddSeconds(jwtOptions.Value.ExpiresInSecond);

            var token = new System.IdentityModel.Tokens.Jwt.JwtSecurityToken(
                jwtOptions.Value.Issuer,
                jwtOptions.Value.Issuer,
                userIdentity.Claims,
                expires: expires,
                signingCredentials: creds
            );

            return new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler().WriteToken(token);
        }

        #endregion
    }
}
