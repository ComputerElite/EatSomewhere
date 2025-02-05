class BackendRegisterRequest {
  String? Username;
  String? Password;

  BackendRegisterRequest({this.Username, this.Password});

  factory BackendRegisterRequest.fromJson(Map<String, dynamic> json) {
    return BackendRegisterRequest(
      Username: json['Username'] as String?,
      Password: json['Password'] as String?,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'Username': Username,
      'Password': Password,
    };
  }
}

class BackendLoginRequest {
  String? Username;
  String? ChallengeId;
  String? PasswordHash;
  String? CNonce;

  BackendLoginRequest({this.Username, this.ChallengeId, this.PasswordHash, this.CNonce});

  factory BackendLoginRequest.fromJson(Map<String, dynamic> json) {
    return BackendLoginRequest(
      Username: json['Username'] as String?,
      ChallengeId: json['ChallengeId'] as String?,
      PasswordHash: json['PasswordHash'] as String?,
      CNonce: json['CNonce'] as String?,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'Username': Username,
      'ChallengeId': ChallengeId,
      'PasswordHash': PasswordHash,
      'CNonce': CNonce,
    };
  }
}

class BackendRegisterResponse {
  String? SessionId;
  String? Error;
  bool Success;

  BackendRegisterResponse({this.SessionId, this.Error, required this.Success});

  factory BackendRegisterResponse.fromJson(Map<String, dynamic> json) {
    return BackendRegisterResponse(
      SessionId: json['SessionId'] as String?,
      Error: json['Error'] as String?,
      Success: json['Success'] as bool,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'SessionId': SessionId,
      'Error': Error,
      'Success': Success,
    };
  }
}

class LoginResponse {
  bool? Requires2fa;
  String? Nonce;
  String? SessionId;
  String? Error;
  bool Success;
  String? ChallengeId;

  LoginResponse({
    this.Requires2fa,
    this.Nonce,
    this.SessionId,
    this.Error,
    required this.Success,
    this.ChallengeId,
  });

  factory LoginResponse.fromJson(Map<String, dynamic> json) {
    return LoginResponse(
      Requires2fa: json['Requires2fa'] as bool?,
      Nonce: json['Nonce'] as String?,
      SessionId: json['SessionId'] as String?,
      Error: json['Error'] as String?,
      Success: json['Success'] as bool,
      ChallengeId: json['ChallengeId'] as String?,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'Requires2fa': Requires2fa,
      'Nonce': Nonce,
      'SessionId': SessionId,
      'Error': Error,
      'Success': Success,
      'ChallengeId': ChallengeId,
    };
  }
}

class Challenge {
  String Id;
  String UserId;
  String Nonce;
  ChallengeType Type;
  String Username;

  Challenge({
    required this.Id,
    required this.UserId,
    required this.Nonce,
    required this.Type,
    required this.Username,
  });

  factory Challenge.fromJson(Map<String, dynamic> json) {
    return Challenge(
      Id: json['Id'] as String,
      UserId: json['UserId'] as String,
      Nonce: json['Nonce'] as String,
      Type: ChallengeType.values.firstWhere((e) => e.toString() == 'ChallengeType.${json['Type']}'),
      Username: json['Username'] as String,
    );
  }

  Map<String, dynamic> toJson() {
    return {
      'Id': Id,
      'UserId': UserId,
      'Nonce': Nonce,
      'Type': Type.toString().split('.').last,
      'Username': Username,
    };
  }
}

enum ChallengeType { 
  password, 
  totp, 
  register 
}
