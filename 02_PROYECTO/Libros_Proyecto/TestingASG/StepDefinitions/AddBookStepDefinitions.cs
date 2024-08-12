using System;
using TechTalk.SpecFlow;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace TestingASG.StepDefinitions
{
    [Binding]
    public sealed class AddBookStepDefinitions
    {
        private IWebDriver _driver;
        private string _nombre;

        [Given(@"Quiero agregar un libro en la secci�n de libros")]
        public void GivenQuieroAgregarUnLibroEnLaSeccionDeLibros()
        {
            _driver = new ChromeDriver();
            _driver.Navigate().GoToUrl("http://localhost:49797/Book");
        }

        [When(@"rellene los campos de ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)"", ""(.*)""")]
        public void WhenRelleneLosCamposDe(string nombre, string a�oPublicacion, string isbn, string editorial, string stock, string generoLiterario, string autor)
        {
            _nombre = nombre;  // Guardamos el nombre en una variable de clase para usarlo despu�s

            // Hacer clic en el bot�n "Agregar Libro" para abrir el modal
            _driver.FindElement(By.CssSelector("button[data-target='#createModal']")).Click();

            // Espera a que el modal est� visible
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.Id("createModal")).Displayed);

            // Rellenar el formulario dentro del modal
            _driver.FindElement(By.Id("createModal")).FindElement(By.Id("NOMBRELIBRO")).SendKeys(nombre);
            _driver.FindElement(By.Id("createModal")).FindElement(By.Id("ANIOPUBLIBRO")).SendKeys(a�oPublicacion);
            _driver.FindElement(By.Id("createModal")).FindElement(By.Id("ISBNLIBRO")).SendKeys(isbn);
            _driver.FindElement(By.Id("createModal")).FindElement(By.Id("EDITORIALLIBRO")).SendKeys(editorial);
            _driver.FindElement(By.Id("createModal")).FindElement(By.Id("STOCKLIBRO")).SendKeys(stock);

            var generoSelect = _driver.FindElement(By.Id("createModal")).FindElement(By.Id("IDGL"));
            var generoOptions = generoSelect.FindElements(By.TagName("option"));
            foreach (var option in generoOptions)
            {
                if (option.Text == generoLiterario)
                {
                    option.Click();
                    break;
                }
            }

            var autorSelect = _driver.FindElement(By.Id("createModal")).FindElement(By.Id("IDAUTOR"));
            var autorOptions = autorSelect.FindElements(By.TagName("option"));
            foreach (var option in autorOptions)
            {
                if (option.Text == autor)
                {
                    option.Click();
                    break;
                }
            }

            // Hacer clic en el bot�n de guardar dentro del modal
            _driver.FindElement(By.Id("createModal")).FindElement(By.CssSelector("input[type='submit']")).Click();
        }

        [Then(@"compruebo que el libro se agreg� en la ventana principal /books")]
        public void ThenComprueboQueElLibroSeAgregoEnLaVentanaPrincipal()
        {
            // Espera expl�cita para asegurar que el libro se haya agregado y que la p�gina se haya actualizado.
            var wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(By.XPath("//h5[contains(text(), '" + _nombre + "')]")).Displayed);

            var addedBook = _driver.FindElement(By.XPath("//h5[contains(text(), '" + _nombre + "')]"));
            if (addedBook == null)
            {
                throw new Exception("El libro no se encontr� en la lista principal");
            }

            _driver.Quit();
        }
    }
}
