import 'package:eat_somewhere/backend_data/Backend_user.dart';
import 'package:eat_somewhere/widgets/chips/combinable_chip.dart';
import 'package:eat_somewhere/widgets/chips/user_chip.dart';
import 'package:flutter/material.dart';

class AdditionalPersonsChip extends CombinableChip {
  final int additionalPersons;
  final BackendUser? user;
  final Function()? onTap;

  AdditionalPersonsChip(
      {Key? key, required this.additionalPersons, this.user, this.onTap, shape})
      : super(key: key, shape: shape);

  @override
  Widget build(BuildContext context) {
    return Chip(
      label: SizedBox(
        width: 55,
        child: Row(
        spacing: 6,
        mainAxisAlignment: MainAxisAlignment.spaceBetween,
        children: [
          Text(
            "+$additionalPersons",
            style: TextStyle(color: UserColors.getContrastColor(user)),
          ),
          Icon(Icons.group, color: UserColors.getContrastColor(user),)
        ],
      ),
      ),
      shape: this.shape,
      backgroundColor: UserColors.getColor(user),
      padding: const EdgeInsets.all(0),
    );
  }
}
