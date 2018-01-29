using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pantalla : MonoBehaviour {
    //mensajes constantes
    const string menuHint = "You can tipe MENU at any time";

    //Atributos de clase
    //Estos arrays tienen las palabras de los distintos niveles
    string[] passwordLv1 = { "book", "page", "word", "loan"};
    string[] passwordLv2 = { "credit", "debit", "payment", "vault" };
    string[] passwordLv3 = { "planetary", "uranus", "stellar"};
    int level;
    //Enumerado de los estados del juego
    enum GameState { MainMenu, Password, Win};
    GameState currentState = GameState.MainMenu;
    //contraseña correcta
    string password;

    //Metodos
    private void ShowMainMenu() {
        currentState = GameState.MainMenu;
        Terminal.ClearScreen();
        Terminal.WriteLine("What do you want to hack today?");
        Terminal.WriteLine("1. Library's Database");
        Terminal.WriteLine("2. City's Bank");
        Terminal.WriteLine("3. NASA main sistem");
        Terminal.WriteLine("Option?");
    }

    void OnUserInput (String input) {
        //Si el usuario escribe menu, se abre el menu
        if (input == "MENU") {
            ShowMainMenu();
        } else if (input == "QUIT" || input == "CLOSE" || input == "EXIT") { //Si se escribe, quit, close o exit, se cierra la aplicacion
            //si el usuario esta en un navegador, le pedimos que cierre la ventana
            Terminal.WriteLine("Please, close the browser's tab");
            Application.Quit();
        }else if (currentState == GameState.MainMenu) { //Se checa el estado actual del juego
            RunMainMenu(input);
        }else if (currentState == GameState.Password) {
            CheckPassword(input);
        }
    }

    private void CheckPassword(string input) {
        if (password == input) {
            DisplayWinScreen();
        } else {
            AskForPassword();
        }
    }

    private void DisplayWinScreen() {
        currentState = GameState.Win;
        Terminal.ClearScreen();
        ShowLevelReward();
        Terminal.WriteLine(menuHint);
    }

    private void ShowLevelReward() {
        switch(level) {
            case 1:
                Terminal.WriteLine("Which book you will want today?");
                Terminal.WriteLine(@"
          ________        
        /_______/ |
        |       | |
        |       | |
        |       | |
        |_______|/

        ");
                break;

            case 2:
                Terminal.WriteLine("How much monew you want to retire");
                Terminal.WriteLine(@"
 ______________________
|           |          |
|            >         |
|          <           |
|___________|__________|
");
            break;

            case 3:
                Terminal.WriteLine("Welcome to the NASA servers");
                Terminal.WriteLine(@"
      /\
     /  \
     |  |
     |  |
    /    \
    |_||_|
");
            break;

            default:
                Debug.LogError("Invalid level. How did you manage that?");
            break;
        }
    }

    private void RunMainMenu(String input) {
        //Checamos que el input sea valido
        bool isValidInput =(input == "1")||(input == "2")||(input == "3") ;
        //Si es valido
        if (isValidInput) {
            //pasamos el imput a un int
            level = int.Parse(input);
            AskForPassword();
        } else { //si el usuario no ingreso un nivel valido
            if (input == "007") { //si es el codigo correcto, se activara el huevo de pascua
                Terminal.WriteLine("Please enter a valid level, Mr. Bond");
            } else { //si no, simplemente le pedimos que inserte un nivel valido
                Terminal.WriteLine("Enter a valid level");
            }
        }
    }

    private void AskForPassword() {
        //Cambiamos el estado del juego a jugando (contraseña)
        currentState = GameState.Password;
        //Limpiamos la pantalla
        Terminal.ClearScreen();
        //Llamamos al metodo encargado de elegir una contraseña aleatoria
        SetRandomPassword();
        //Mostramos en pantalla las instrucciones
        Terminal.WriteLine("Enter your Password. Hint " + password.Anagram());
        Terminal.WriteLine(menuHint);
    }

    private void SetRandomPassword() {
        switch (level) {
            case 1:
                password = passwordLv1[UnityEngine.Random.Range(0, passwordLv1.Length)];
                break;

            case 2:
                password = passwordLv2[UnityEngine.Random.Range(0, passwordLv2.Length)];
                break;

            case 3:
                password = passwordLv3[UnityEngine.Random.Range(0, passwordLv3.Length)];
                break;

            default:
                Debug.LogError("Invalid level. How did you manage that?");
                break;
        }
    }

    // Use this for initialization
    void Start () {
        ShowMainMenu();
	}


    // Update is called once per frame
    void Update () {
		
	}
}
