class Food {
  String? id;
  String? name;
  String? assemblyId;
  List<IngredientEntry> ingredients = [];
  List<Tag> tags = [];
  int personCount = 1;
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
      "Recipe": recipe,
    };
  }

  Food.fromJson(Map<String, dynamic> json) {
    id = json["Id"];
    name = json["Name"];
    assemblyId = json["Assembly"]["Id"];
    ingredients = (json["Ingredients"] as List<dynamic>).map((e) => IngredientEntry.fromJson(e)).toList();
    tags = (json["Tags"] as List<dynamic>).map((e) => Tag.fromJson(e)).toList();
    personCount = json["PersonCount"];
    recipe = json["Recipe"];
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
  Unit unit = Unit.gramm;

  toJson() {
    return {
      "Id": id,
      "Ingredient": ingredient?.toJson(),
      "Amount": amount,
      "Unit": unit.index,
    };
  }

  IngredientEntry.fromJson(Map<String, dynamic> json) {
    id = json["Id"];
    ingredient = Ingredient.fromJson(json["Ingredient"]);
    amount = json["Amount"];
    unit = Unit.values[json["Unit"]];
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
  double cost = 0;
  double amount = 0;
  Unit unit = Unit.gramm;
  String? assemblyId;

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
    };
  }

  Ingredient.fromJson(Map<String, dynamic> json) {
    id = json["Id"];
    name = json["Name"];
    cost = json["Cost"];
    amount = json["Amount"];
    unit = Unit.values[json["Unit"]];
    assemblyId = json["Assembly"]["Id"];
  }
}