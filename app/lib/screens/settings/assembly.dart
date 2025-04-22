import 'package:eat_somewhere/backend_data/Backend_user.dart';
import 'package:eat_somewhere/backend_data/assembly.dart';
import 'package:eat_somewhere/data/user.dart';
import 'package:eat_somewhere/main.dart';
import 'package:eat_somewhere/service/server_loader.dart';
import 'package:eat_somewhere/service/storage.dart';
import 'package:eat_somewhere/widgets/error_dialog.dart';
import 'package:eat_somewhere/widgets/info_dialog.dart';
import 'package:eat_somewhere/widgets/loading_dialog.dart';
import 'package:eat_somewhere/widgets/padded_card.dart';
import 'package:flutter/material.dart';

class AssemblyCard extends StatefulWidget {
  final Assembly assembly;
  Function() onSelected;

  AssemblyCard({Key? key, required this.assembly, required this.onSelected})
      : super(key: key);

  @override
  State<AssemblyCard> createState() => _AssemblyCardState();
}

class _AssemblyCardState extends State<AssemblyCard> {
  void removeUser(BackendUser u) async {
    showDialog(context: context, builder: (context) => AlertDialog.adaptive(
          title: Text("Remove user"),
          content: Text("Are you sure you want to remove ${u.username} from the assembly?"),
          actions: [
            TextButton(
              onPressed: () {
                Navigator.of(context).pop();
              },
              child: const Text("Cancel"),
            ),
            TextButton(
              onPressed: () async {
                LoadingDialog.show("Removing user");
                ErrorContainer<CreatedResponse> error =
                    await ServerLoader.removeUserFromAssembly(widget.assembly.id!, u.Id!);
                Navigator.of(context).pop();
                if (error.error != null) {
                  ErrorDialog.show("Error while removing", error.error!);
                  return;
                }
                widget.assembly.users.remove(u);
                Navigator.of(context).pop();
                InfoDialog.show("Success", error.value!.data);
                Storage.reloadAssemblies();
              },
              child: const Text("Remove"),
            ),
          ],
        ));

    
  }

  void openDetails() {
    showDialog(
        context: context,
        builder: (context) => AlertDialog.adaptive(
              title: Text("Assembly details"),
              content: SingleChildScrollView(
                child: Column(
                  mainAxisSize: MainAxisSize.min,
                  crossAxisAlignment: CrossAxisAlignment.start,
                  children: [
                    Text("Name: ${widget.assembly.name}"),
                    Text("Description: ${widget.assembly.description}"),
                    Text("ID: ${widget.assembly.id}"),
                    Text("Pending Users"),
                    ...widget.assembly.pending
                        .map((e) => PaddedCard(
                                child: Row(
                              mainAxisAlignment: MainAxisAlignment.spaceBetween,
                              children: [
                                Text(e.username),
                                Row(
                                  children: [
                                    IconButton(
                                        onPressed: () {
                                          removeUser(e);
                                        },
                                        icon: Icon(Icons.delete)),
                                    IconButton(
                                        onPressed: () async {
                                          LoadingDialog.show("Adding user to assembly");
                                      ErrorContainer<CreatedResponse> error =
                                          await ServerLoader.addToAssembly(
                                              widget.assembly.id!, e.Id!);
                                      Navigator.of(context).pop();
                                      if (error.error != null) {
                                        ErrorDialog.show(
                                            "Error while adding",
                                            error.error!);
                                      } else {
                                        widget.assembly.admins.add(e);
                                        Navigator.of(context).pop();
                                        InfoDialog.show(
                                            "Success", error.value!.data);
                                      }

                                      setState(() {});
                                        },
                                        icon: Icon(Icons.add))
                                  ],
                                )
                              ],
                            )))
                        .toList(),
                    Text("Users"),
                    ...widget.assembly.users.map((e) {
                      bool isAdmin =
                          widget.assembly.admins.any((x) => x.Id == e.Id);
                      return PaddedCard(
                          child: Row(
                        mainAxisAlignment: MainAxisAlignment.spaceBetween,
                        children: [
                          Text("${e.username}${isAdmin ? " (Admin)" : ""}"),
                          Row(
                            children: [
                              if (!isAdmin)
                                FilledButton(
                                    onPressed: () async {
                                      LoadingDialog.show("Promoting to admin");
                                      ErrorContainer<CreatedResponse> error =
                                          await ServerLoader.promoteToAdmin(
                                              widget.assembly.id!, e.Id!);
                                      Navigator.of(context).pop();
                                      if (error.error != null) {
                                        ErrorDialog.show(
                                            "Error while promoting",
                                            error.error!);
                                      } else {
                                        widget.assembly.admins.add(e);
                                        InfoDialog.show(
                                            "Success", error.value!.data);
                                      }

                                      setState(() {});
                                    },
                                    child: Text("Promote to Admin")),

                              IconButton(
                                onPressed: () {
                                  removeUser(e);
                                },
                                icon: Icon(Icons.delete)),
                            ],
                          )
                        ],
                      ));
                    }).toList(),
                    Text("ToDo: Add users and join request"),
                  ],
                ),
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
            Storage.getSettings().chosenAssembly == widget.assembly.id
                ? Icon(Icons.check_circle, color: t.colorScheme.primary)
                : FilledButton(
                    onPressed: () {
                      Storage.getSettings().chosenAssembly =
                          widget.assembly.id!;
                      Storage.saveSettings();
                      widget.onSelected();
                    },
                    child: Text("Select")),
            IconButton(onPressed: openDetails, icon: Icon(Icons.edit))
          ],
        )
      ],
    ));
  }
}
