using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Windows.Admin.Infrastructure.Configuration;
using Windows.Infrastructure.Extensions;

namespace Windows.Infrastructure.Utils
{
    public class JwtUtils
    {
        /// <summary>
        /// 生成JwtToken
        /// </summary>
        /// <param name="payload">不敏感的用户数据</param>
        /// <returns></returns>
        public static string SetJwtEncode(Token model)
        {
            DateTime expTime = DateTime.Now.AddMinutes(AppSetting.Jwt.ExpireMinutes);
            string exp = $"{new DateTimeOffset(expTime).ToUnixTimeSeconds()}";
            string now = $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}";
           
            var claims = new List<Claim>
            {
                //new Claim(ClaimTypes.Role,userInfo.Role_Id ),
                new Claim(JwtRegisteredClaimNames.Jti,model.UserId.ToString()),
                new Claim(ClaimTypes.Name,model.UserName),
                //签发时间
                new Claim(JwtRegisteredClaimNames.Iat,now ),
                //生效时间
                new Claim(JwtRegisteredClaimNames.Nbf,now) ,
                //JWT过期时间
                //默认设置jwt过期时间20分钟
                new Claim (JwtRegisteredClaimNames.Exp,exp),
                new Claim(ClaimTypes.Expiration, expTime.ToString()),
                //发行人
                new Claim(JwtRegisteredClaimNames.Iss,AppSetting.Jwt.Issuer),
                //订阅者
                new Claim(JwtRegisteredClaimNames.Aud,AppSetting.Jwt.Audience),
            };
            //claims.AddRange(model.RoleIds.Split(',').Select(s => new Claim(ClaimTypes.Role, s)));
            //秘钥16位
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSetting.Jwt.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            JwtSecurityToken securityToken = new JwtSecurityToken(
                issuer: AppSetting.Jwt.Issuer,
                claims: claims, 
                signingCredentials: creds);
            string token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
        /// <summary>
        /// 根据jwtToken  获取实体
        /// </summary>
        /// <param name="token">jwtToken</param>
        /// <returns></returns>
        public static bool TryGetJwtDecode(string token, out Token model)
        {
            var jwtHandler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtToken = jwtHandler.ReadJwtToken(token);
            DateTime expDate = (jwtToken.Payload[JwtRegisteredClaimNames.Exp] ?? 0).ToInt().TimeStamp2DateTime();
            if (expDate < DateTime.Now)
            {
                model = null;
                return false;
            }
            model = new Token
            {
                UserId = jwtToken.Id.ToGuid(),
                UserName = jwtToken.Payload[ClaimTypes.Name]?.ToString()
            };
            return true;
        }
    }
}
