import 'package:eat_somewhere/data/food.dart';
import 'package:eat_somewhere/main.dart';
import 'package:eat_somewhere/screens/create_food.dart';
import 'package:flutter/material.dart';

class FoodScreen extends StatefulWidget {
  const FoodScreen({Key? key}) : super(key: key);

  @override
  State<FoodScreen> createState() => _FoodScreenState();

  static void CreateFood() {
    Navigator.push(
      navigatorKey.currentContext!,
      MaterialPageRoute(
        builder: (context) => CreateFoodScreen(food: Food(),),
      ),
    );
  }
}

class _FoodScreenState extends State<FoodScreen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Food Screen'),
      ),
      body: Center(
        child: Text('This is the food screen'),
      ),
    );
  }
}