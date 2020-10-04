class EventTypesModel {
  int eventTypeID;
  String eventTypeNameEn;
  String eventTypeNameAr;
  int statusID;

  EventTypesModel(
      {this.eventTypeID,
      this.eventTypeNameEn,
      this.eventTypeNameAr,
      this.statusID});

  EventTypesModel.fromJson(Map<String, dynamic> json) {
    eventTypeID = json['eventTypeID'];
    eventTypeNameEn = json['eventTypeNameEn'];
    eventTypeNameAr = json['eventTypeNameAr'];
    statusID = json['statusID'];
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['eventTypeID'] = this.eventTypeID;
    data['eventTypeNameEn'] = this.eventTypeNameEn;
    data['eventTypeNameAr'] = this.eventTypeNameAr;
    data['statusID'] = this.statusID;
    return data;
  }
}
