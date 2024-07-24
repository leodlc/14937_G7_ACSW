package ec.edu.espe.bookstore;

import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import org.openqa.selenium.By;
import org.openqa.selenium.JavascriptExecutor;
import org.openqa.selenium.WebElement;

import java.util.List;
import java.util.concurrent.TimeUnit;

import static org.junit.Assert.fail;

public class AddBookStepDefinition extends BasicStepDefinition {
    @Given("Quiero agregar un nuevo libro")
    public void quiero_agregar_un_nuevo_libro() {
        createPDF("AgregarLibro");
        addText("Inicio de prueba: Quiero agregar un nuevo libro");

        driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
        driver.manage().window().maximize();
        driver.get("http://localhost:5275/Home/AddBook");
    }

    @When("Relleno los campos, nombre: {string}, isbn: {string}, editorial: {string}, precio: {string}, autor: {string}, año: {string}, genero: {string}")
    public void relleno_los_campos_nombre_isbn_editorial_precio_autor_año_genero(String name, String isbn, String editorial, String price, String author, String year, String genre) {
        addText("Relleno los campos nombre: " + name + ", isbn: " + isbn + ", editorial: " + editorial + ", autor: " + author + ", año: " + year + ", genero: " + genre);

        driver.findElement(new By.ByName("title")).sendKeys(name);
        driver.findElement(new By.ByName("isbn")).sendKeys(isbn);
        driver.findElement(new By.ByName("publisher")).sendKeys(editorial);

        WebElement inputNumberYear = driver.findElement(new By.ByName("year"));
        WebElement inputNumberPrice = driver.findElement(new By.ByName("price"));

        inputNumberYear.clear();
        inputNumberYear.sendKeys(year);

        inputNumberPrice.clear();
        inputNumberPrice.sendKeys(price);

        WebElement authorsSelect = driver.findElement(new By.ByName("AuthorId"));
        List<WebElement> authorOptions = authorsSelect.findElements(new By.ByTagName("option"));

        for (WebElement option : authorOptions) {
            if (option.getText().toLowerCase().contains(author.toLowerCase())) {
                option.click();
                break;
            }
        }

        WebElement genresSelect = driver.findElement(new By.ByName("GenreIds"));
        List<WebElement> genreOptions = genresSelect.findElements(new By.ByTagName("option"));

        for (WebElement option : genreOptions) {
            if (option.getText().toLowerCase().contains(genre.toLowerCase())) {
                option.click();
                break;
            }
        }

        captureScreenShot();
        driver.findElement(new By.ByCssSelector("button[type='submit']")).click();
    }

    @Then("El libro {string} debe ser agregado correctamente")
    public void el_libro_debe_ser_agregado_correctamente(String name) {
        addText("El libro debe ser agregado correctamente");
        wait(2);
        captureScreenShot();

        try {
            WebElement container = driver.findElement(new By.ByCssSelector("div.container .row"));
            WebElement book = container.findElement(new By.ByCssSelector(".col:first-child"));
            boolean bookFound = book.findElement(new By.ByCssSelector("h5.card-title")).getText().toLowerCase().contains(name.toLowerCase());

            if (bookFound) {
                addText("Prueba correcta: El libro " + name + " fue agregado correctamente");
            } else {
                addText("Prueba fallida: El libro " + name + " no fue agregado correctamente");
            }

            book.findElement(new By.ByCssSelector(".actions ul li:last-child a")).click();
            wait(1);
            this.driver.quit();
            closePDF();
        } catch (Exception ex) {
            addText("Prueba fallida: El libro " + name + " no fue agregado correctamente");
            wait(1);
            this.driver.quit();
            closePDF();
            ex.printStackTrace();
            fail("El libro " + name + " no fue agregado correctamente");
        }
    }
}
