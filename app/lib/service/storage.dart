import 'dart:convert';

import 'package:eat_somewhere/backend_data/assembly.dart';
import 'package:eat_somewhere/data/food.dart';
import 'package:eat_somewhere/service/server_loader.dart';
import 'package:flutter/gestures.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../data/settings.dart';
import '../data/user.dart';

class Storage {
  User? user;
  static Storage instance = Storage();
  Settings settings = Settings();
  List<Assembly> ownAssemblies = [];
  List<Ingredient> ingredients = [];
  List<Food> foods = [];

  static Function() onDataReload = () {};

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
      instance.ownAssemblies = (jsonDecode(ownAssembliesJson) as List<dynamic>)
          .map((e) => Assembly.fromJson(e))
          .toList();
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
    await reloadFoods();
    await reloadIngredients();
    
  }

  static List<Assembly> getOwnAssemblies() {
    return instance.ownAssemblies;
  }

  static Future reloadAssemblies() async {
    instance.ownAssemblies = await ServerLoader.LoadAssemblies();
    onDataReload();
  }

  static Future reloadIngredients() async {
    instance.ingredients = await ServerLoader.LoadIngredients();
    onDataReload();
  }

  static Future reloadFoods() async {
    instance.foods = await ServerLoader.LoadFoods();
    onDataReload();
  }

  static getIngredientsForCurrentAssembly() {
    return instance.ingredients
        .where((element) => element.assemblyId == getSettings().chosenAssembly)
        .toList();
  }
  static getFoodsForCurrentAssembly() {
    return instance.foods
        .where((element) => element.assemblyId == getSettings().chosenAssembly)
        .toList();
  }

  static Future<String?> updateIngredient(Ingredient ingredient) async {
    // If ingredient is new, add it to the list, else update it. with server request
    ingredient.assemblyId = getSettings().chosenAssembly;

    ErrorContainer<CreatedResponse> serverResponse =
        await ServerLoader.postIngredient(ingredient);
    if (serverResponse.error != null) {
      return serverResponse.error;
    }
    Storage.instance.ingredients.removeWhere((x) => x.id == ingredient.id);
    Storage.instance.ingredients.add(Ingredient.fromJson(serverResponse.value!.data!));
    Storage.saveIngredients();
    return null;
  }

  static void saveIngredients() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    prefs.setString('ingredients',
        jsonEncode(instance.ingredients.map((x) => x.toJson()).toList()));
  }

  static Future<String?> updateFood(Food food) async {
    // If ingredient is new, add it to the list, else update it. with server request
    food.assemblyId = getSettings().chosenAssembly;

    ErrorContainer<CreatedResponse> serverResponse =
        await ServerLoader.postFood(food);
    if (serverResponse.error != null) {
      return serverResponse.error;
    }
    // The old food was archived, therefore we need to remove it
    Storage.instance.foods.removeWhere((x) => x.id == food.id);
    Storage.instance.foods.add(Food.fromJson(serverResponse.value!.data!));
    Storage.saveFoods();
    return null;
  }

  static void saveFoods() async {
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    prefs.setString(
        'foods', jsonEncode(instance.foods.map((x) => x.toJson()).toList()));
  }
}
