import 'package:eat_somewhere/backend_data/Backend_user.dart';
import 'package:eat_somewhere/widgets/chips/additional_persons_chip.dart';
import 'package:eat_somewhere/widgets/chips/chip_combiner.dart';
import 'package:eat_somewhere/widgets/chips/combinable_chip.dart';
import 'package:eat_somewhere/widgets/chips/price_chip.dart';
import 'package:eat_somewhere/widgets/chips/user_chip.dart';
import 'package:flutter/material.dart';

class UserAdditionalPersonChip extends StatelessWidget {
  final int additionalPersons;
  final BackendUser? user;
  final Function()? onTap;

  const UserAdditionalPersonChip({Key? key, required this.additionalPersons, required this.user, this.onTap})
      : super(key: key);

  @override
  Widget build(BuildContext context) {
    if (additionalPersons <= 0) {
      return UserChip(
          user: user,
          onTap: onTap);
    }
    return ChipCombiner(
      chips: [
        UserChip(
            user: user,
            onTap: onTap,
            shape: RoundedRectangleBorder(
                borderRadius:
                    BorderRadiusGeometry.horizontal(left: Radius.circular(8)))),
        AdditionalPersonsChip(
            additionalPersons: additionalPersons,
            onTap: onTap,
            user: user,
            shape: RoundedRectangleBorder(
                borderRadius:
                    BorderRadiusGeometry.horizontal(right: Radius.circular(8))))
      ],
    );
  }
}
