
import 'package:flutter/material.dart';

class Settings {
  ThemeMode theme = ThemeMode.system;
  String chosenAssembly = "";


  Settings();

  Settings.fromJson(Map<String, dynamic> json) {
    if(json["theme"] != null)
      theme = ThemeMode.values[json["theme"]];

    if(json["chosenAssembly"] != null)
      chosenAssembly = json["chosenAssembly"];
  }

  Map<String, dynamic> toJson() {
    return {
      "theme": theme.index,
      "chosenAssembly": chosenAssembly,
    };
  }
}