using KykShopApp.Entities;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;

namespace KykMotorApp.WebIU.CustomValidator
{
	public class UserValidator : IUserValidator<ApplicationUser>
	{
		public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user)
		{
			var errors = new List<IdentityError>();
			// Kullanıcı adı kontrolü
			string userName = user.UserName; // kullanıcı adı
			if (string.IsNullOrEmpty(userName))
			{
				errors.Add(new IdentityError
				{
					Code = "UserNameEmpty",
					Description = "Kullanıcı adı boş olamaz."
				});
			}
			else if (!Regex.IsMatch(userName, @"^[a-zA-Z0-9.\-+]+$"))
			{
				errors.Add(new IdentityError
				{
					Code = "UserNameInvalidCharacters",
					Description = "Kullanıcı adı yalnızca harf, rakam, nokta, artı ve tire içerebilir."
				});
			}

			
			if (user.UserName.Length < 6)
			{
				errors.Add(new IdentityError
				{
					Code = "UserNameTooShort",
					Description = "Kullanıcı adı en az 6 karakter olmalıdır."
				});
			}

			// UserName büyük harf içermeli
			if (!user.UserName.Any(char.IsUpper))
			{
				errors.Add(new IdentityError
				{
					Code = "UserNameNoUppercase",
					Description = "Kullanıcı adı en az bir büyük harf içermelidir."
				});
			}

			// UserName küçük harf içermeli
			if (!user.UserName.Any(char.IsLower))
			{
				errors.Add(new IdentityError
				{
					Code = "UserNameNoLowercase",
					Description = "Kullanıcı adı en az bir küçük harf içermelidir."
				});
			}
			
			if (!Regex.IsMatch(user.SurName, @"^[a-zA-Z0-9.\-+]+$"))
			{
				errors.Add(new IdentityError
				{
					Code = "UserNameInvalidCharacters",
					Description = "Kullanıcı adı yalnızca harf, rakam, nokta, artı ve tire içerebilir."
				});
			}


			if (string.IsNullOrEmpty(user.UserName))
			{
				errors.Add(new IdentityError
				{
					Code = "UserNameEmpty",
					Description = "Kullanıcı adı boş olamaz."
				});
			}
			

			if (errors.Any())
			{
				return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
			}

			return Task.FromResult(IdentityResult.Success);
		}
	}
}

