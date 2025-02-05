import 'package:dynamic_color/dynamic_color.dart';
import 'package:eat_somewhere/service/storage.dart';
import 'package:flutter/material.dart';

import 'screens/screen_selector.dart';

void main() {
  WidgetsFlutterBinding.ensureInitialized();
  Storage.loadFromStorage();

  // from now on Storage is available everywhere
  runApp(MyApp());
}

class MyApp extends StatefulWidget {
  const MyApp({Key? key}) : super(key: key);

  @override
  State<MyApp> createState() => MyAppState();
}

class MyAppState extends State<MyApp> {
  ThemeMode themeMode = Storage.getSettings().theme;

  void setThemeMode(ThemeMode themeMode) {
    Storage.getSettings().theme = themeMode;
    Storage.saveSettings();
    setState(() {
      this.themeMode = themeMode;
    });
  }

  // This widget is the root of your application.
  @override
  Widget build(BuildContext context) {
    return DynamicColorBuilder(builder: (lightColorScheme, darkColorScheme) {
      return MaterialApp(
        title: 'EatSomewhere',
        theme: lightColorScheme != null ? ThemeData(
          useMaterial3: true,
          colorScheme: lightColorScheme,
        ): ThemeData.light(),
        darkTheme: darkColorScheme != null ? ThemeData(
          useMaterial3: true,
          colorScheme: darkColorScheme,
        ): ThemeData.dark(),
        themeMode: themeMode,
        home: ScreenSelector(),
      );
    });
  }
}