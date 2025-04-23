import 'package:eat_somewhere/data/food.dart';
import 'package:eat_somewhere/screens/create_food.dart';
import 'package:eat_somewhere/screens/create_ingredient_dialog.dart';
import 'package:eat_somewhere/screens/food_widget.dart';
import 'package:eat_somewhere/screens/ingredient_widget.dart';
import 'package:eat_somewhere/service/storage.dart';
import 'package:eat_somewhere/widgets/searchable_list.dart';
import 'package:flutter/material.dart';

class SelectFoodScreen extends StatefulWidget {
  const SelectFoodScreen({Key? key}) : super(key: key);

  @override
  State<SelectFoodScreen> createState() => _SelectFoodScreenState();
}

class _SelectFoodScreenState extends State<SelectFoodScreen> {
  @override
  Widget build(BuildContext context) {
    return SearchableListScreen<Food>(
        title: "Select Food",
        items: Storage.getFoodsForCurrentAssembly(),
        onRefresh: () async {
          await Storage.reloadFoods();
          setState(() {});
        },
        mappingFunction: (x) => FoodWidget(
              food: x,
              onTap: () => Navigator.pop(context, x),
              allowNew: false,
            ),
        stringFunction: (x) => x.name ?? "Unknown",
        newAction: () async {
          Food? newFood = await showDialog(
              context: context,
              builder: (builder) => CreateFoodScreen(
                    food: Food(),
                  ));
          if (newFood != null) {
            Navigator.pop(context, newFood);
          }
        });
  }
}
