class EventInfoModel {
  int eventID;
  String eventNameEn;
  String eventNameAr;
  String eventDescEn;
  String eventDescAr;
  double latitude;
  double longitude;
  int eventTypeID;
  int countryID;
  String eventDate;
  int userID;
  int statusID;
  String eventImage;
  bool isFav;

  EventInfoModel(
      {this.eventID,
      this.eventNameEn,
      this.eventNameAr,
      this.eventDescEn,
      this.eventDescAr,
      this.latitude,
      this.longitude,
      this.eventTypeID,
      this.countryID,
      this.eventDate,
      this.userID,
      this.statusID,
      this.eventImage});

  EventInfoModel.fromJson(Map<String, dynamic> json) {
    eventID = json['eventID'];
    eventNameEn = json['eventNameEn'];
    eventNameAr = json['eventNameAr'];
    eventDescEn = json['eventDescEn'];
    eventDescAr = json['eventDescAr'];
    latitude = json['latitude'];
    longitude = json['longitude'];
    eventTypeID = json['eventTypeID'];
    countryID = json['countryID'];
    eventDate = json['eventDate'];
    userID = json['userID'];
    statusID = json['statusID'];
    eventImage = json['eventImage'];
    isFav = false;
  }

  Map<String, dynamic> toJson() {
    final Map<String, dynamic> data = new Map<String, dynamic>();
    data['eventID'] = this.eventID;
    data['eventNameEn'] = this.eventNameEn;
    data['eventNameAr'] = this.eventNameAr;
    data['eventDescEn'] = this.eventDescEn;
    data['eventDescAr'] = this.eventDescAr;
    data['latitude'] = this.latitude;
    data['longitude'] = this.longitude;
    data['eventTypeID'] = this.eventTypeID;
    data['countryID'] = this.countryID;
    data['eventDate'] = this.eventDate;
    data['userID'] = this.userID;
    data['statusID'] = this.statusID;
    data['eventImage'] = this.eventImage;
    return data;
  }
}
