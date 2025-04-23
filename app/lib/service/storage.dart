import 'dart:convert';

import 'package:eat_somewhere/backend_data/Backend_user.dart';
import 'package:eat_somewhere/backend_data/assembly.dart';
import 'package:eat_somewhere/data/bill.dart';
import 'package:eat_somewhere/data/food.dart';
import 'package:eat_somewhere/data/foodentry.dart';
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
  Map<String, List<Ingredient>> ingredients = {};
  Map<String, List<Food>> foods = {};
  Map<String, List<FoodEntry>> foodEntries = {};
  Map<String, List<Bill>> bills = {};

  static Function() onDataReload = () {};

  static Future saveUser(User? user) async {
    instance.user = user;
    final SharedPreferences prefs = await SharedPreferences.getInstance();
    await prefs.setString('user', jsonEncode(user));
    initialStart();
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
    await reloadFoodEntries();
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
    instance.ingredients[getSettings().chosenAssembly] = await ServerLoader.LoadIngredients();
    onDataReload();
  }

  static Future reloadFoods() async {
    instance.foods[getSettings().chosenAssembly] = await ServerLoader.LoadFoods();
    onDataReload();
  }
  static Future reloadFoodEntries() async {
    instance.foodEntries[getSettings().chosenAssembly] = await ServerLoader.LoadFoodEntries(0, 20);
    instance.foodEntries[getSettings().chosenAssembly]!.sort((x,y) => y.date.compareTo(x.date));
    onDataReload();
  }

  static Future reloadBills() async {
    instance.bills[getSettings().chosenAssembly] = await ServerLoader.LoadBills(0, 20);
    onDataReload();
  }

  static Future loadMoreFoodEntries() async {
    for(FoodEntry e in await ServerLoader.LoadFoodEntries((instance.foodEntries[getSettings().chosenAssembly] ?? []).where((x) => x.assemblyId == getSettings().chosenAssembly).length, 20)) {
      // replace the old food entry with the new one but
      instance.foodEntries[getSettings().chosenAssembly]?.removeWhere((x) => x.id == e.id);
      instance.foodEntries[getSettings().chosenAssembly]?.add(e);
    }
    instance.foodEntries[getSettings().chosenAssembly]?.sort((x,y) => y.date.compareTo(x.date));
    onDataReload();
  }

  static List<Ingredient>? getIngredientsForCurrentAssembly() {
    return (instance.ingredients[getSettings().chosenAssembly] ?? null);
  }
  static List<Food>? getFoodsForCurrentAssembly() {
    return instance.foods[getSettings().chosenAssembly];
  }

  static List<FoodEntry>? getFoodEntriesForCurrentAssembly() {
    return instance.foodEntries[getSettings().chosenAssembly];
  }
  static List<Bill>? getBillsForCurrentAssembly() {
    return instance.bills[getSettings().chosenAssembly];
  }

  static Future<String?> updateIngredient(Ingredient ingredient) async {
    // If ingredient is new, add it to the list, else update it. with server request
    ingredient.assemblyId = getSettings().chosenAssembly;

    ErrorContainer<CreatedResponse> serverResponse =
        await ServerLoader.postIngredient(ingredient);
    if (serverResponse.error != null) {
      return serverResponse.error;
    }
    ingredient.id = serverResponse.value!.data!["Id"];
    Storage.instance.ingredients[getSettings().chosenAssembly]?.removeWhere((x) => x.id == ingredient.id);
    Storage.instance.ingredients[getSettings().chosenAssembly]?.add(Ingredient.fromJson(serverResponse.value!.data!));
    return null;
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
    food.id = serverResponse.value!.data!["Id"];
    Storage.instance.foods[getSettings().chosenAssembly]?.removeWhere((x) => x.id == food.id);
    Storage.instance.foods[getSettings().chosenAssembly]?.add(Food.fromJson(serverResponse.value!.data!));
    return null;
  }


  static Future<String?> updateFoodEntry(FoodEntry foodEntry) async {
    // If ingredient is new, add it to the list, else update it. with server request
    foodEntry.assemblyId = getSettings().chosenAssembly;
    print(json.encode(foodEntry.toJson()));

    ErrorContainer<CreatedResponse> serverResponse =
        await ServerLoader.postFoodEntry(foodEntry);
    if (serverResponse.error != null) {
      return serverResponse.error;
    }
    // The old food was archived, therefore we need to remove it
    Storage.instance.foodEntries[getSettings().chosenAssembly]?.removeWhere((x) => x.id == foodEntry.id);
    Storage.instance.foodEntries[getSettings().chosenAssembly]?.add(FoodEntry.fromJson(serverResponse.value!.data!));
    return null;
  }

  static List<BackendUser> getUsersForCurrentAssembly() {
    return instance.ownAssemblies
        .firstWhere((element) => element.id == getSettings().chosenAssembly)
        .users;
  }

  static Future<String?> deleteFood(Food food) async {

    ErrorContainer<bool?> serverResponse =
        await ServerLoader.deleteFood(food);
    if (serverResponse.error != null) {
      return serverResponse.error;
    }
    // The old food was archived, therefore we need to remove it
    Storage.instance.foods[getSettings().chosenAssembly]?.removeWhere((x) => x.id == food.id);
    return null;
  }
}
