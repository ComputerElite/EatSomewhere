import 'package:eat_somewhere/backend_data/assembly.dart';
import 'package:eat_somewhere/service/storage.dart';
import 'package:eat_somewhere/widgets/padded_card.dart';
import 'package:flutter/material.dart';

class AssemblyCard extends StatefulWidget {
  final Assembly assembly;
  Function() onSelected;

  AssemblyCard({Key? key, required this.assembly, required this.onSelected}) : super(key: key);

  @override
  State<AssemblyCard> createState() => _AssemblyCardState();
}

class _AssemblyCardState extends State<AssemblyCard> {

  void openDetails() {
    showDialog(context: context, builder: (context) => AlertDialog.adaptive(
      title: Text("Assembly details"),
      content: Column(
        mainAxisSize: MainAxisSize.min,
        children: [
          Text("Name: ${widget.assembly.name}"),
          Text("Description: ${widget.assembly.description}"),
          Text("ID: ${widget.assembly.id}"),
          Text("ToDo: Add users and join request"),
        ],
      ),
      actions: [
        TextButton(
          onPressed: () {
            Navigator.of(context).pop();
          },
          child: const Text("OK"),
        ),
      ],
    ));
  }

  @override
  Widget build(BuildContext context) {
    ThemeData t = Theme.of(context);
    return PaddedCard(
        child: Row(
      mainAxisAlignment: MainAxisAlignment.spaceBetween,
      children: [
        Column(
          children: [
            Text(widget.assembly.name, style: t.textTheme.headlineSmall),
            Text(widget.assembly.description, style: t.textTheme.bodySmall),
          ],
        ),
        Row(
          children: [
            Storage.getSettings().chosenAssembly == widget.assembly.id ?
            Icon(Icons.check_circle, color: t.colorScheme.primary) :
            FilledButton(onPressed: () {
              Storage.getSettings().chosenAssembly = widget.assembly.id!;
              Storage.saveSettings();
              widget.onSelected();
            }, child: Text("Select")),
            IconButton(onPressed: openDetails, icon: Icon(Icons.edit))
            ],
        )
      ],
    ));
  }
}
