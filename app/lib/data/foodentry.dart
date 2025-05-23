import 'package:eat_somewhere/backend_data/Backend_user.dart';
import 'package:eat_somewhere/data/bill.dart';
import 'package:eat_somewhere/data/food.dart';

class FoodEntry {
  String? id;
  DateTime date = DateTime.now();
  String? comment;
  Food? food;
  int estimatedCost = 0;
  int cost = 0;
  int costPerPerson = 0;
  int personCount = 1;
  List<FoodParticipant> participants = [];
  List<Bill?> bills = [];
  BackendUser? payedBy;
  String? assemblyId;
  bool inProgress = false;

  int getPersonCount() {
    return participants.fold(0, (sum, participant) => sum + participant.getPersonAmount());
  }

  int getEstimatedCost() {
    if (food == null) return 0;
    return food!.getCostPerPerson() * getPersonCount();
  }

  FoodEntry.fromJson(Map<String, dynamic> json) {

    id = json["Id"];
    assemblyId = json["Assembly"]["Id"];
    date = DateTime.parse(json["Date"]);
    comment = json["Comment"];
    food = Food.fromJson(json["Food"]);
    inProgress = json["InProgress"] ?? false;
    cost = json["Cost"];
    costPerPerson = json["CostPerPerson"];
    personCount = json["PersonCount"];
    bills = (json["Bills"] as List<dynamic>)
        .map((e) => Bill.fromJson(e))
        .toList();
    if(bills == null) {
      bills = [];
    }
    participants = (json["Participants"] as List<dynamic>)
        .map((e) => FoodParticipant.fromJson(e))
        .toList();
    if(json["PayedBy"] != null) payedBy = BackendUser.fromJson(json["PayedBy"]);
  }

  FoodEntry();

  toJson() {
    inProgress = cost <= 0;
    return {
      "Assembly": {
        "Id": assemblyId,
      },
      "Id": id,
      "InProgress": inProgress,
      "Date": date.toIso8601String(),
      "Comment": comment,
      "Food": food?.toJson(),
      "Cost": cost,
      "Participants": participants.map((e) => e.toJson()).toList(),
      "PayedBy": payedBy?.toJson(),
    };
  }

  int getCostPerPerson() {
    if (cost == 0) {
      return 0;
    }
    if(getPersonCount() == 0) return 0;
    return (cost / getPersonCount()).ceil();
  }

  List<IngredientEntry> getShoppingList() {
    List<IngredientEntry> shoppingList = [];
    int persons = getPersonCount();
    if(food == null) return [];
    for (IngredientEntry ingredient in food!.ingredients) {
      print("Ingredient: ${ingredient.ingredient}");
      if(ingredient.ingredient == null) continue;
      IngredientEntry shoppingItem = IngredientEntry.fromIngredient(ingredient.ingredient!);
      shoppingItem.amount = ingredient.amount / (food?.personCount ?? 1) * persons;
      shoppingList.add(shoppingItem);
    }
    return shoppingList;
  }

  FoodEntry.fromFood(Food this.food);
}

class FoodParticipant {
  String? id;
  BackendUser? user;
  int additionalPersons = 0;

  int getPersonAmount() {
    return additionalPersons + 1;
  }

  FoodParticipant.fromJson(Map<String, dynamic> json) {
    id = json["Id"];
    user = BackendUser.fromJson(json["User"]);
    additionalPersons = json["AdditionalPersons"];
  }

  toJson() {
    return {
      "Id": id,
      "User": user?.toJson(),
      "AdditionalPersons": additionalPersons,
    };
  }

  FoodParticipant.fromUser(BackendUser backendUser) {
    user = backendUser;
    additionalPersons = 0;
  }
}