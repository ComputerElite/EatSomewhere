class Food {
  String? id;
  String? name;
  String? assemblyId;
  List<IngredientEntry> ingredients = [];
  List<Tag> tags = [];
  int personCount = 1;
  int estimatedCost = 0;
  bool archived = false;
  String recipe = "unknown recipe";

  Food();

  toJson() {
    return {
      "Id": id,
      "Name": name,
      "Assembly": {
        "Id": assemblyId,
      },
      "Ingredients": ingredients.map((e) => e.toJson()).toList(),
      "Tags": tags.map((e) => e.toJson()).toList(),
      "PersonCount": personCount,
      "EstimatedCost": estimatedCost,
      "Recipe": recipe,
      "Archived": archived,
    };
  }

  Food.fromJson(Map<String, dynamic> json) {
    id = json["Id"];
    name = json["Name"];
    assemblyId = json["Assembly"]["Id"];
    ingredients = (json["Ingredients"] as List<dynamic>).map((e) => IngredientEntry.fromJson(e)).toList();
    tags = (json["Tags"] as List<dynamic>).map((e) => Tag.fromJson(e)).toList();
    personCount = json["PersonCount"];
    estimatedCost = json["EstimatedCost"];
    recipe = json["Recipe"];
    archived = json["Archived"];
  }

  int getCostPerPerson() {
    return (estimatedCost / personCount).ceil();
  }

  int getEstimatedCost() {
    if(ingredients.isEmpty) return 0;
    return ingredients.map((x) => x.getEstimatedCost()).reduce((value, element) => value + element);
  }
}

class Tag {
  String? id;
  String? name;
  String? assemblyId;

  toJson() {
    return {
      "Id": id,
      "Name": name,
      "Assembly": {
        "Id": assemblyId,
      },
    };
  }

  Tag.fromJson(Map<String, dynamic> json) {
    id = json["Id"];
    name = json["Name"];
    assemblyId = json["Assembly"]["Id"];
  }
}

class IngredientEntry {
  String? id;
  Ingredient? ingredient;
  double amount = 1;
  int estimatedCost = 0;

  IngredientEntry();
  IngredientEntry.fromIngredient(Ingredient ingredient) {
    this.ingredient = ingredient;
    amount = ingredient.amount;
  }

  toJson() {
    return {
      "Id": id,
      "Ingredient": ingredient?.toJson(),
      "Amount": amount,
      "EstimatedCost": estimatedCost,
    };
  }

  IngredientEntry.fromJson(Map<String, dynamic> json) {
    id = json["Id"];
    ingredient = Ingredient.fromJson(json["Ingredient"]);
    amount = json["Amount"] is int ? (json["Amount"] as int).toDouble() : json["Amount"];
    estimatedCost = json["EstimatedCost"];
  }

  int getEstimatedCost() {
    if (ingredient == null) return 0;
    return ((ingredient!.cost * amount) / ingredient!.amount).ceil();
  }
}

enum Unit {
  gramm,
  milliliter,
  piece,
  pinch
}

class Ingredient {
  String? id;
  String? name;
  int cost = 0;
  double amount = 0;
  Unit unit = Unit.gramm;
  String? assemblyId;
  bool archived = false;

  Ingredient();

  toJson() {
    return {
      "Id": id,
      "Name": name,
      "Cost": cost,
      "Amount": amount,
      "Unit": unit.index,
      "Assembly": {
        "Id": assemblyId,
      },
      "Archived": archived,
    };
  }

  Ingredient.fromJson(Map<String, dynamic> json) {
    id = json["Id"];
    if(json["Name"] != null) name = json["Name"];
    if(json["Cost"] != null) cost = json["Cost"];
    if (json["Amount"] != null) amount = json["Amount"] is int ? (json["Amount"] as int).toDouble() : json["Amount"];
    if(json["Unit"] != null) unit = Unit.values[json["Unit"]];
    if(json["Assembly"] != null) assemblyId = json["Assembly"]["Id"];
    if(json["Archived"] != null) archived = json["Archived"];
  }
}