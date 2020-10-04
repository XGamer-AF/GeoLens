class EventRefrenceModel {
  int _eventRefrencesID;
  int _orderID;
  String _eventURL;
  int _eventID;
  int _statusID;

  EventRefrenceModel(
      {int eventRefrencesID,
        int orderID,
        String eventURL,
        int eventID,
        int statusID}) {
    this._eventRefrencesID = eventRefrencesID;
    this._orderID = orderID;
    this._eventURL = eventURL;
    this._eventID = eventID;
    this._statusID = statusID;
  }

  int get eventRefrencesID => _eventRefrencesID;
  set eventRefrencesID(int eventRefrencesID) =>
      _eventRefrencesID = eventRefrencesID;
  int get orderID => _orderID;
  set orderID(int orderID) => _orderID = orderID;
  String get eventURL => _eventURL;
  set eventURL(String eventURL) => _eventURL = eventURL;
  int get eventID => _eventID;
  set eventID(int eventID) => _eventID = eventID;
  int get statusID => _statusID;
  set statusID(int statusID) => _statusID = statusID;

  EventRefrenceModel.fromJson(Map<String, dynamic> json) {
    _eventRefrencesID = json['eventRefrencesID'];
    _orderID = json['orderID'];
    _eventURL = json['eventURL'];
    _eventID = json['eventID'];
    _statusID = json['statusID'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['eventRefrencesID'] = this._eventRefrencesID;
    data['orderID'] = this._orderID;
    data['eventURL'] = this._eventURL;
    data['eventID'] = this._eventID;
    data['statusID'] = this._statusID;
    return data;
  }
}
