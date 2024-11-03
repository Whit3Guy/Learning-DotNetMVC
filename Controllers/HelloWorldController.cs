using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;

namespace DotNetApplicationMVC.Controllers
{
    public class HelloWorldController : Controller
    {
        //Get https:Aplication/HelloWorld/
        // se tiver id, acho que não tem como ter outras instancias ou sla, alguma coisa assim meu Deus que sono
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult LoginWorld(int id)
        {
            ViewData["nums"] = id;
            return View();

        }

        public bool IsPar(int num)
        {
            return num % 2 == 0 ? true : false;
        }

        // Requires using System.Text.Encodings.Web;
        public string ShowId(string nome, int idade)
        {
            //return $"your name is {nome} and your age is {idade}";
            return HtmlEncoder.Default.Encode($"Your name is {nome} and your age is {idade}");
        }
    }
}
