class HelloWorld {
  public static void saySomethingUsingJava(String message) {
    System.out.println(message);
  }
  public static void saySomethingUsingDotNet(String message) {
    System.Console.WriteLine(message);
  }
}
public class MyProgram {
  public static void main(String[] args) {
    HelloWorld.saySomethingUsingJava("A message via Java API's");
    HelloWorld.saySomethingUsingDotNet("A message via .NET framework API's");
  }
}