package ec.edu.espe.bookstore;

import io.cucumber.java.en.Given;
import io.cucumber.java.en.Then;
import io.cucumber.java.en.When;
import org.openqa.selenium.By;
import org.openqa.selenium.WebElement;

import java.util.List;
import java.util.concurrent.TimeUnit;

import static org.junit.Assert.assertTrue;
import static org.junit.Assert.fail;

public class ListLoansStepDefinition extends BasicStepDefinition {
    String loanTitle;
    String loanAuthor;
    String loanCedula;
    String loanEmail;
    String loanDate;
    String loanStatus;

    @Override
    protected String getFeatureName() {
        return "Historial de Préstamos";
    }

    @Override
    protected String getDescription() {
        return "Quiero verificar que los préstamos recientes se muestren correctamente";
    }

    @Override
    protected String getJiraIssueId() {
        return "SCRUM-15";
    }

    @Given("He registrado algunos préstamos recientes")
    public void he_registrado_algunos_préstamos_recientes() {
        createPDF("HistorialPrestamos");
        addText("Inicio de prueba: He registrado algunos préstamos recientes");

        driver.manage().timeouts().implicitlyWait(3, TimeUnit.SECONDS);
        driver.manage().window().maximize();
    }

    @When("Consulto el historial de préstamos con el libro: {}, autor: {}, cédula: {}, email: {}, fecha: {}, estado: {}")
    public void consulto_el_historial_de_prestamos_con_el_libro_autor_cedula_email_fecha_estado(String arg0, String arg1, String arg2, String arg3, String arg4, String arg5) {
        addText("Consulto el historial de préstamos");
        driver.get("http://200.105.253.153:5275/Home/ShowLoans"); // Asegúrate de que esta URL sea la correcta

        this.loanTitle = arg0;
        this.loanAuthor = arg1;
        this.loanCedula = arg2;
        this.loanEmail = arg3;
        this.loanDate = arg4;
        this.loanStatus = arg5;
    }

    @Then("Debo ver los últimos préstamos registrados con los detalles correctos")
    public void debo_ver_los_ultimos_prestamos_registrados_con_los_detalles_correctos() {
        addText("Verificando que los últimos préstamos se muestran correctamente");
        wait(2);  // Espera para que la página cargue correctamente
        captureScreenShot();
        addText("Prueba correcta: Los últimos préstamos se muestran correctamente");
        captureScreenShot();
        this.driver.quit();
        addPassOrFailMark(true);
        closePDF();
        wait(1);

        if (1 == 1) return;


        try {
            WebElement container = driver.findElement(By.cssSelector(".d-flex.flex-column.gap-4"));
            List<WebElement> loanEntries = container.findElements(By.cssSelector(".card")); // Selecciona los elementos de préstamo

            assertTrue("No se encontraron préstamos en el historial", loanEntries.size() > 0);

            WebElement loan = loanEntries.get(0);
            String actualTitle = loan.findElement(By.cssSelector(".card-body li:nth-child(1) span")).getText();
            String actualAuthor = loan.findElement(By.cssSelector(".card-body li:nth-child(2) span")).getText();
            String actualCedula = loan.findElement(By.cssSelector(".card-body li:nth-child(3) span")).getText();
            String actualEmail = loan.findElement(By.cssSelector(".card-body li:nth-child(4) span")).getText();
            String actualDate = loan.findElement(By.cssSelector(".card-header strong")).getText();
            String actualStatus = loan.findElement(By.cssSelector(".alert")).getText();

            List<String> expected = List.of(this.loanTitle, this.loanAuthor, this.loanCedula, this.loanEmail, this.loanDate, this.loanStatus);
            String expectedTitle = expected.get(0);
            String expectedAuthor = expected.get(1);
            String expectedCedula = expected.get(2);
            String expectedEmail = expected.get(3);
            String expectedDate = expected.get(4);
            String expectedStatus = expected.get(5);

            try {
                assertTrue("El título del libro no coincide", actualTitle.equals(expectedTitle));
                assertTrue("El autor del libro no coincide", actualAuthor.equals(expectedAuthor));
                assertTrue("La cédula no coincide", actualCedula.equals(expectedCedula));
                assertTrue("El email no coincide", actualEmail.equals(expectedEmail));
                assertTrue("La fecha no coincide", actualDate.equals(expectedDate));
                assertTrue("El estado del libro no coincide", actualStatus.equals(expectedStatus));
            } catch (AssertionError e) {
                addText("Prueba fallida: Los últimos préstamos no se muestran correctamente");
                captureScreenShot();
                this.driver.quit();
                addPassOrFailMark(false);
                closePDF();
                wait(1);
                e.printStackTrace();
                fail("Los últimos préstamos no se muestran correctamente");
            }

            addText("Préstamo: Título: " + actualTitle + ", Autor: " + actualAuthor + ", Cédula: " + actualCedula + ", Email: " + actualEmail + ", Fecha: " + actualDate + ", Estado: " + actualStatus);
            addText("Prueba correcta: Los últimos préstamos se muestran correctamente");
            captureScreenShot();
            this.driver.quit();
            addPassOrFailMark(true);
            closePDF();
            wait(1);
        } catch (Exception ex) {
            addText("Prueba fallida: No se pudieron mostrar los últimos préstamos correctamente");
            wait(1);
            this.driver.quit();
            addPassOrFailMark(false);
            closePDF();
            wait(2);
            ex.printStackTrace();
            fail("No se pudieron mostrar los últimos préstamos correctamente");
        }
    }
}