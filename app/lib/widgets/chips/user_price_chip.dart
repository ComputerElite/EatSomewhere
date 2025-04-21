import 'package:eat_somewhere/backend_data/Backend_user.dart';
import 'package:eat_somewhere/widgets/chips/chip_combiner.dart';
import 'package:eat_somewhere/widgets/chips/price_chip.dart';
import 'package:eat_somewhere/widgets/chips/user_chip.dart';
import 'package:flutter/material.dart';

class UserPriceChip extends StatelessWidget {
  final int? amount;
  final BackendUser? user;
  final Function()? onTap;

  const UserPriceChip({Key? key, required this.amount, required this.user, this.onTap})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    return ChipCombiner(
      chips: [
        UserChip(
            user: user,
            onTap: onTap),
        PriceChip(
            amount: amount,
            onTap: onTap),
      ],
    );
  }
}
