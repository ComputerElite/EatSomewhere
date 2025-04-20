import 'package:eat_somewhere/main.dart';
import 'package:flutter/material.dart';

class FoodEntryScreen extends StatefulWidget {
  const FoodEntryScreen({Key? key}) : super(key: key);

  @override
  State<FoodEntryScreen> createState() => _FoodEntryScreenState();

  static void CreateFoodEntry() {
    Navigator.push(
      navigatorKey.currentContext!,
      MaterialPageRoute(
        builder: (context) => const FoodEntryScreen(),
      ),
    );
  }
}

class _FoodEntryScreenState extends State<FoodEntryScreen> {
  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: const Text('Food Entry'),
      ),
      body: Center(
        child: const Text('Food Entry Screen'),
      ),
    );
  }
}