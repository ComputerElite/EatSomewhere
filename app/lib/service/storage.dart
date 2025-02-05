import 'dart:convert';

import 'package:shared_preferences/shared_preferences.dart';

import '../data/settings.dart';
import '../data/user.dart';

class Storage {
  User? user;
  static Storage instance = Storage();
  Settings settings = Settings();

  static Future saveUser(User? user) async {
    instance.user = user;
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    await prefs.setString('user', jsonEncode(user));
  }

  static Future loadFromStorage() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();

    final String? userJson = prefs.getString('user');
    if (userJson != null) {
      instance.user = User.fromJson(jsonDecode(userJson));
    }

    final String? settingsJson = prefs.getString('settings');
    if (settingsJson != null) {
      instance.settings = Settings.fromJson(jsonDecode(settingsJson));
    }
  }

  static Settings getSettings() {
    return instance.settings;
  }

  static void savePageIndex(int index) async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    prefs.setInt("page", index);
  }

  static Future<int> getPageIndex() async {
    SharedPreferences prefs = await SharedPreferences.getInstance();
    return prefs.getInt("page") ?? -1;
  }

  static User? getUser() {
    return instance.user;
  }

  static void saveSettings() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    prefs.setString('settings', jsonEncode(instance.settings));
  }

  static void setUser(User user) {
    instance.user = user;
    saveUser(user);
  }
}