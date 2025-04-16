import 'dart:convert';

import 'package:eat_somewhere/backend_data/assembly.dart';
import 'package:eat_somewhere/backend_data/ingredient.dart';
import 'package:eat_somewhere/service/server_loader.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../data/settings.dart';
import '../data/user.dart';

class Storage {
  User? user;
  static Storage instance = Storage();
  Settings settings = Settings();
  List<Assembly> ownAssemblies = [];
  List<Ingredient> ingredients = [];

  static Future saveUser(User? user) async {
    instance.user = user;
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    await prefs.setString('user', jsonEncode(user));
  }

  static Future saveOwnAssemblies(List<Assembly> assemblies) async {
    instance.ownAssemblies = assemblies;
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    await prefs.setString('ownAssemblies', jsonEncode(assemblies));
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

    final String? ownAssembliesJson = prefs.getString('ownAssemblies');
    if (ownAssembliesJson != null) {
      instance.ownAssemblies = (jsonDecode(ownAssembliesJson) as List<dynamic>).map((e) => Assembly.fromJson(e)).toList();
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

  static Future initialStart() async {
    if (instance.user == null) {
      return;
    }
    await reloadAssemblies();
    await reloadIngredients();
  }

  static List<Assembly> getOwnAssemblies() {
    return instance.ownAssemblies;
  }

  static Future reloadAssemblies() async {
    instance.ownAssemblies = await ServerLoader.LoadAssemblies();
  }

  static Future reloadIngredients() async {
    instance.ingredients = await ServerLoader.LoadIngredients();
  }
}