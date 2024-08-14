package ec.edu.espe.bookstore;

import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;

import java.util.concurrent.TimeUnit;

import static org.junit.Assert.fail;

public class AddAutorStepDefinition extends BasicStepDefinition {
    @Override
    protected String getFeatureName() {
        return "Agregar un Autor";
    }

    @Override
    protected String getDescription() {
        return "Quiero verificar que un autor pueda ser agregado correctamente";
    }

    @Override
    protected String getJiraIssueId() {
        return "SCRUM-6";
    }

    @Given("Quiero agregar un nuevo autor")
    public void quiero_agregar_un_nuevo_autor() {
        createPDF("AgregarAutor");
        addText("Inicio de prueba: Quiero agregar un nuevo autor");

        driver.manage().timeouts().implicitlyWait(10, TimeUnit.SECONDS);
        driver.manage().window().maximize();
        driver.get("http://200.105.253.153:5275/Authors/AddAuthor");
    }

    @When("Relleno los campos, nombre: {string}, apellido: {string}, pseudónimo: {string}")
    public void relleno_los_campos_nombre_apellido_pseudonimo(String firstName, String lastName, String pseudonym) {
        addText("Relleno los campos nombre: " + firstName + ", apellido: " + lastName + ", pseudónimo: " + pseudonym);

        driver.findElement(new By.ByName("Firstname")).sendKeys(firstName);
        driver.findElement(new By.ByName("Lastname")).sendKeys(lastName);
        driver.findElement(new By.ByName("Pseudonym")).sendKeys(pseudonym);
        captureScreenShot();
        driver.findElement(new By.ByCssSelector("button[type='submit']")).click();
    }

    @Then("El autor {string} {string} debe ser agregado correctamente")
    public void el_autor_debe_ser_agregado_correctamente(String firstName, String lastName) {
        wait(1);
        addText("El autor debe ser agregado correctamente");
        captureScreenShot();

        String name = firstName + " " + lastName;

        try {
            WebElement container = driver.findElement(new By.ByCssSelector(".table tbody"));
            WebElement author = container.findElement(new By.ByCssSelector("tr:last-child"));

            WebElement firstNameEl = author.findElement(new By.ByCssSelector("td:nth-child(2)"));
            WebElement lastNameEl = author.findElement(new By.ByCssSelector("td:nth-child(3)"));

            boolean authorFound = firstNameEl.getText().equals(firstName);
            authorFound = authorFound && lastNameEl.getText().equals(lastName);

            if (authorFound) {
                addText("Prueba correcta: El autor " + name + " fue agregado correctamente");
                addPassOrFailMark(true);
            } else {
                addText("Prueba fallida: El autor " + name + " no fue agregado correctamente");
                addPassOrFailMark(false);
            }

            author.findElement(new By.ByCssSelector("a.delete")).click();
            wait(1);
            this.driver.quit();
            closePDF();
        } catch (Exception ex) {
            addText("Prueba fallida: El autor " + name + " no fue agregado correctamente");
            addPassOrFailMark(false);
            wait(1);
            this.driver.quit();
            closePDF();
            ex.printStackTrace();
            fail("El autor " + name + " no fue agregado correctamente");
        }
    }
}
