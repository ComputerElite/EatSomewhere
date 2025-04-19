import 'package:eat_somewhere/data/food.dart';
import 'package:eat_somewhere/data/helper.dart';
import 'package:eat_somewhere/screens/select_ingredient.dart';
import 'package:eat_somewhere/service/storage.dart';
import 'package:eat_somewhere/widgets/error_dialog.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';

class CreateFoodScreen extends StatefulWidget {
  Food food;
  CreateFoodScreen({Key? key, required this.food}) : super(key: key);

  @override
  State<CreateFoodScreen> createState() => _CreateFoodScreenState();
}

class _CreateFoodScreenState extends State<CreateFoodScreen> {
  TextEditingController nameController = TextEditingController();
  TextEditingController recipeController = TextEditingController();
  TextEditingController personCountController = TextEditingController();

  @override
  void initState() {
    super.initState();
    nameController.text = widget.food.name ?? "";
    recipeController.text = widget.food.recipe ?? "";
    personCountController.text = widget.food.personCount.toString();
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
        appBar: AppBar(
          title: const Text('Create Food'),
        ),
        body: ListView(
          
          children: [
            TextField(
              decoration: InputDecoration(
                labelText: "Name",
                hintText: "Enter food name",
              ),
              controller: nameController,
              onChanged: (value) {
                widget.food.name = value;
              },
            ),
            TextField(
              decoration: InputDecoration(
                labelText: "Person count",
                hintText: "Enter person count for food",
              ),
              controller: personCountController,
              onChanged: (value) {
                widget.food.personCount = int.tryParse(value) ?? 1;
                personCountController.text = widget.food.personCount.toString();
              },
            ),
            Text("Ingredients"),
            Table(
              columnWidths: {
                0: IntrinsicColumnWidth(),
                1: IntrinsicColumnWidth(),
                2: IntrinsicColumnWidth(),
                3: IntrinsicColumnWidth(),
              },
              defaultVerticalAlignment: TableCellVerticalAlignment.bottom,
              children: widget.food.ingredients
                  .map((ingredient) => TableRow(
                        children: [
                          Expanded(
                            child: TextField(
                              decoration: InputDecoration(labelText: "Amount"),
                              keyboardType: TextInputType.number,

                              onChanged: (value) {
                                ingredient.amount = double.tryParse(
                                        value.replaceAll(",", ".")) ??
                                    1;
                              },
                              onEditingComplete: () {
                                setState(() {
                                  
                                });
                              },
                              controller: TextEditingController(
                                text: ingredient.amount.toString(),
                              ),
                              inputFormatters: <TextInputFormatter>[
                                FilteringTextInputFormatter.allow(RegExp(
                                    r'^[0-9]*((.|,)[0-9]*)?$')) // only allows numbers
                              ], // Only numbers can be entered
                            ),
                          ),
                          Column(
                            crossAxisAlignment: CrossAxisAlignment.start,
                            children: [
                              Text(
                            ingredient.ingredient?.unit.name ?? "Unknown",
                            style: TextStyle(fontSize: 16),
                          ),
                          Text(
                            "~ ${PriceHelper.formatPriceWithUnit(ingredient.getEstimatedCost())}",
                            style: TextStyle(fontSize: 16),
                          ),
                            ],
                          ),
                          Column(
                            children: [
                              Text(
                                ingredient.ingredient?.name ?? "Unknown",
                                style: TextStyle(fontSize: 16),
                              ),
                              Text(
                                "~ ${PriceHelper.formatPriceWithUnit(ingredient.ingredient?.cost)} per ${ingredient.ingredient?.amount} ${ingredient.ingredient?.unit.name}",
                              ),
                            ],
                          ),
                          IconButton(
                            onPressed: () {
                              setState(() {
                                widget.food.ingredients.remove(ingredient);
                              });
                            },
                            icon: Icon(Icons.delete),
                          ),
                        ],
                      ))
                  .toList(),
            ),
            IconButton(
                onPressed: () async {
                  Ingredient? chosenIngredient = await Navigator.push(
                    context,
                    MaterialPageRoute(
                      builder: (context) => SelectIngredientScreen(),
                    ),
                  );
                  if (chosenIngredient == null) {
                    return;
                  }
                  IngredientEntry newIngredient =
                      IngredientEntry.fromIngredient(chosenIngredient);
                  widget.food.ingredients.add(newIngredient);
                  setState(() {});
                },
                icon: Icon(Icons.add)),
                Text("Total cost: ~ ${PriceHelper.formatPriceWithUnit(widget.food.ingredients.map((x) => x.getEstimatedCost()).reduce((value, element) => value + element))}"),
            TextField(
              decoration: InputDecoration(
                labelText: "Recipe",
                hintText: "Enter food recipe",
              ),
              controller: recipeController,
              maxLines: null,
              onChanged: (value) {
                widget.food.recipe = value;
              },
            ),
            FilledButton(
                onPressed: () async {
                  String? error = await Storage.updateFood(widget.food);
                  if (error != null) {
                    ErrorDialog.show("Error", error);
                    return;
                  }
                  Navigator.pop(context, widget.food);
                },
                child: Text("Save"))
          ],
        ));
  }
}
