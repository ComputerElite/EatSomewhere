import 'package:eat_somewhere/data/food.dart';
import 'package:eat_somewhere/screens/ingredient_entry.dart';
import 'package:flutter/material.dart';

class CreateFoodScreen extends StatefulWidget {
  Food food;
  CreateFoodScreen({Key? key, required this.food}) : super(key: key);

  @override
  State<CreateFoodScreen> createState() => _CreateFoodScreenState();
}

class _CreateFoodScreenState extends State<CreateFoodScreen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Create Food'),
      ),
      body: ListView(children: [
        TextField(
          decoration: InputDecoration(
            labelText: "Name",
            hintText: "Enter food name",
          ),
          onChanged: (value) {
            widget.food.name = value;
          },
        ),
        Text("Ingredients"),
        ...widget.food.ingredients
            .map((ingredient) => IngredientEntryWidget(ingredient: ingredient)),
            IconButton(onPressed: () {
              // ToDo: Show dialog to select ingredient
              // and add it to the list
              Ingredient? chosenIngredient;
              
            }, icon: Icon(Icons.add)),
        TextField(
          decoration: InputDecoration(
            labelText: "Recipe",
            hintText: "Enter food recipe",
          ),
          maxLines: null,
          onChanged: (value) {
            widget.food.recipe = value;
          },
        ),
      ],)
    );
  }
}