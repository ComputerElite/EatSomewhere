import 'package:flutter/material.dart';

class PaddedCard extends StatelessWidget {
  final Widget child;
  final EdgeInsets padding;

  const PaddedCard({Key? key, required this.child, this.padding = const EdgeInsets.all(10)}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    return Padding(
      padding: padding,
      child: Card(
        child: child,
      ),
    );
  }
}