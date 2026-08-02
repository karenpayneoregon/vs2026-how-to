namespace CommonLibrary.CustonExceptions;

public class MissingMainConnectionException() : Exception("The main connection string is missing in the appsettings.json file.");
