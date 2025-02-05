import 'package:eat_somewhere/backend_data/backend_user.dart';
import 'package:eat_somewhere/service/LoginManager.dart';
import 'package:eat_somewhere/widgets/loading_dialog.dart';
import 'package:eat_somewhere/widgets/user_widget.dart';
import 'package:flutter/material.dart';

import '../data/user.dart';
import '../main.dart';
import '../service/storage.dart';
import '../widgets/constrained_container.dart';
import '../widgets/padded_card.dart';

class SettingsScreen extends StatefulWidget {
  const SettingsScreen({Key? key}) : super(key: key);

  @override
  State<SettingsScreen> createState() => _SettingsScreenState();
}

class _SettingsScreenState extends State<SettingsScreen> {
  void startLogin(BuildContext context, bool registerPrompt) {
    TextEditingController serverController = TextEditingController();
    TextEditingController usernameController = TextEditingController();
    TextEditingController passwordController = TextEditingController();
    
    showDialog(context: context, builder: (context) => AlertDialog(
      title: Text(registerPrompt ? "Register" : "Log in"),
      content: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          TextField(
            controller: serverController,
            decoration: InputDecoration(
              labelText: "Server",
              hintText: "https://example.com",
            ),
          ),
          AutofillGroup(child: Column(
            mainAxisSize: MainAxisSize.min,
            children: [
              TextField(
                controller: usernameController,
                decoration: InputDecoration(
                  labelText: "Username",
                  hintText: "username"
                ),
                autofillHints: [AutofillHints.username],
              ),
              TextField(
                controller: passwordController,
                decoration: InputDecoration(
                  labelText: "Password",
                ),
                autofillHints: [AutofillHints.password],
                obscureText: true,
              ),],
          ))
        ],
      ),
      actions: [
        TextButton(
          onPressed: () {
            Navigator.of(context).pop();
          },
          child: Text("Close"),
        ),
        TextButton(onPressed: () async {
          LoadingDialog.show(context, registerPrompt ? "Registering" : "Logging in");
          String username = usernameController.text;
          String password = passwordController.text;
          String server = serverController.text;

          LoginManager l = LoginManager(server: server, username: username, password: password);

          LoginResult response = registerPrompt ? await l.register() : await l.login();

          if(response.error != null) {
            showDialog(context: context, builder: (context) => AlertDialog(
              title: Text("Error"),
              content: Text(response.error!),
              actions: [
                TextButton(onPressed: () {
                  Navigator.of(context).pop();
                }, child: Text("Close")),
              ],
            ));
            return;
          }

        }, child: Text(registerPrompt ? "Register" : "Log in")),
      ],
    ));
  }

  @override
  Widget build(BuildContext context) {
    ThemeData t = Theme.of(context);
    return ConstrainedContainer(
        child: ListView(
      children: <Widget>[
        Column(
          spacing: 10,
          children: [
            Text(
              'Settings',
              style: t.textTheme.headlineMedium,
            ),
            (Storage.getUser() == null) ?
              PaddedCard(
                child: Text(
                    "You are not logged in, please log in to access your journal",
                    style: t.textTheme.headlineSmall),
              )
            :
            UserWidget(user: Storage.getUser()!),
          ],
        ),
        Row(
          spacing: 10,
          mainAxisAlignment: MainAxisAlignment.center,
          children: [
          FilledButton(
            onPressed: () async {
              startLogin(context, false);
            },
            child: Text("Log in",
                style: TextStyle(fontSize: t.textTheme.titleMedium!.fontSize)),
          ),
          FilledButton(
            onPressed: () async {
              startLogin(context, true);
            },
            child: Text("Register",
                style: TextStyle(fontSize: t.textTheme.titleMedium!.fontSize)),
          ),
        ],),
        Padding(padding: EdgeInsets.all(5)),
        Row(
          mainAxisAlignment: MainAxisAlignment.spaceBetween,
          children: [
            Text("Theme"),
            SegmentedButton<int>(
              segments: [
                ButtonSegment(value: 0, label: Icon(Icons.devices)),
                ButtonSegment(value: 1, label: Icon(Icons.sunny)),
                ButtonSegment(value: 2, label: Icon(Icons.nightlight)),
              ],
              selected: {
                switch (
                    context.findAncestorStateOfType<MyAppState>()?.themeMode) {
                  null => throw UnimplementedError(), // should never be null ig
                  ThemeMode.system => 0,
                  ThemeMode.light => 1,
                  ThemeMode.dark => 2,
                }
              },
              onSelectionChanged: (Set<int> newSelection) {
                if (newSelection.isNotEmpty) {
                  switch (newSelection.first) {
                    case 0:
                      context
                          .findAncestorStateOfType<MyAppState>()
                          ?.setThemeMode(ThemeMode.system);
                      break;
                    case 1:
                      context
                          .findAncestorStateOfType<MyAppState>()
                          ?.setThemeMode(ThemeMode.light);
                      break;
                    case 2:
                      context
                          .findAncestorStateOfType<MyAppState>()
                          ?.setThemeMode(ThemeMode.dark);
                      break;
                  }
                }
              },
            )
          ],
        ),
      ],
    ));
  }
}
