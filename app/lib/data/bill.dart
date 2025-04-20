import 'package:eat_somewhere/backend_data/Backend_user.dart';

class Bill {
  BackendUser? user;
  BackendUser? recipient;
  int amount = 0;

  Bill.fromJson(Map<String, dynamic> json) {
    user = BackendUser.fromJson(json["User"]);
    recipient = BackendUser.fromJson(json["Recipient"]);
    amount = json["Amount"];
  }
  
  toJson() {
    return {
      "User": user?.toJson(),
      "Recipient": recipient?.toJson(),
      "Amount": amount,
    };
  }
}