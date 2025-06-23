using KykShopApp.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Text;

namespace KykMotorApp.WebIU.TagHelpers
{
    
        [HtmlTargetElement("user-role-names")]
        public class UserRoleNamesTagHelper : TagHelper
        {
            public string UserId { get; set; } = null!;
            private readonly UserManager<ApplicationUser> _userManager;
            public UserRoleNamesTagHelper(UserManager<ApplicationUser> userManager)
            {
                _userManager = userManager;
            }

            public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
            {
                var user = await _userManager.FindByIdAsync(UserId);
                var userRoles = await _userManager.GetRolesAsync(user!);


                var stringBuilder = new StringBuilder();

                userRoles.ToList().ForEach(x =>
                {
                    stringBuilder.Append($@"
                        <span class='badge-primary badge-primary mr-1 '>{x.ToLower()}</span>"
                      );
                });
                output.Content.SetHtmlContent(stringBuilder.ToString());

            }

        }
    }

