import 'package:eat_somewhere/data/food.dart';
import 'package:eat_somewhere/screens/create_ingredient_dialog.dart';
import 'package:eat_somewhere/screens/ingredient_widget.dart';
import 'package:eat_somewhere/service/storage.dart';
import 'package:eat_somewhere/widgets/searchable_list.dart';
import 'package:flutter/material.dart';

class SelectIngredientScreen extends StatefulWidget {
  const SelectIngredientScreen({Key? key}) : super(key: key);

  @override
  State<SelectIngredientScreen> createState() => _SelectIngredientScreenState();
}

class _SelectIngredientScreenState extends State<SelectIngredientScreen> {
  @override
  Widget build(BuildContext context) {
    return SearchableListScreen<Ingredient>(
        title: "Select ingredient",
        items: Storage.getIngredientsForCurrentAssembly(),
        onRefresh: () async {
          await Storage.reloadIngredients();
          setState(() {});
        },
        mappingFunction: (x) => IngredientWidget(
          ingredientRemoved: () => setState(() {}),
              ingredient: x,
              onTap: () {
                Navigator.pop(context, x);
              },
            ) as Widget,
        stringFunction: (x) => x.name ?? "Unknown",
        newAction: () async {
          // Shop pop up of ingredient
          Ingredient? newIngredient = await showDialog(
              context: context,
              builder: (builder) => CreateIngredientDialog(
                    ingredient: Ingredient(),
                  ));
          if (newIngredient != null) {
            Navigator.pop(context, newIngredient);
          }
        });
  }
}
