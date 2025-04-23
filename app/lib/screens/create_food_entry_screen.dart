import 'package:eat_somewhere/backend_data/Backend_user.dart';
import 'package:eat_somewhere/data/food.dart';
import 'package:eat_somewhere/data/foodentry.dart';
import 'package:eat_somewhere/data/helper.dart';
import 'package:eat_somewhere/screens/select_food.dart';
import 'package:eat_somewhere/screens/shopping_list_screen.dart';
import 'package:eat_somewhere/service/storage.dart';
import 'package:eat_somewhere/widgets/chips/additional_persons_chip.dart';
import 'package:eat_somewhere/widgets/chips/chip_combiner.dart';
import 'package:eat_somewhere/widgets/chips/user_chip.dart';
import 'package:eat_somewhere/widgets/constrained_container.dart';
import 'package:eat_somewhere/widgets/error_dialog.dart';
import 'package:eat_somewhere/widgets/chips/price_chip.dart';
import 'package:eat_somewhere/widgets/chips/user_additional_person_chip.dart';
import 'package:eat_somewhere/widgets/heading.dart';
import 'package:eat_somewhere/widgets/loading_dialog.dart';
import 'package:flutter/material.dart';

class CreateFoodEntryScreen extends StatefulWidget {
  FoodEntry foodEntry;
  CreateFoodEntryScreen({Key? key, required this.foodEntry}) : super(key: key);

  @override
  State<CreateFoodEntryScreen> createState() => _CreateFoodEntryScreenState();
}

class _CreateFoodEntryScreenState extends State<CreateFoodEntryScreen> {
  TextEditingController commentController = TextEditingController();
  TextEditingController costController = TextEditingController();

  @override
  void initState() {
    super.initState();
    commentController.text = widget.foodEntry.comment ?? "";
    costController.text =
        PriceHelper.formatPriceWithoutUnit(widget.foodEntry.cost);
  }

  Future<BackendUser?> selectUserDialog(List<String?> disallowList) async {
    return await showDialog<BackendUser>(
      context: context,
      builder: (BuildContext context) {
        return AlertDialog(
          title: const Text('Select User'),
          content: SingleChildScrollView(
            child: Column(
              children: [
                const Text('Select a user from the list'),
                ...Storage.getUsersForCurrentAssembly()
                    .where((x) => !disallowList.contains(x.Id))
                    .map<Widget>((x) => ListTile(
                          title: Text(x.username),
                          onTap: () {
                            Navigator.pop(context, x);
                          },
                        ))
                    .toList(),
              ],
            ),
          ),
          actions: <Widget>[
            TextButton(
              child: const Text('Cancel'),
              onPressed: () {
                Navigator.of(context).pop();
              },
            ),
          ],
        );
      },
    );
  }

  @override
  Widget build(BuildContext context) {
    return Scaffold(
      appBar: AppBar(
        title: Text(
            "${widget.foodEntry.id == null ? "Create" : "Edit"} Food Entry"),
      ),
      body: ConstrainedContainer(
        child: Column(
          spacing: 5,
          crossAxisAlignment: CrossAxisAlignment.start,
          children: <Widget>[
            Table(
              defaultVerticalAlignment: TableCellVerticalAlignment.middle,
              columnWidths: {
                0: IntrinsicColumnWidth(),
                1: IntrinsicColumnWidth(),
              },
              children: [
                TableRow(
                  children: [
                    Text("Food:"),
                    FilledButton(
                        onPressed: () async {
                          Food? selectedFood = await Navigator.push(
                            context,
                            MaterialPageRoute(
                              builder: (context) => SelectFoodScreen(),
                            ),
                          );
                          if (selectedFood != null) {
                            widget.foodEntry.food = selectedFood;
                            setState(() {});
                          }
                        },
                        child: Text(
                            widget.foodEntry.food?.name ?? "Not selected")),
                  ],
                ),
                TableRow(
                  children: [
                    Text("Payed by:"),
                    FilledButton(
                        onPressed: () async {
                          widget.foodEntry.payedBy = await selectUserDialog([]);
                          setState(() {});
                        },
                        child: Text(widget.foodEntry.payedBy?.username ??
                            "Not selected"))
                  ],
                ),
              ],
            ),
            Text(
                "Estimated cost (based on recipe and person count): ${PriceHelper.formatPriceWithUnit(widget.foodEntry.getEstimatedCost())}"),
            TextField(
              controller: costController,
              decoration: const InputDecoration(labelText: "Actual Cost (€)"),
              keyboardType: TextInputType.number,
              onChanged: (value) {
                setState(() {
                  widget.foodEntry.cost =
                      ((double.tryParse(value) ?? 0) * 100).toInt();
                });
              },
            ),
            Heading(text: "Participants"),
            Divider(),
            Table(
              children: [
                ...widget.foodEntry.participants
                    .map((participant) => TableRow(children: [
                          ChipCombiner(chips: [
                            UserChip(user: participant.user),
                            AdditionalPersonsChip(
                              additionalPersons: participant.additionalPersons,
                              user: participant.user,
                            ),
                            PriceChip(
                                amount: widget.foodEntry.getCostPerPerson() *
                                    participant.getPersonAmount()),
                          ]),
                          Row(
                            children: [
                              IconButton(
                                  onPressed: () {
                                    setState(() {
                                      participant.additionalPersons--;
                                      if (participant.additionalPersons < 0) {
                                        participant.additionalPersons = 0;
                                      }
                                    });
                                  },
                                  icon: const Icon(Icons.remove)),
                              IconButton(
                                  onPressed: () {
                                    setState(() {
                                      participant.additionalPersons++;
                                    });
                                  },
                                  icon: const Icon(Icons.add)),
                            ],
                          ),
                          IconButton(
                            icon: const Icon(Icons.delete),
                            onPressed: () {
                              setState(() {
                                widget.foodEntry.participants
                                    .remove(participant);
                              });
                            },
                          ),
                        ]))
              ],
            ),
            Text(
                "Total: ${widget.foodEntry.getPersonCount()} persons @ ${PriceHelper.formatPriceWithUnit(widget.foodEntry.getCostPerPerson())} each"),
            if (widget.foodEntry.participants.length <
                Storage.getUsersForCurrentAssembly().length)
              FilledButton(
                  onPressed: () async {
                    BackendUser? selectedUser = await selectUserDialog(widget
                        .foodEntry.participants
                        .map((x) => x.user?.Id)
                        .toList());
                    if (selectedUser != null) {
                      setState(() {
                        widget.foodEntry.participants
                            .add(FoodParticipant.fromUser(selectedUser!));
                      });
                    }
                  },
                  child: const Text("Add Participant")),
            FilledButton(onPressed: () {
              Navigator.push(
                context,
                MaterialPageRoute(
                  builder: (context) => ShoppingListScreen(
                    foodEntry: widget.foodEntry
                  ),
                ),
              );
            }, child: Text("Show shopping list")),
            TextField(
              decoration: InputDecoration(
                labelText: "Comment",
                hintText: "Enter comment",
              ),
              controller: commentController,
              maxLines: null,
              onChanged: (value) {
                widget.foodEntry.comment = value;
              },
            ),
            FilledButton(
                onPressed: () async {
                  LoadingDialog.show("Saving food entry...");
                  String? error =
                      await Storage.updateFoodEntry(widget.foodEntry);
                  Navigator.pop(context);
                  if (error != null) {
                    ErrorDialog.show("Error", error);
                    return;
                  }
                  Navigator.pop(context, widget.foodEntry);
                },
                child: Text("Save"))
          ],
        ),
      ),
    );
  }
}
