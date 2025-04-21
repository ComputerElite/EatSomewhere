import 'package:eat_somewhere/data/bill.dart';
import 'package:eat_somewhere/data/foodentry.dart';
import 'package:eat_somewhere/data/helper.dart';
import 'package:eat_somewhere/main.dart';
import 'package:eat_somewhere/screens/create_food_entry_screen.dart';
import 'package:eat_somewhere/service/storage.dart';
import 'package:eat_somewhere/widgets/chips/additional_persons_chip.dart';
import 'package:eat_somewhere/widgets/padded_card.dart';
import 'package:eat_somewhere/widgets/chips/price_chip.dart';
import 'package:eat_somewhere/widgets/chips/user_additional_person_chip.dart';
import 'package:eat_somewhere/widgets/chips/user_chip.dart';
import 'package:eat_somewhere/widgets/chips/user_price_chip.dart';
import 'package:flutter/material.dart';

class FoodEntryScreen extends StatefulWidget {
  const FoodEntryScreen({Key? key}) : super(key: key);

  @override
  State<FoodEntryScreen> createState() => _FoodEntryScreenState();

  static void CreateFoodEntry() {
    Navigator.push(
      navigatorKey.currentContext!,
      MaterialPageRoute(
        builder: (context) => CreateFoodEntryScreen(foodEntry: FoodEntry(),),
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
      body: ListView(
        children: Storage.getFoodEntriesForCurrentAssembly()
            .map<Widget>((x) => FoodEntryWidget(foodEntry: x))
            .toList(),
      ),
    );
  }
}

class FoodEntryWidget extends StatefulWidget {
  FoodEntry foodEntry;
  Function()? onTap;
  FoodEntryWidget({Key? key, required this.foodEntry, this.onTap}) : super(key: key);

  @override
  State<FoodEntryWidget> createState() => _FoodEntryWidgetState();
}

class _FoodEntryWidgetState extends State<FoodEntryWidget> {
  @override
  Widget build(BuildContext context) {
    String billSummary = "";
    List<Bill?> bill = widget.foodEntry.bills.where((x) => x!.user?.Id == Storage.getUser()?.userId).toList();
    Widget billSummaryWidget = const Text("You did not participate in this bill");
    
    if (bill.isNotEmpty) {
      billSummaryWidget = Row(
        children: [
          UserAdditionalPersonChip(additionalPersons: (bill[0]?.persons ?? 1) - 1, user: bill[0]?.user),
          const Text("->"),
          UserPriceChip(amount: bill[0]?.amount, user: bill[0]?.recipient),
        ],
      );
    }
    return PaddedCard(
      onTap: widget.onTap,
        child: Row(
      spacing: 10,
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        Column(
          crossAxisAlignment: CrossAxisAlignment.start,
          children: [
          Text(DateHelper.formatDateTime(widget.foodEntry.date)),
          Row(children: [
            UserChip(user: widget.foodEntry.payedBy),
            const Text("paid"),
            PriceChip(amount: widget.foodEntry.cost),
          ],),
          billSummaryWidget
        ]),
        Row(
          spacing: 10,
          children: [
            IconButton(
              icon: const Icon(Icons.edit),
              onPressed: () async {
                await Navigator.push(
                  context,
                  MaterialPageRoute(
                    builder: (context) => CreateFoodEntryScreen(
                      foodEntry: widget.foodEntry,
                    ),
                  ),
                );
                setState(() {
                  
                });
              })
          ],
        )
      ],
    ));
  }
}