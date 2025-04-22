import 'package:eat_somewhere/backend_data/Backend_user.dart';
import 'package:eat_somewhere/widgets/chips/combinable_chip.dart';
import 'package:flutter/material.dart';

class UserColors {
  static final List<Color> userColors = [
    Colors.redAccent,
    Colors.blueAccent,
    Colors.greenAccent,
    Colors.orangeAccent,
    Colors.purpleAccent,
    Colors.tealAccent,
    Colors.amberAccent,
    // Add more as needed
  ];
  static Color getColor(BackendUser? user) {
    return userColors[(user?.Id.hashCode ?? 1) % userColors.length];
  }
  
  static getContrastColor(BackendUser? user) {
    return getColor(user).computeLuminance() > 0.5 ? Colors.black : Colors.white;
  }
}

class UserChip extends CombinableChip {
  final BackendUser? user;
  final Function()? onTap;

  UserChip({Key? key, required this.user, this.onTap, shape}) : super(key: key, shape: shape);

  @override
  Widget build(BuildContext context) {
    return InkWell(
      onTap: onTap,
      child: Chip(
          padding: const EdgeInsets.all(0),
          backgroundColor:
              UserColors.getColor(user),
              shape: shape,
          label: Text(
            user?.username ?? "Unspecified user",
            style: TextStyle(color: UserColors.getContrastColor(user)),
          )),
    );
  }
}
