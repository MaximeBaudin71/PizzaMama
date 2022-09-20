using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;

namespace pizza_mama.Pages.Admin
{
    

    public class IndexModel : PageModel
    {

        public bool adminErrorMessage = false;
        public bool isDeveloppment = false;
        /** Méthode : recuperer la configuration --> ce qu'il y a dans appsettings.json
         * 
         * 
         */
        IConfiguration configuration;
        public IndexModel(IConfiguration configuration, IWebHostEnvironment app)
        {
            this.configuration = configuration;

            if (app.IsDevelopment())
            {
                isDeveloppment = true;
            }

        }

        public IActionResult OnGet()
        {
            Console.WriteLine(HttpContext.User);
            if (HttpContext.User.Identity.IsAuthenticated) //Controle si l'utilisateur est déja authentifié
            {
                return Redirect("/Admin/Pizzas");
            }
            return Page();
        }

        /**
         * IActionResult permet de retourner la page
         * Task car fonction Asynchrone
         * @Returnurl : signifie l'url demandé
         */
        public async Task<IActionResult> OnPost(string username, string password, string ReturnUrl)
        {
            var authSection = configuration.GetSection("Auth"); //récupère la section Auth

            string adminLogin = authSection["AdminLogin"]; //récupère la valeur de AdminLogin 
            string adminPassword = authSection["AdminPassword"]; //récupère la valeur de AdminPassword

            if ((username == adminLogin) && (password == adminPassword))
            {
                var claims = new List<Claim> //Créé un claim pour accepter l'utilisateur
                {
                    new Claim(ClaimTypes.Name, username)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Login");
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new
               ClaimsPrincipal(claimsIdentity));
                return Redirect(ReturnUrl == null ? "/Admin/Pizzas" : ReturnUrl); //redirection vers admin/pizza si rien demandé, sinon l'url demandée
            }

            adminErrorMessage = true;
            return Page(); //retourne la page courante
        }


        /* Méthode : Permet à l'utilisateur de se déconnecter
         * 
         * 
         */
        public async Task<IActionResult> OnGetLogout()
        {
            await HttpContext.SignOutAsync(); //Demande de se déconnecter
            return Redirect("/Admin");
        }
    }
}
