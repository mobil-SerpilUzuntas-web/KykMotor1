using System.ComponentModel.DataAnnotations;

namespace KykMotorApp.WebIU.Models
{
	public class AccountLoginModel
	{
		[Required(ErrorMessage = "Kullanıcı Adı Boş Geçilemez")]
		public string UserName { get; set; }
		[Required(ErrorMessage = "Şifre Alanı Boş Geçilemez")]
		[DataType(DataType.Password)]
		public string Password { get; set; }
	}
}
