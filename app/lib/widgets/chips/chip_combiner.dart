import 'package:eat_somewhere/widgets/chips/combinable_chip.dart';
import 'package:flutter/material.dart';

class ChipCombiner extends StatelessWidget {
  final List<CombinableChip> chips;

  const ChipCombiner({Key? key, required this.chips}) : super(key: key);

  @override
  Widget build(BuildContext context) {
    if(chips.length == 1) {
      return Row(
        mainAxisSize: MainAxisSize.min,
        children: [
          chips.first
        ],
      );
    }
    chips.first.shape = RoundedRectangleBorder(
        borderRadius: BorderRadiusGeometry.horizontal(left: Radius.circular(8)));
    chips.last.shape = RoundedRectangleBorder(
        borderRadius: BorderRadiusGeometry.horizontal(right: Radius.circular(8)));
    chips.skip(1).take(chips.length - 2).forEach((chip) {
      chip.shape = RoundedRectangleBorder(
          borderRadius: BorderRadiusGeometry.horizontal(left: Radius.circular(0), right: Radius.circular(0)));
    });
    return Row(
      mainAxisSize: MainAxisSize.min,
      children: chips
    );
  }
}