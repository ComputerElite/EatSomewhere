
import 'package:flutter/material.dart';

class Settings {
  ThemeMode theme = ThemeMode.system;


  Settings();

  Settings.fromJson(Map<String, dynamic> json) {
    if(json["theme"] != null)
      theme = ThemeMode.values[json["theme"]];
  }

  Map<String, dynamic> toJson() {
    return {
      "theme": theme.index,
    };
  }
}