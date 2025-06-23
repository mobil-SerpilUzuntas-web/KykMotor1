using KykShopApp.Entities;
using Microsoft.AspNetCore.Identity;

namespace KykMotorApp.WebIU.CustomValidator
{
	public class PaswordValidator : IPasswordValidator<ApplicationUser>
	{
		public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password)
		{
			var errors = new List<IdentityError>();

			// Şifre kullanıcı adını içeremez
			if (password!.Contains(user.UserName!))
			{
				errors.Add(new()
				{
					Code = "PasswordContainUserName",
					Description = "Şifre alanı kullanıcı adı içeremez"
				});
			}

			// Şifre ardışık sayı içeremez
			if (password.StartsWith("123456"))
			{
				errors.Add(new()
				{
					Code = "PasswordContain123456",
					Description = "Şifre alanı ardışık sayı içeremez"
				});
			}

			// Şifre en az bir büyük harf içermelidir
			if (!password.Any(char.IsUpper))
			{
				errors.Add(new()
				{
					Code = "PasswordMustContainUppercase",
					Description = "Şifre en az bir büyük harf içermelidir (A-Z)"
				});
			}

			// Şifre en az bir büyük harf içermelidir
			if (!password.Any(char.IsLower))
			{
				errors.Add(new()
				{
					Code = "PasswordMustContainUppercase",
					Description = "Şifre en az bir küçük harf içermelidir (a-z)"
				});
			}
			// Şifre en az bir alfanumerik olmayan karakter içermelidir
			if (!password.Any(c => !char.IsLetterOrDigit(c)))
			{
				errors.Add(new()
				{
					Code = "PasswordMustContainNonAlphanumeric",
					Description = "Şifre en az bir alfanumerik olmayan karakter içermelidir"
				});
			}

			// Eğer herhangi bir hata varsa, bunları döndür
			if (errors.Any())
			{
				return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
			}

			// Şifre geçerliyse
			return Task.FromResult(IdentityResult.Success);
		}
	}
}



//using KykShopApp.Entities;
//using Microsoft.AspNetCore.Identity;

//namespace KykMotorApp.WebIU.CustomValidator
//{
//    public class PaswordValidator : IPasswordValidator<ApplicationUser>
//    {
//        public Task<IdentityResult> ValidateAsync(UserManager<ApplicationUser> manager, ApplicationUser user, string password)
//        {
//            var errors = new List<IdentityError>();
//			// Şifre kullanıcı adını içeremez
//			if (password!.Contains(user.UserName!))
//			{
//				errors.Add(new()
//				{
//					Code = "PasswordNoContainUserName",
//					Description = "Şifre alanı kullanıcı adı içeremez"
//				});
//			}

//			// Şifre ardışık sayı içeremez
//			if (password.StartsWith("123456"))
//			{
//				errors.Add(new()
//				{
//					Code = "PasswordNoContain123456",
//					Description = "Şifre alanı ardışık sayı içeremez"
//				});
//			}

//			// Şifre en az bir büyük harf içermelidir
//			if (!password.Any(char.IsUpper))
//			{
//				errors.Add(new()
//				{
//					Code = "PasswordMustContainUppercase",
//					Description = "Şifre en az bir büyük harf içermelidir (A-Z)"
//				});
//			}

//			// Şifre en az bir alfanumerik olmayan karakter içermelidir
//			if (!password.Any(c => !char.IsLetterOrDigit(c)))
//			{
//				errors.Add(new()
//				{
//					Code = "PasswordMustContainNonAlphanumeric",
//					Description = "Şifre en az bir alfanumerik olmayan karakter içermelidir"
//				});
//			}

//			if (errors.Any())
//            {
//                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));

//            }
//            return Task.FromResult(IdentityResult.Success);
//        }
//    }
//}