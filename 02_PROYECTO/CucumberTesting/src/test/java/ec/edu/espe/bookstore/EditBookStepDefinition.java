package ec.edu.espe.bookstore;

import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;

import java.util.List;
import java.util.concurrent.TimeUnit;

import static org.junit.Assert.fail;

public class EditBookStepDefinition extends BasicStepDefinition {
    private String oldName;
    private String newName;

    @Override
    protected String getFeatureName() {
        return "Editar Libro";
    }

    @Override
    protected String getDescription() {
        return "Editar un libro existente";
    }

    @Override
    protected String getJiraIssueId() {
        return "SCRUM-2";
    }

    @Given("Quiero editar un libro")
    public void quiero_editar_un_libro() {
        createPDF("EditarLibro");
        addText("Inicio de prueba: Editar un libro");

        driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
        driver.manage().window().maximize();
        driver.get("http://200.105.253.153:5275/Home/AddBook");
    }

    @When("Creo un nuevo libro con nombre {string}")
    public void creo_un_nuevo_libro_con_el_nombre(String name) {
        addText("Creo un nuevo libro con el nombre: " + name);
        this.oldName = name;

        driver.findElement(new By.ByName("title")).sendKeys(name);
        driver.findElement(new By.ByName("isbn")).sendKeys("978-84-450-7379-1");
        driver.findElement(new By.ByName("publisher")).sendKeys("test");

        WebElement inputNumberYear = driver.findElement(new By.ByName("year"));
        WebElement inputNumberPrice = driver.findElement(new By.ByName("price"));

        inputNumberYear.clear();
        inputNumberYear.sendKeys("2020");

        inputNumberPrice.clear();
        inputNumberPrice.sendKeys("10");

        WebElement authorsSelect = driver.findElement(new By.ByName("AuthorId"));
        List<WebElement> authorOptions = authorsSelect.findElements(new By.ByTagName("option"));

        for (WebElement option : authorOptions) {
            if (option.getText().toLowerCase().contains("Orwell".toLowerCase())) {
                option.click();
                break;
            }
        }

        WebElement genresSelect = driver.findElement(new By.ByName("GenreIds"));
        List<WebElement> genreOptions = genresSelect.findElements(new By.ByTagName("option"));

        for (WebElement option : genreOptions) {
            if (option.getText().toLowerCase().contains("Aventura".toLowerCase())) {
                option.click();
                break;
            }
        }

        captureScreenShot();
        driver.findElement(new By.ByCssSelector("button[type='submit']")).click();
    }

    @When("Edito el nombre del libro {string} con el nuevo nombre {string}")
    public void edito_el_nombre_del_libro_con_el_nuevo_nombre(String oldName, String newName) {
        addText("Edito el nombre del libro: " + oldName + " con el nuevo nombre: " + newName);

        WebElement container = driver.findElement(By.cssSelector(".container .row .col"));
        List<WebElement> cards = container.findElements(By.cssSelector(".card"));
        WebElement card = cards.get(0);
        WebElement editButton = card.findElement(By.cssSelector(".edit-book"));

        editButton.click();

        driver.findElement(new By.ByName("title")).clear();
        driver.findElement(new By.ByName("title")).sendKeys(newName);
        captureScreenShot();
        driver.findElement(new By.ByCssSelector("button[type='submit']")).click();
    }

    @Then("El libro debe ser editado correctamente")
    public void el_libro_debe_ser_editado_correctamente() {
        addText("El libro debe ser editado correctamente");
        wait(2);
        captureScreenShot();

        try {
            WebElement container = driver.findElement(new By.ByCssSelector("div.container .row"));
            WebElement book = container.findElement(new By.ByCssSelector(".col:first-child"));
            boolean bookFound = book.findElement(new By.ByCssSelector("h5.card-title")).getText().toLowerCase().trim().equals(this.newName.toLowerCase().trim());

            if (bookFound) {
                addText("Prueba exitosa: El libro se editó correctamente");
                captureScreenShot();
                book.findElement(By.cssSelector(".delete-book")).click();
                this.driver.quit();
                addPassOrFailMark(true);
                closePDF();
                wait(1);
            } else {
                addText("Prueba fallida: El libro no se editó correctamente");
                captureScreenShot();
                book.findElement(By.cssSelector(".delete-book")).click();
                this.driver.quit();
                addPassOrFailMark(false);
                closePDF();
                wait(1);
                fail("El libro no se editó correctamente");
            }
        } catch (Exception e) {
            addText("Prueba fallida: El libro no se editó correctamente");
            captureScreenShot();
            this.driver.quit();
            addPassOrFailMark(false);
            closePDF();
            wait(1);
            fail("El libro no se editó correctamente");
        }
    }
}
