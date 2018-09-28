using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace SimpleBBS.Web
{
    public static class IdentityExtensions
    {
        /// <summary>
        ///  Return the user id using the UserIdClaimType
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static T GetUserId<T>(this IIdentity identity) where T : IConvertible
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                string text = claimsIdentity.FindFirstValue(ClaimTypes.NameIdentifier);
                if (text != null)
                {
                    return (T)((object)Convert.ChangeType(text, typeof(T), CultureInfo.InvariantCulture));
                }
            }
            return default(T);
        }

        /// <summary>
        ///     Return the user name using the UserNameClaimType
        /// </summary>
        /// <param name="identity"></param>
        /// <returns></returns>
        public static string GetUserName(this IIdentity identity)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            ClaimsIdentity claimsIdentity = identity as ClaimsIdentity;
            if (claimsIdentity != null)
            {
                return claimsIdentity.FindFirstValue(ClaimTypes.Name);
            }
            return null;
        }

        /// <summary>
        ///     Return the claim value for the first claim with the specified type if it exists, null otherwise
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="claimType"></param>
        /// <returns></returns>
        public static string FindFirstValue(this ClaimsIdentity identity, string claimType)
        {
            if (identity == null)
            {
                throw new ArgumentNullException("identity");
            }
            Claim claim = identity.FindFirst(claimType);
            if (claim == null)
            {
                return null;
            }
            return claim.Value;
        }
    }
}
