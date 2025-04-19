import 'package:eat_somewhere/data/food.dart';
import 'package:eat_somewhere/data/helper.dart';
import 'package:eat_somewhere/service/storage.dart';
import 'package:eat_somewhere/widgets/error_dialog.dart';
import 'package:flutter/material.dart';

class CreateIngredientDialog extends StatefulWidget {
  Ingredient ingredient;
  CreateIngredientDialog({Key? key, required this.ingredient})
      : super(key: key);

  @override
  State<CreateIngredientDialog> createState() => _CreateIngredientDialogState();
}

class _CreateIngredientDialogState extends State<CreateIngredientDialog> {
  TextEditingController nameController = TextEditingController();
  TextEditingController costController = TextEditingController();
  TextEditingController amountController = TextEditingController();

  @override
  void initState() {
    super.initState();
    if (widget.ingredient.id != null) {
      nameController.text = widget.ingredient.name ?? "";
      costController.text = PriceHelper.formatPriceWithoutUnit(widget.ingredient.cost);
      amountController.text = widget.ingredient.amount.toString();
    }
  }

  @override
  Widget build(BuildContext context) {
    return AlertDialog.adaptive(
      title: Text(widget.ingredient.id == null
          ? "Create Ingredient"
          : "Edit Ingredient"),
      content: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          TextField(
            controller: nameController,
            decoration: const InputDecoration(labelText: "Name"),
          ),
          Row(
            mainAxisSize: MainAxisSize.min,
            children: [
              Expanded(
                child: TextField(
                  controller: amountController,
                  decoration: const InputDecoration(labelText: "Amount"),
                  keyboardType: TextInputType.number,
                ),
              ),
              DropdownMenu(
                dropdownMenuEntries: Unit.values
                    .map((e) => DropdownMenuEntry(value: e, label: e.name))
                    .toList(),
                initialSelection: widget.ingredient.unit,
                onSelected: (value) => widget.ingredient.unit = value!,
              ),
            ],
          ),
          TextField(
            controller: costController,
            decoration: const InputDecoration(labelText: "Cost (€)"),
            keyboardType: TextInputType.number,
          ),
        ],
      ),
      actions: [
        TextButton(
          onPressed: () {
            Navigator.pop(context);
          },
          child: const Text("Cancel"),
        ),
        TextButton(
          onPressed: () async {
            widget.ingredient.name = nameController.text;
            widget.ingredient.cost =
                ((double.tryParse(costController.text) ?? 0) * 100).toInt();
            widget.ingredient.amount =
                double.tryParse(amountController.text) ?? 1;
            String? error = await Storage.updateIngredient(widget.ingredient);
            if (error != null) {
              ErrorDialog.showErrorDialog("Error", error);
              return;
            }
            Navigator.pop(context, widget.ingredient);
          },
          child: const Text("Save"),
        )
      ],
    );
  }
}
