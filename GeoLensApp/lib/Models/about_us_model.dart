class AboutUsModel {
  int _aboutUsID;
  String _aboutUsDescEn;
  String _aboutUsDescAr;
  int _statusID;

  AboutUsModel(
      {int aboutUsID,
        String aboutUsDescEn,
        String aboutUsDescAr,
        int statusID}) {
    this._aboutUsID = aboutUsID;
    this._aboutUsDescEn = aboutUsDescEn;
    this._aboutUsDescAr = aboutUsDescAr;
    this._statusID = statusID;
  }

  int get aboutUsID => _aboutUsID;
  set aboutUsID(int aboutUsID) => _aboutUsID = aboutUsID;
  String get aboutUsDescEn => _aboutUsDescEn;
  set aboutUsDescEn(String aboutUsDescEn) => _aboutUsDescEn = aboutUsDescEn;
  String get aboutUsDescAr => _aboutUsDescAr;
  set aboutUsDescAr(String aboutUsDescAr) => _aboutUsDescAr = aboutUsDescAr;
  int get statusID => _statusID;
  set statusID(int statusID) => _statusID = statusID;

  AboutUsModel.fromJson(Map<String, dynamic> json) {
    _aboutUsID = json['aboutUsID'];
    _aboutUsDescEn = json['aboutUsDescEn'];
    _aboutUsDescAr = json['aboutUsDescAr'];
    _statusID = json['statusID'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['aboutUsID'] = this._aboutUsID;
    data['aboutUsDescEn'] = this._aboutUsDescEn;
    data['aboutUsDescAr'] = this._aboutUsDescAr;
    data['statusID'] = this._statusID;
    return data;
  }
}
