import 'package:flutter/material.dart';

class PaddedCard extends StatelessWidget {
  final Widget child;
  final EdgeInsets padding;
  Function()? onTap;

  PaddedCard(
      {Key? key,
      required this.child,
      this.padding = const EdgeInsets.all(10),
      this.onTap})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return GestureDetector(
        onTap: onTap,
        child: Card(
          child: Padding(padding: padding, child: child),
        ));
  }
}
