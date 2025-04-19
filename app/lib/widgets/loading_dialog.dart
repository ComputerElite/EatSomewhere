
import 'package:eat_somewhere/main.dart';
import 'package:flutter/material.dart';

class LoadingDialog extends StatelessWidget {
  final String title;
  const LoadingDialog({
    required this.title,
    super.key,
  });

  @override
  Widget build(BuildContext context) {
    return AlertDialog(
        title: Text(title),
        content: Row(
            mainAxisAlignment: MainAxisAlignment.center,
            children: [CircularProgressIndicator()]),
        actions: []);
  }

  static void show(String title) {
    showDialog(
        context: navigatorKey.currentContext!,
        builder: (BuildContext context) {
          return LoadingDialog(title: title);
        });
  }
}
