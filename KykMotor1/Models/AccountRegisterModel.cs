using System.ComponentModel.DataAnnotations;

namespace KykMotorApp.WebIU.Models
{
	public class AccountRegisterModel
	{
		[Required(ErrorMessage = "Lütfen adınızı giriniz")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Lütfen soyadinizi giriniz")]
		public string SurName { get; set; }

		[Required(ErrorMessage = "Lütfen telefon numarası  giriniz")]
		public string Phone { get; set; }
		[Required(ErrorMessage = "Lütfen şehir giriniz")]
		public string Ctiy { get; set; }

		[Required(ErrorMessage = "Lütfen firma adı giriniz")]
		public string FirmaAdı { get; set; }

		[Required(ErrorMessage = "Lütfen Vergi dairesi adı giriniz")]
		public string VergiDairesi { get; set; }

		[Required(ErrorMessage = "Lütfen vergi no giriniz")]
		public string VergiNo { get; set; }
		[Required(ErrorMessage = "Lütfen kullanıcı adınızı giriniz")]
		public string UserName { get; set; }

		[Required(ErrorMessage = "Lütfen mail adresinizi giriniz")]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

        public bool IsActive { get; set; } = true;
		[Required(ErrorMessage = "Lütfen şifrenizi giriniz")]
		[DataType(DataType.Password)]
		public string Password { get; set; }

		

		[Required(ErrorMessage = "Lütfen şifrenizi tekrar giriniz")]
		[DataType(DataType.Password)]
		[Compare("Password", ErrorMessage = "Şifreler birbirine uyumlu degil !")]
		public string ConfirmPassword { get; set; }

	}
}
