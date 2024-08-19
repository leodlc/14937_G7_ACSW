package ec.edu.espe.bookstore;

import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;

import java.util.concurrent.TimeUnit;

import static org.junit.Assert.fail;

public class EditAuthorStepDefinition extends BasicStepDefinition {
    private String oldName;
    private String newName;

    @Override
    protected String getFeatureName() {
        return "Editar Autor";
    }

    @Override
    protected String getDescription() {
        return "Editar un autor existente";
    }

    @Override
    protected String getJiraIssueId() {
        return "SCRUM-7";
    }

    @Given("Quiero editar un autor")
    public void quiero_editar_un_autor() {
        createPDF("EditarAutor");
        addText("Inicio de prueba: Editar un autor");

        driver.manage().timeouts().implicitlyWait(3, TimeUnit.SECONDS);
        driver.manage().window().maximize();
        driver.get("http://200.105.253.153:5275/Authors/AddAuthor");
    }

    @When("Creo un nuevo autor con nombre {string} y apellido {string}")
    public void creo_un_nuevo_autor_con_nombre_y_apellido(String name, String lastName) {
        addText("Creo un nuevo autor con nombre: " + name + " y apellido: " + lastName);

        driver.findElement(new By.ByName("Firstname")).sendKeys(name);
        driver.findElement(new By.ByName("Lastname")).sendKeys(lastName);
        captureScreenShot();
        driver.findElement(new By.ByCssSelector("button[type='submit']")).click();
    }

    @When("Edito el nombre del autor {string} con el nuevo nombre {string}")
    public void edito_el_nombre_del_autor_con_el_nuevo_nombre(String oldName, String newName) {
        addText("Edito el nombre del autor: " + oldName + " con el nuevo nombre: " + newName);
        this.oldName = oldName;
        this.newName = newName;

        WebElement table = driver.findElement(new By.ByCssSelector(".table tbody"));
        WebElement author = table.findElement(new By.ByCssSelector("tr:last-child"));
        author.findElement(new By.ByCssSelector("a.edit")).click();

        driver.findElement(new By.ByName("Firstname")).clear();
        driver.findElement(new By.ByName("Firstname")).sendKeys(newName);
        captureScreenShot();
        driver.findElement(new By.ByCssSelector("button[type='submit']")).click();
    }

    @Then("El autor debe ser editado correctamente")
    public void el_autor_debe_ser_editado_correctamente() {
        addText("El autor debe ser editado correctamente");
        wait(1);
        captureScreenShot();

        try {
            WebElement container = driver.findElement(new By.ByCssSelector(".table tbody"));
            WebElement author = container.findElement(new By.ByCssSelector("tr:last-child"));
            String name = author.findElement(new By.ByCssSelector("td:nth-child(2)")).getText();
            boolean authorFound = name.equals(newName);

            if (authorFound) {
                addText("Prueba exitosa: El autor se editó correctamente");
                captureScreenShot();
                author.findElement(By.cssSelector(".delete")).click();
                this.driver.quit();
                addPassOrFailMark(true);
                closePDF();
                wait(1);
            } else {
                addText("Prueba fallida: El autor no se editó correctamente");
                captureScreenShot();
                driver.get("http://200.105.253.153:5275/Authors");
                container = driver.findElement(new By.ByCssSelector(".table tbody"));
                author = container.findElement(new By.ByCssSelector("tr:last-child"));
                author.findElement(By.cssSelector(".delete")).click();
                this.driver.quit();
                addPassOrFailMark(false);
                closePDF();
                wait(1);
                fail("El autor no se editó correctamente");
            }
        } catch (Exception e) {
            addText("Prueba fallida: El autor no se editó correctamente");
            captureScreenShot();
            driver.get("http://200.105.253.153:5275/Authors");
            WebElement container = driver.findElement(new By.ByCssSelector(".table tbody"));
            WebElement author = container.findElement(new By.ByCssSelector("tr:last-child"));
            author.findElement(By.cssSelector(".delete")).click();
            this.driver.quit();
            addPassOrFailMark(false);
            closePDF();
            e.printStackTrace();
            fail("El autor no se editó correctamente");
        }
    }
}
