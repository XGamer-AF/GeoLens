class EventImageModel {
  int _eventImageID;
  int _eventID;
  int _statusID;

  EventImageModel({int eventImageID, int eventID, int statusID}) {
    this._eventImageID = eventImageID;
    this._eventID = eventID;
    this._statusID = statusID;
  }

  int get eventImageID => _eventImageID;
  set eventImageID(int eventImageID) => _eventImageID = eventImageID;
  int get eventID => _eventID;
  set eventID(int eventID) => _eventID = eventID;
  int get statusID => _statusID;
  set statusID(int statusID) => _statusID = statusID;

  EventImageModel.fromJson(Map<String, dynamic> json) {
    _eventImageID = json['eventImageID'];
    _eventID = json['eventID'];
    _statusID = json['statusID'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['eventImageID'] = this._eventImageID;
    data['eventID'] = this._eventID;
    data['statusID'] = this._statusID;
    return data;
  }
}
