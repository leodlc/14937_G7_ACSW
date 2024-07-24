package ec.edu.espe.bookstore;

import io.cucumber.junit.Cucumber;
import io.cucumber.junit.CucumberOptions;
import org.junit.runner.RunWith;

@RunWith(Cucumber.class)
@CucumberOptions(
    features = "src/test/resources/features",
    glue = {"ec.edu.espe.bookstore"},
    plugin = {"pretty", "html:target/cucumber-html-report", "json:cucumber.json"}
)
public class RunCucumberTest {

}