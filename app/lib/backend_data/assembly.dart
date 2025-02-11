import 'package:eat_somewhere/backend_data/Backend_user.dart';

class Assembly {
  String? id;
  List<BackendUser> users;
  List<BackendUser> pending;
  String name;
  String description;
  List<BackendUser> admins;

  Assembly({
    required this.id,
    required this.users,
    required this.pending,
    required this.name,
    required this.description,
    required this.admins,
  });

  factory Assembly.fromJson(Map<String, dynamic> json) {
    return Assembly(
      id: json['Id'] as String,
      users: (json['Users'] as List<dynamic>)
          .map((e) => BackendUser.fromJson(e as Map<String, dynamic>))
          .toList(),
      pending: (json['Pending'] as List<dynamic>)
          .map((e) => BackendUser.fromJson(e as Map<String, dynamic>))
          .toList(),
      name: json['Name'] as String,
      description: json['Description'] as String,
      admins: (json['Admins'] as List<dynamic>)
          .map((e) => BackendUser.fromJson(e as Map<String, dynamic>))
          .toList(),
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'Id': id,
      'Users': users.map((e) => e.toJson()).toList(),
      'Pending': pending.map((e) => e.toJson()).toList(),
      'Name': name,
      'Description': description,
      'Admins': admins.map((e) => e.toJson()).toList(),
    };
  }
}

class DummyAssembly {
  String? id;
  String? name;

  DummyAssembly({this.id, this.name});

  factory DummyAssembly.fromJson(Map<String, dynamic> json) {
    return DummyAssembly(
      id: json['id'],
      name: json['name'],
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'id': id,
      'name': name,
    };
  }
}