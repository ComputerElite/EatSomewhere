import 'package:eat_somewhere/backend_data/ingredient.dart';
import 'package:flutter/material.dart';

class SelectIngredientScreen extends StatefulWidget {
  const SelectIngredientScreen({Key? key}) : super(key: key);

  @override
  State<SelectIngredientScreen> createState() => _SelectIngredientScreenState();
}

class _SelectIngredientScreenState extends State<SelectIngredientScreen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Select Ingredient'),
      ),
      body: ListView(children: [
        
      ],)
    );
  }
}