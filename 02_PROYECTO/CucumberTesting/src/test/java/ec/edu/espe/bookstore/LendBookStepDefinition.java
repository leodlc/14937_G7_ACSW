package ec.edu.espe.bookstore;

import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;

import java.util.List;
import java.util.concurrent.TimeUnit;
import java.util.regex.Matcher;
import java.util.regex.Pattern;

import static org.junit.Assert.fail;

public class LendBookStepDefinition extends BasicStepDefinition {
    private static String nombreLibro;
    private static int bookId;
    private static int loanId;

    @Override
    protected String getFeatureName() {
        return "Prestamo de un Libro";
    }

    @Override
    protected String getDescription() {
        return "Quiero verificar que un libro pueda ser prestado correctamente";
    }

    @Override
    protected String getJiraIssueId() {
        return "SCRUM-11";
    }

    @Given("Quiero registrar el prestamo de un libro")
    public void quiero_registrar_el_prestamo_de_un_libro() {
        createPDF("PrestamoLibro");
        addText("Inicio de prueba: Quiero registrar el prestamo de un libro");

        driver.manage().timeouts().implicitlyWait(3, TimeUnit.SECONDS);
        driver.manage().window().maximize();
        driver.get("http://200.105.253.153:5275");
    }

    @When("Selecciono el libro en la pagina de inicio para prestar")
    public void selecciono_el_libro_en_la_pagina_de_inicio_para_prestar() {
        addText("Selecciono el libro en la pagina de inicio para prestar");
        WebElement button = driver.findElements(new By.ByCssSelector(".lend-book")).get(0);
        WebElement title = driver.findElements(new By.ByCssSelector(".card-title")).get(0);
        captureScreenShot();

        Pattern pattern = Pattern.compile(".+([0-9]+)");
        Matcher matcher = pattern.matcher(button.getAttribute("href"));
        bookId = matcher.find() ? Integer.parseInt(matcher.group(1)) : 0;
        nombreLibro = title.getText();
        button.click();
    }

    @When("Ingreso los datos cedula: {string} y correo: {string}")
    public void ingreso_los_datos_cedula_y_correo(String cedula, String correo) {
        addText("Ingreso los datos cedula: " + cedula + " y correo: " + correo);

        driver.findElement(new By.ByName("IdentificationNumber")).sendKeys(cedula);
        driver.findElement(new By.ByName("Email")).sendKeys(correo);

        WebElement button = driver.findElement(new By.ByCssSelector("[type='submit']"));
        captureScreenShot();
        button.click();
        wait(1);
        addText("Doy click en el botón de realizar préstamo");
        captureScreenShot();
    }

    @Then("El libro debe aparecer en la lista de prestamos")
    public void el_libro_debe_aparecer_en_la_lista_de_prestamos() {
        addText("El libro" + nombreLibro + "debe aparecer en la lista de préstamos");
        captureScreenShot();

        try {
            List<WebElement> cards = driver.findElements(new By.ByCssSelector(".card"));
            WebElement card = cards.get(0);
            List<WebElement> alerts = card.findElements(new By.ByCssSelector(".alert-info"));

            if (!alerts.isEmpty()) {
                addText("Prueba fallida: El libro " + nombreLibro + " no fue prestado correctamente");
                addPassOrFailMark(false);
                wait(1);
                this.driver.quit();
                closePDF();
                fail("El libro " + nombreLibro + " no fue prestado correctamente");
                return;
            }

            card.findElement(By.xpath("//*[text()='" + nombreLibro + "']"));

            WebElement button = driver.findElements(new By.ByCssSelector(".complete-loan")).get(0);
            button.click();
            wait(1);
            addText("Prueba exitosa: El libro " + nombreLibro + " fue prestado correctamente");
            addPassOrFailMark(true);
            this.driver.quit();
            closePDF();
        } catch (Exception ex) {
            addText("Prueba fallida: El libro " + nombreLibro + " no fue prestado correctamente");
            addPassOrFailMark(false);
            wait(1);
            this.driver.quit();
            closePDF();
            ex.printStackTrace();
            fail("El libro " + nombreLibro + " no fue prestado correctamente");
        }
    }
}
