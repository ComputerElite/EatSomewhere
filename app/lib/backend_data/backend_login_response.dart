class BackendLoginResponse {
  bool? Requires2fa;
  String? Nonce;
  String? SessionId;
  String? Error;
  bool Success;
  String? ChallengeId;

  BackendLoginResponse({
    this.Requires2fa,
    this.Nonce,
    this.SessionId,
    this.Error,
    required this.Success,
    this.ChallengeId,
  });

  factory BackendLoginResponse.fromJson(Map<String, dynamic> json) {
    return BackendLoginResponse(
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

enum ChallengeType { 
  password, 
  totp, 
  register 
}
