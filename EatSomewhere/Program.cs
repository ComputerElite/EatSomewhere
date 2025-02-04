using ComputerUtils.Logging;
using EatSomewhere;
using EatSomewhere.Server;

Config.LoadConfig();
Config.SaveConfig();
Logger.displayLogInConsole = true;
Webserver s = new();
s.SetupRoutesAndStartServer();